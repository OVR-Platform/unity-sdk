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

using System.Collections.Generic;
using UnityEngine;

namespace OverSDK
{
    [ExecuteInEditMode]
    public class OvrHexBounds : MonoBehaviour, ISerializationCallbackReceiver
    {
        // Hexagons this perimeter was generated from. Serialized so the SDK window
        // can tell whether the objects in the open scene still belong to the land
        // currently selected (the selection lives in the project preferences, the
        // objects live in the scene: the two drift apart on every scene change).
        [ReadOnly]
        public List<string> hexIds = new List<string>();

        // Geographic origin of the scene: the geocenter of the main land, which Unity
        // (0,0,0) corresponds to. Anything that has to be placed by its coordinates
        // needs this frame, and it must survive scene saves and domain reloads.
        [ReadOnly]
        public bool hasGeoOrigin;
        [ReadOnly]
        public double originLatitude;
        [ReadOnly]
        public double originLongitude;

        // Unity center of every hexagon, used to place the OVRMaps of a folder.
        // Unity does not serialize dictionaries: without the two backing lists the
        // content is lost on every domain reload and scene reopen.
        public Dictionary<string, Vector3> folderCenters = new Dictionary<string, Vector3>();

        [SerializeField, HideInInspector] private List<string> folderCenterKeys = new List<string>();
        [SerializeField, HideInInspector] private List<Vector3> folderCenterValues = new List<Vector3>();

        public void OnBeforeSerialize()
        {
            folderCenterKeys.Clear();
            folderCenterValues.Clear();

            if (folderCenters == null)
                return;

            foreach (KeyValuePair<string, Vector3> entry in folderCenters)
            {
                folderCenterKeys.Add(entry.Key);
                folderCenterValues.Add(entry.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            folderCenters = new Dictionary<string, Vector3>();

            int count = Mathf.Min(folderCenterKeys.Count, folderCenterValues.Count);
            for (int i = 0; i < count; i++)
            {
                folderCenters[folderCenterKeys[i]] = folderCenterValues[i];
            }
        }

        protected void Update()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}
