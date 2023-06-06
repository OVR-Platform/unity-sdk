/**
 * OVR Unity SDK License
 *
 * Copyright 2021 OVR
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
 * with services provided by OVR.
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
using System.Collections.Generic;
using UnityEngine;

namespace Over_Editor
{
    /******************** Nft Collection ********************/

    [Serializable]
    public class UserNftResponse
    {
        public UserNftResponse()
        {
            Nfts = new List<Nft>();
        }

        [Tooltip("Is the result of the ask positive?")]
        public bool result;

        [Tooltip("The list of the Nfts.")]
        public List<Nft> Nfts;

        [Tooltip("Is a next page avaible?")]
        public bool nextPage;

        [Tooltip("The current page we are in.")]
        public string currentPage;

        [Tooltip("The message the server wants to give to the user.")]
        public string message;

        /// <summary>
        /// Empty all fields and the NFT list.
        /// </summary>
        public void Clear()
        {
            result = false;
            Nfts.Clear();
            nextPage = false;
            currentPage = string.Empty;
            message = string.Empty;
        }
    }

    [Serializable]
    public class Nft
    {
        [Tooltip("The identifier of the Nft.")]
        public string id;

        [Tooltip("The name of the Nft.")]
        public string name;

        [Tooltip("The url where to download the thumbnail.")]
        public string thumbnailUrl;

        public Texture thumbnailImage;

        [Tooltip("The physical url of the Nft.")]
        public string fileUrl;

        [Tooltip("The name of the Nft creator.")]
        public string creatorName;

        [Tooltip(".")]
        public string imageUrl;

        [Tooltip(".")]
        public string permalink;

        [Tooltip("Is the Nft ready to be download or the server have to convert it first in GameObject?")]
        public bool isImported;

        [Tooltip("The contract address of the Nft to know where it's from.")]
        public string assetAddress;

        [Tooltip("The download progress of the NFT.")]
        public float downloadProgress = -1;
    }

    /******************** Nft Single ********************/

    [Serializable]
    public class UserNftSingleResponse
    {
        public bool result;
        public NftSingle NftData;
        public string message;
    }

    [Serializable]
    public class NftSingle
    {
        public string uuid;
        public string file_public_name;
        public string file_url;
        public string file_name;
        public string file_extension;
        public string file_extension_mime;
        public string file_content_type;
        public string thumbnail_url;
        public bool status;
        public string created_at;
        public string nft_asset_address;
        public string nft_id;

        // Metadata
        public string metadata_order;
        public string metadata_value;
        public string metadata_max_value;
        public string metadata_trait_type;
        public string metadata_trait_count;
        public string metadata_display_type;

        /// <summary>
        /// Create a shallow copy of the object.
        /// </summary>
        /// <returns>The new instance of the object copied.</returns>
        public NftSingle ShallowCopy() => (NftSingle)MemberwiseClone();
    }
}
