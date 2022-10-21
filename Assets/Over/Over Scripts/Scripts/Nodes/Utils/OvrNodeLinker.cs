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
using System.Collections.Generic;
using UnityEngine;

namespace Over
{
    [Serializable]
    public struct OvrNodeLink
    {
        [OvrNode]
        public OvrNode ovrNode;
        public List<string> nodesIds;
    }

    public enum OvrNodeLinkerActionType { LinkToNode, LinkGameObject, LinkInstantiatedGameObject }
    public enum OvrNodeLinkType { From, To }

    public class OvrNodeLinker : OvrNode
    {
        public OvrNodeLinkerActionType actionType;
        public OvrNodeLinkType linkType;

        [OvrNode]
        public OvrInstantiate ovrInstantiate;
        public GameObject linkGameObject;
        public List<OvrNodeLink> ovrNodeLinks = new List<OvrNodeLink>();

        [OvrNode]
        public OvrNode ovrNodeToLink;
        [OvrNode]
        public OvrNode ovrNodeWhereLink;

        protected override void Execution()
        {
            switch (actionType)
            {
                case OvrNodeLinkerActionType.LinkInstantiatedGameObject:

                    if (ovrInstantiate == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }

                    break;
                case OvrNodeLinkerActionType.LinkGameObject:

                    if (linkGameObject == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }

                    break;
                case OvrNodeLinkerActionType.LinkToNode:

                    if (ovrNodeToLink == null || ovrNodeWhereLink == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }

                    break;               
            }


            switch (actionType)
            {
                case OvrNodeLinkerActionType.LinkInstantiatedGameObject:

                    OvrObject ovrObject = ovrInstantiate.LastNewOvrObject;

                    if (ovrObject != null)
                    {
                        foreach (var item in ovrNodeLinks)
                        {
                            if (item.ovrNode == null)
                            {
                                if (Application.isEditor)
                                    Debug.LogError("Null reference at gameObject " + gameObject.name);
                                continue;
                            }

                            foreach (var nodeId in item.nodesIds)
                            {
                                OvrNode[] ovrNodes = ovrObject.GetComponentsInChildren<OvrNode>(true);

                                foreach (var ovrNode in ovrNodes)
                                {
                                    if (ovrNode.NodeId == nodeId)
                                    {
                                        switch (linkType)
                                        {
                                            case OvrNodeLinkType.From:
                                                item.ovrNode.AddNode(ovrNode);
                                                break;
                                            case OvrNodeLinkType.To:
                                                ovrNode.AddNode(item.ovrNode);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }

                    break;
                case OvrNodeLinkerActionType.LinkGameObject:

                    foreach (var item in ovrNodeLinks)
                    {
                        if (item.ovrNode == null)
                        {
                            if (Application.isEditor)
                                Debug.LogError("Null reference at gameObject " + gameObject.name);
                            continue;
                        }

                        foreach (var nodeId in item.nodesIds)
                        {
                            OvrNode[] ovrNodes = linkGameObject.GetComponentsInChildren<OvrNode>(true);

                            foreach (var ovrNode in ovrNodes)
                            {
                                if (ovrNode.NodeId == nodeId)
                                {
                                    switch (linkType)
                                    {
                                        case OvrNodeLinkType.From:
                                            item.ovrNode.AddNode(ovrNode);
                                            break;
                                        case OvrNodeLinkType.To:
                                            ovrNode.AddNode(item.ovrNode);
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    break;
                case OvrNodeLinkerActionType.LinkToNode:

                    ovrNodeWhereLink.AddNode(ovrNodeToLink);

                    break;
            }
        }
    }
}