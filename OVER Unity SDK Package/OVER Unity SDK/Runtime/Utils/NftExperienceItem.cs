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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverSDK
{
    public class NftExperienceItem : MonoBehaviour
    {
        public string NftId { get => nftId; set => nftId = value; }
        [SerializeField] private string nftId;

        public string NftName { get => nftName; set => nftName = value; }
        [SerializeField] private string nftName;

        public string NftAddress { get => nftAddress; set => nftAddress = value; }
        [SerializeField] private string nftAddress;

        public bool IsControllable { get => isControllable; set => isControllable = value; }
        [Space] [SerializeField] private bool isControllable;
        
        public bool IsNetObject { get => isNetObject; set => isNetObject = value; }
        [Space] [SerializeField] private bool isNetObject = true;

        public void UpdateNftData(string newId, string newName, string newAddress)
        {
            NftId = newId;
            NftName = newName;
            NftAddress = newAddress;
        }

        [Space]
        [SerializeField] private string objectID;
        public string ObjectID { get => objectID; set => objectID = value; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(ObjectID))
            {
                //Genera guid
                ObjectID = System.Guid.NewGuid().ToString();
            }
        }
    } 
}
