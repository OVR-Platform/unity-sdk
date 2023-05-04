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

using BlueGraph;
using BlueGraph.Editor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OverSDK.VisualScripting.Editor
{
    public class OverGetVariableSearchProvider : ISearchProvider
    {
        OverGraph graph;

        public IEnumerable<SearchResult> GetSearchResults(SearchFilter filter)
        {
            OverScriptManager.Main.UpdateMappings();        //!potrebbe generare overhead: per ora è necessario all'uscita dal play
            if (!string.IsNullOrEmpty(graph.GUID) && OverScriptManager.Main.overDataMappings.ContainsKey(graph.GUID))
            {
                var globals = OverScriptManager.Main.globalVariables.VariableDict.Values;
                Dictionary<string, OverVariableData> variableDict = new Dictionary<string, OverVariableData>();

                variableDict = OverScriptManager.Main.overDataMappings[graph.GUID].overScript.data.VariableDict;

                foreach (var global in globals)
                {
                    if (!variableDict.Keys.Contains(global.id))
                    {
                        global.isGlobal = true;
                        variableDict.Add(global.id, global);
                    }
                }

                var variables = variableDict.Values;

                foreach (var variable in variables)
                {
                    Type nodeType;

                    switch (variable.type)
                    {
                        //getter
                        case OverVariableType.Int: nodeType = typeof(OverGetVariableInt); break;
                        case OverVariableType.Float: nodeType = typeof(OverGetVariableFloat); break;
                        case OverVariableType.Bool: nodeType = typeof(OverGetVariableBool); break;
                        case OverVariableType.String: nodeType = typeof(OverGetVariableString); break;
                        case OverVariableType.Vector2: nodeType = typeof(OverGetVariableVector2); break;
                        case OverVariableType.Vector3: nodeType = typeof(OverGetVariableVector3); break;
                        case OverVariableType.Quaternion: nodeType = typeof(OverGetVariableQuaternion); break;
                        case OverVariableType.Transform: nodeType = typeof(OverGetVariableTransform); break;
                        case OverVariableType.RectTransform: nodeType = typeof(OverGetVariableRectTransform); break;
                        case OverVariableType.Rigidbody: nodeType = typeof(OverGetVariableRigidbody); break;
                        case OverVariableType.Object: nodeType = typeof(OverGetVariableGameObject); break;
                        case OverVariableType.Renderer: nodeType = typeof(OverGetVariableRenderer); break;
                        case OverVariableType.LineRenderer: nodeType = typeof(OverGetVariableLineRenderer); break;
                        case OverVariableType.Audio: nodeType = typeof(OverGetVariableAudioSource); break;
                        case OverVariableType.Video: nodeType = typeof(OverGetVariableVideoPlayer); break;
                        case OverVariableType.Animator: nodeType = typeof(OverGetVariableAnimator); break;
                        case OverVariableType.Light: nodeType = typeof(OverGetVariableLight); break;
                        //case OverVariableType.Button: nodeType = typeof(OverGetVariableButton); break;
                        case OverVariableType.Text: nodeType = typeof(OverGetVariableText); break;
                        case OverVariableType.TextTMP: nodeType = typeof(OverGetVariableTextTMP); break;
                        case OverVariableType.Image: nodeType = typeof(OverGetVariableImage); break;
                        case OverVariableType.RawImage: nodeType = typeof(OverGetVariableRawImage); break;
                        case OverVariableType.Color: nodeType = typeof(OverGetVariableColor); break;
                        default: nodeType = null; break;
                    }

                    var entry = NodeReflection.GetNodeType(nodeType);
                    if (entry != null)
                    {
                        var node = entry;
                        Tuple<OverVariableData, NodeReflectionData> tuple = new Tuple<OverVariableData, NodeReflectionData>(variable, node);

                        if (
                            IsCompatible(filter.SourcePort, node) &&
                            IsInSupportedTags(filter.IncludeTags, node.Tags)
                            )
                        {
                            string scope = variable.isGlobal ? "Global" : "Local";
                            string icon = variable.isGlobal ? "VARIABLES/GLOBAL" : "VARIABLES/LOCAL";

                            yield return new SearchResult
                            {
                                Prefix = "[GET]",
                                Name = $"{variable.id}",
                                Path = new string[] { "Variables", scope, "GET", variable.type.ToString() },
                                UserData = tuple,
                                Icon = icon
                            };
                        }
                    }
                }
            }
        }

        public Node Instantiate(SearchResult result)
        {
            Tuple<OverVariableData, NodeReflectionData> data = result.UserData as Tuple<OverVariableData, NodeReflectionData>;
            OverVariableData variable = data.Item1;

            switch (variable.type)
            {
                case OverVariableType.Int:
                    OverGetVariableInt node_i = data.Item2.CreateInstance() as OverGetVariableInt;
                    node_i._id = variable.id;
                    node_i.TypedVariable = variable.integerValue;
                    node_i.Icon = result.Icon;
                    return node_i;
                case OverVariableType.Float:
                    OverGetVariableFloat node_f = data.Item2.CreateInstance() as OverGetVariableFloat;
                    node_f._id = variable.id;
                    node_f.TypedVariable = variable.floatValue;
                    node_f.Icon = result.Icon;
                    return node_f;
                case OverVariableType.Bool:
                    OverGetVariableBool node_b = data.Item2.CreateInstance() as OverGetVariableBool;
                    node_b._id = variable.id;
                    node_b.TypedVariable = variable.boolValue;
                    node_b.Icon = result.Icon;
                    return node_b;
                case OverVariableType.String:
                    OverGetVariableString node_s = data.Item2.CreateInstance() as OverGetVariableString;
                    node_s._id = variable.id;
                    node_s.TypedVariable = variable.stringValue;
                    node_s.Icon = result.Icon;
                    return node_s;
                case OverVariableType.Vector2:
                    OverGetVariableVector2 node_v2 = data.Item2.CreateInstance() as OverGetVariableVector2;
                    node_v2._id = variable.id;
                    node_v2.TypedVariable = variable.vector2Value;
                    node_v2.Icon = result.Icon;
                    return node_v2;
                case OverVariableType.Vector3:
                    OverGetVariableVector3 node_v3 = data.Item2.CreateInstance() as OverGetVariableVector3;
                    node_v3._id = variable.id;
                    node_v3.TypedVariable = variable.vector3Value;
                    node_v3.Icon = result.Icon;
                    return node_v3;
                case OverVariableType.Quaternion:
                    OverGetVariableQuaternion node_q = data.Item2.CreateInstance() as OverGetVariableQuaternion;
                    node_q._id = variable.id;
                    node_q.TypedVariable = variable.QuaternionValue;
                    node_q.Icon = result.Icon;
                    return node_q;
                case OverVariableType.Transform:
                    OverGetVariableTransform node_tr = data.Item2.CreateInstance() as OverGetVariableTransform;
                    node_tr._id = variable.id;
                    node_tr.TypedVariable = variable.transformValue;
                    node_tr.Icon = result.Icon;
                    return node_tr;
                case OverVariableType.RectTransform:
                    OverGetVariableRectTransform node_rtr = data.Item2.CreateInstance() as OverGetVariableRectTransform;
                    node_rtr._id = variable.id;
                    node_rtr.TypedVariable = variable.rectTransform;
                    node_rtr.Icon = result.Icon;
                    return node_rtr;
                case OverVariableType.Rigidbody:
                    OverGetVariableRigidbody node_rb = data.Item2.CreateInstance() as OverGetVariableRigidbody;
                    node_rb._id = variable.id;
                    node_rb.TypedVariable = variable.rigidbodyValue;
                    node_rb.Icon = result.Icon;
                    return node_rb;
                case OverVariableType.Object:
                    OverGetVariableGameObject node_obj = data.Item2.CreateInstance() as OverGetVariableGameObject;
                    node_obj._id = variable.id;
                    node_obj.TypedVariable = variable.gameObject;
                    node_obj.Icon = result.Icon;
                    return node_obj;
                case OverVariableType.Renderer:
                    OverGetVariableRenderer node_renderer = data.Item2.CreateInstance() as OverGetVariableRenderer;
                    node_renderer._id = variable.id;
                    node_renderer.TypedVariable = variable.renderer;
                    node_renderer.Icon = result.Icon;
                    return node_renderer;
                case OverVariableType.LineRenderer:
                    OverGetVariableLineRenderer node_line_renderer = data.Item2.CreateInstance() as OverGetVariableLineRenderer;
                    node_line_renderer._id = variable.id;
                    node_line_renderer.TypedVariable = variable.lineRenderer;
                    node_line_renderer.Icon = result.Icon;
                    return node_line_renderer;
                case OverVariableType.Audio:
                    OverGetVariableAudioSource node_as = data.Item2.CreateInstance() as OverGetVariableAudioSource;
                    node_as._id = variable.id;
                    node_as.TypedVariable = variable.audioSource;
                    node_as.Icon = result.Icon;
                    return node_as;
                case OverVariableType.Video:
                    OverGetVariableVideoPlayer node_vd = data.Item2.CreateInstance() as OverGetVariableVideoPlayer;
                    node_vd._id = variable.id;
                    node_vd.TypedVariable = variable.videoPlayer;
                    node_vd.Icon = result.Icon;
                    return node_vd;
                case OverVariableType.Animator:
                    OverGetVariableAnimator node_anmtr = data.Item2.CreateInstance() as OverGetVariableAnimator;
                    node_anmtr._id = variable.id;
                    node_anmtr.TypedVariable = variable.animator;
                    node_anmtr.Icon = result.Icon;
                    return node_anmtr;
                case OverVariableType.Light:
                    OverGetVariableLight node_lgt = data.Item2.CreateInstance() as OverGetVariableLight;
                    node_lgt._id = variable.id;
                    node_lgt.TypedVariable = variable.light;
                    node_lgt.Icon = result.Icon;
                    return node_lgt;
                //case OverVariableType.Button:
                //    OverGetVariableButton node_button = data.Item2.CreateInstance() as OverGetVariableButton;
                //    node_button._id = variable.id;
                //    node_button.TypedVariable = variable.button;
                //    node_button.Icon = result.Icon;
                //    return node_button;
                case OverVariableType.Text:
                    OverGetVariableText node_text = data.Item2.CreateInstance() as OverGetVariableText;
                    node_text._id = variable.id;
                    node_text.TypedVariable = variable.text;
                    node_text.Icon = result.Icon;
                    return node_text;
                case OverVariableType.TextTMP:
                    OverGetVariableTextTMP node_textTMP = data.Item2.CreateInstance() as OverGetVariableTextTMP;
                    node_textTMP._id = variable.id;
                    node_textTMP.TypedVariable = variable.textTMP;
                    node_textTMP.Icon = result.Icon;
                    return node_textTMP;
                case OverVariableType.Image:
                    OverGetVariableImage node_img = data.Item2.CreateInstance() as OverGetVariableImage;
                    node_img._id = variable.id;
                    node_img.TypedVariable = variable.image;
                    node_img.Icon = result.Icon;
                    return node_img;
                case OverVariableType.RawImage:
                    OverGetVariableRawImage node_rawImg = data.Item2.CreateInstance() as OverGetVariableRawImage;
                    node_rawImg._id = variable.id;
                    node_rawImg.TypedVariable = variable.rawImage;
                    node_rawImg.Icon = result.Icon;
                    return node_rawImg;
                case OverVariableType.Color:
                    OverGetVariableColor node_color = data.Item2.CreateInstance() as OverGetVariableColor;
                    node_color._id = variable.id;
                    node_color.TypedVariable = variable.color;
                    node_color.Icon = result.Icon;
                    return node_color;
                default: return null;
            }
        }

        public bool IsSupported(IGraph graph)
        {
            this.graph = graph as OverGraph;

            if (OverScriptManager.Main != null && !string.IsNullOrEmpty(this.graph.GUID) && OverScriptManager.Main.overDataMappings.ContainsKey(this.graph.GUID))
            {
                return OverScriptManager.Main.overDataMappings[this.graph.GUID].overScript.data.VariableDict.Count > 0 || OverScriptManager.Main.globalVariables.VariableDict.Count > 0;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// Returns true if the intersection between the tags and our allow
        /// list has more than one tag, OR if our allow list is empty.
        /// </summary>
        private bool IsInSupportedTags(IEnumerable<string> supported, IEnumerable<string> tags)
        {
            // If we have no include list, allow anything.
            if (supported.Count() < 1)
            {
                return true;
            }

            // Otherwise - only allow if at least one tag intersects. 
            return supported.Intersect(tags).Count() > 0;
        }

        private bool IsCompatible(Port sourcePort, NodeReflectionData node)
        {
            if (sourcePort == null)
            {
                return true;
            }

            if (sourcePort.Direction == PortDirection.Input)
            {
                return node.HasOutputOfType(sourcePort.Type);
            }

            return node.HasInputOfType(sourcePort.Type);
        }
    }
}