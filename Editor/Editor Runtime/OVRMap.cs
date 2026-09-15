/**
 * OVER Unity SDK License
 *
 * Copyright 2021 Over The Realty
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * 1. The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * 2. All copies of substantial portions of the Software may only be used in connection
 * with services provided by OVER.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
//using UnityEditor;
using UnityEngine;

namespace OverSDK
{
    [Serializable]
    public class OvrMapInfo
    {
        public string land_map_uuid;
        public RepositionData reposition_data;
    }

    [Serializable]
    public class RepositionData
    {
        public bool hasData;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    [ExecuteInEditMode]
    public class OVRMap : MonoBehaviour
    {
        // OVRLand the reposition data is measured against. Shown read only in the
        // inspector so it is always clear which hex the values below refer to.
        [ReadOnly]
        public string landHexId;

        // Readable name of the same OVRLand, shown next to the hex id
        [ReadOnly]
        public string landSentence;

        [ReadOnly]
        public Vector3 mapRelativePosition;
        [ReadOnly]
        public Quaternion mapRelativeRotation;
        [ReadOnly]
        public Vector3 mapRelativeScale;

        public OvrMapInfo tmpMappingInfo;
        public OvrMapInfo defaultMappingInfo;

        private bool isSaving;

        public Action<OvrMapInfo, Action<bool>> OnSaveMappingBtnClicked;

        public Vector3 centerReferencePosition = Vector3.zero;

        // Raised by one every time the server accepts a save. The editor watches it to
        // know that what the server holds has changed and has to be read again: this
        // component cannot call the APIs itself, they live in another assembly.
        [ReadOnly]
        public int serverRevision;

        // Poses travel to the server as text and come back with different trailing
        // digits: compared exactly, an untouched pose reads as modified. These are the
        // thresholds below which two poses are considered the same one.
        public const float positionTolerance = 0.0001f;   // 0.1 mm
        public const float rotationTolerance = 0.000001f; // quaternion component
        public const float scaleTolerance = 0.0001f;

        public static bool SamePosition(Vector3 a, Vector3 b)
        {
            return (a - b).sqrMagnitude <= positionTolerance * positionTolerance;
        }

        public static bool SameRotation(Quaternion a, Quaternion b)
        {
            // q and -q are the same rotation, and the two sides of the comparison do
            // not always come from the same place (one from the transform, one from
            // the server payload), so both signs count as a match.
            return SameComponents(a, b) || SameComponents(a, new Quaternion(-b.x, -b.y, -b.z, -b.w));
        }

        private static bool SameComponents(Quaternion a, Quaternion b)
        {
            return Mathf.Abs(a.x - b.x) <= rotationTolerance
                && Mathf.Abs(a.y - b.y) <= rotationTolerance
                && Mathf.Abs(a.z - b.z) <= rotationTolerance
                && Mathf.Abs(a.w - b.w) <= rotationTolerance;
        }

        public static bool SameScale(Vector3 a, Vector3 b)
        {
            // The object is always kept with a flipped Y (see Update), while the server
            // may store the scale with either sign: compare in the same convention or
            // an untouched scale reads as modified forever.
            return (FlippedScale(a) - FlippedScale(b)).sqrMagnitude <= scaleTolerance * scaleTolerance;
        }

        private static Vector3 FlippedScale(Vector3 scale)
        {
            return new Vector3(Mathf.Abs(scale.x), -Mathf.Abs(scale.y), Mathf.Abs(scale.z));
        }

#if !APP_MAIN
        private void Update()
        {
            if (transform.parent != null)
                transform.SetParent(null);

            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), -Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
        }

        public void InitData(string uuid, RepositionData data, Vector3 centerReference)
        {
            centerReferencePosition = centerReference;

            tmpMappingInfo = new OvrMapInfo();
            tmpMappingInfo.land_map_uuid = uuid;

            if (data != null && data.hasData)
            {
                tmpMappingInfo.reposition_data = data;
                SetRepositionData();
                CaptureServerPose();
            }
            else
            {
                // No reposition saved yet: the offset from the hex is zero, but the map
                // still belongs on its own hex, not wherever the scene origin happens
                // to be.
                transform.localPosition = centerReferencePosition;
                mapRelativePosition = Vector3.zero;
                mapRelativeRotation = transform.localRotation;
                mapRelativeScale = transform.localScale;
            }
        }

        public void InitData(string uuid, RepositionData userData, RepositionData defaultData, Vector3 centerReference)
        {
            centerReferencePosition = centerReference;

            tmpMappingInfo = new OvrMapInfo();
            tmpMappingInfo.land_map_uuid = uuid;

            // The official pose of the mapping: the one the app actually uses. It is
            // kept even when no personal adjustment exists yet, so a pose saved later
            // can still be compared against it.
            if (defaultData != null)
            {
                defaultMappingInfo = new OvrMapInfo
                {
                    land_map_uuid = uuid,
                    reposition_data = defaultData
                };
            }

            if (userData != null && userData.hasData)
            {
                tmpMappingInfo.reposition_data = userData;
                SetRepositionData();
                CaptureServerPose();
            }
            else if (defaultData != null)
            {
                tmpMappingInfo.reposition_data = defaultData;
                SetRepositionData();
                CaptureServerPose();
            }
            else
            {
                // Neither a user nor a default reposition: sit on the hex itself.
                transform.localPosition = centerReferencePosition;
                mapRelativePosition = Vector3.zero;
                mapRelativeRotation = transform.localRotation;
                mapRelativeScale = transform.localScale;
            }
        }

        public void SaveMappingData()
        {
            if (OnSaveMappingBtnClicked == null)
            {
                Debug.LogError("[OVER][OVRMap] For security reasons the Over Editor popup must be open in order to save changes correctly");
                //EditorUtility.DisplayDialog("Attention!", "For security reasons the Over Editor popup must be open in order to save changes correctly", "Ok");
                return;
            }
            
            if (!isSaving)
            {
                tmpMappingInfo.reposition_data.hasData = true;
                tmpMappingInfo.reposition_data.position = transform.localPosition - centerReferencePosition;
                tmpMappingInfo.reposition_data.rotation = transform.localRotation;
                tmpMappingInfo.reposition_data.scale = new Vector3(Mathf.Abs(transform.localScale.x), -Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));

                isSaving = true;

                OnSaveMappingBtnClicked?.Invoke(tmpMappingInfo, (bool result) => OnMappingDataSaved(result));
            }
            else
            {
                Debug.Log("[OVER][OVRMap] A save is already in progress, please wait.");
            }
        }

        private void SetRepositionData()
        {
            transform.localPosition = centerReferencePosition + tmpMappingInfo.reposition_data.position;
            transform.localRotation = tmpMappingInfo.reposition_data.rotation;
            transform.localScale = tmpMappingInfo.reposition_data.scale;
            Update();
        }

        // The pose the scene is holding, in the frame every position travels in: the
        // offset from the hexagon this OVRMap belongs to. It is what an experience
        // publishes, and what the position check is asked about.
        public RepositionData GetScenePose()
        {
            return new RepositionData
            {
                hasData = true,
                position = transform.localPosition - centerReferencePosition,
                rotation = transform.localRotation,
                scale = transform.localScale
            };
        }

        // Puts the object on a pose expressed against its hexagon, which is how every
        // position the server hands out is expressed.
        public void ApplyPose(RepositionData pose)
        {
            if (pose == null || !pose.hasData)
                return;

            transform.localPosition = centerReferencePosition + pose.position;
            transform.localRotation = pose.rotation;
            transform.localScale = pose.scale;

            Update();
        }

        // Replaces what the server is known to hold, leaving the scene alone: the pose
        // in the editor belongs to the user until they choose to save it.
        public void RefreshServerData(RepositionData userPose, RepositionData officialPose)
        {
            if (officialPose != null && officialPose.hasData)
            {
                defaultMappingInfo = new OvrMapInfo
                {
                    land_map_uuid = tmpMappingInfo != null ? tmpMappingInfo.land_map_uuid : null,
                    reposition_data = officialPose
                };
            }

            // The pose stored for this account, which is the official one when no
            // personal adjustment exists
            RepositionData serverPose = userPose != null && userPose.hasData ? userPose : officialPose;

            if (serverPose == null || !serverPose.hasData)
                return;

            mapRelativePosition = serverPose.position;
            mapRelativeRotation = serverPose.rotation;
            mapRelativeScale = serverPose.scale;
        }

        // Snapshot of the pose the server holds, relative to the hex the mapping
        // belongs to. Only loading the mapping and a successful save may move it:
        // Undo and Reset change the scene, not what is stored remotely.
        private void CaptureServerPose()
        {
            mapRelativePosition = transform.localPosition - centerReferencePosition;
            mapRelativeRotation = transform.localRotation;
            mapRelativeScale = transform.localScale;
        }

        private void OnMappingDataSaved(bool result)
        {
            isSaving = false;

            if (result)
            {
                Debug.Log("[OVER][OVRMap] OVRMap position saved on server.");
                SetRepositionData();
                CaptureServerPose();

                serverRevision++;
            }
            else
            {
                Debug.LogError("[OVER][OVRMap] Error while saving the OVRMap position, please retry.");
            }
        }

        public bool IsDataSaved()
        {
            return SamePosition(transform.position - centerReferencePosition, mapRelativePosition)
                && SameRotation(transform.rotation, mapRelativeRotation)
                && SameScale(transform.localScale, mapRelativeScale);
        }

        public void UndoMappingData()
        {
            // Back to the pose the server holds for this account. tmpMappingInfo may
            // have been overwritten in the meantime (Realign to Official Pose), so the
            // snapshot is the only reliable source.
            tmpMappingInfo.reposition_data = new RepositionData
            {
                hasData = true,
                position = mapRelativePosition,
                rotation = mapRelativeRotation,
                scale = mapRelativeScale
            };

            SetRepositionData();
        }

        public void ResetMappingData()
        {
            if (defaultMappingInfo != null && defaultMappingInfo.reposition_data.hasData)
            {
                // A copy, not the same instance: saving writes into
                // tmpMappingInfo.reposition_data and would otherwise overwrite the
                // official pose held in defaultMappingInfo.
                tmpMappingInfo.reposition_data = new RepositionData
                {
                    hasData = true,
                    position = defaultMappingInfo.reposition_data.position,
                    rotation = defaultMappingInfo.reposition_data.rotation,
                    scale = defaultMappingInfo.reposition_data.scale
                };

                SetRepositionData();
            }
            else
            {
                Debug.Log("[OVER][OVRMap] No official OVRMap position available to realign to.");
            }
        }
#endif

    }
}
