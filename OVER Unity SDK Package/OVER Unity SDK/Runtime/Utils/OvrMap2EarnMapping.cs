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
using UnityEngine;

namespace OverSDK
{
    [Serializable]
    public struct OvrMap2EarnMappingInfo
    {
        public string land_map_uuid;
        public RepositionData reposition_data;
    }

    [Serializable]
    public struct RepositionData
    {
        public bool hasData;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    [ExecuteInEditMode]
    public class OvrMap2EarnMapping : MonoBehaviour
    {
        [ReadOnly]
        public Vector3 mapRelativePosition;
        [ReadOnly]
        public Quaternion mapRelativeRotation;
        [ReadOnly]
        public Vector3 mapRelativeScale;

        private OvrMap2EarnMappingInfo tmpMappingInfo;
        private bool isSaving;

        public Action<OvrMap2EarnMappingInfo, Action<bool>> OnSaveMappingBtnClicked;

        public Vector3 centerReferencePosition = Vector3.zero;

#if !APP_MAIN
        private void Update()
        {
            transform.SetParent(null);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), -Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
        }

        public void InitData(string uuid, RepositionData data, Vector3 centerReference)
        {
            centerReferencePosition = centerReference;

            tmpMappingInfo = new OvrMap2EarnMappingInfo();
            tmpMappingInfo.land_map_uuid = uuid;

            if (data.hasData)
            {
                tmpMappingInfo.reposition_data = data;
                SetRepositionData();
            }
        }

        public void SaveMappingData()
        {
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
                Debug.Log("Already saving, please wait");
            }
        }

        private void SetRepositionData()
        {
            transform.localPosition = centerReferencePosition + tmpMappingInfo.reposition_data.position;
            transform.localRotation = tmpMappingInfo.reposition_data.rotation;
            transform.localScale = tmpMappingInfo.reposition_data.scale;
            Update();

            mapRelativePosition = transform.localPosition;
            mapRelativeRotation = transform.localRotation;
            mapRelativeScale = transform.localScale;
        }

        private void OnMappingDataSaved(bool result)
        {
            isSaving = false;

            if (result)
            {
                Debug.Log("Mapping data saved on server!");
                SetRepositionData();
            }
            else
            {
                Debug.LogError("Error in saving, please retry!");
            }
        }

        public bool IsDataSaved()
        {
            return (transform.position == mapRelativePosition) && (transform.rotation == mapRelativeRotation) && (transform.localScale == mapRelativeScale);
        }
#endif

    }
}
