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
            Dictionary<string, OverVariableData> variableDict = new Dictionary<string, OverVariableData>();
            Dictionary<string, OverVariableData> allLocals = new Dictionary<string, OverVariableData>();

            OverScriptManager.Main.UpdateScriptReferences();

            foreach (OverScript script in OverScriptManager.Main.managedScripts)
            {
                List<OverVariableData> variablesList = script.Data.GetVariablesList();
                foreach (var variable in variablesList)
                {
                    if (!variable.isGlobal && !allLocals.ContainsKey(variable.GUID))
                        allLocals.Add(variable.GUID, variable);
                }
            }

            var variablesGraphList = graph.Data.GetVariablesList();
            foreach (OverGraphVariableData item in variablesGraphList)
            {
                if (!item.isGlobal && allLocals.ContainsKey(item.GUID))
                {
                    variableDict[item.GUID] = allLocals[item.GUID];
                }
                else
                {
                    variableDict[item.GUID] = item.ToScriptData();
                }
            }

            List<OverVariableData> globals = OverScriptManager.Main.Data.GetVariablesList();
            foreach (var global in globals)
            {
                if (!variableDict.Keys.Contains(global.GUID))
                {
                    global.isGlobal = true;
                    variableDict.Add(global.GUID, global);
                }
            }

            var variables = variableDict.Values;

            foreach (var variable in variables)
            {
                Type nodeType;

                switch (variable.type)
                {
                    //Getter
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
                    case OverVariableType.Collider: nodeType = typeof(OverGetVariableCollider); break;
                    case OverVariableType.CharacterController: nodeType = typeof(OverGetVariableCharacterController); break;
                    case OverVariableType.Object: nodeType = typeof(OverGetVariableGameObject); break;
                    case OverVariableType.Renderer: nodeType = typeof(OverGetVariableRenderer); break;
                    case OverVariableType.LineRenderer: nodeType = typeof(OverGetVariableLineRenderer); break;
                    case OverVariableType.Material: nodeType = typeof(OverGetVariableMaterial); break;
                    case OverVariableType.ParticleSystem: nodeType = typeof(OverGetVariableParticleSystem); break;
                    case OverVariableType.AudioSource: nodeType = typeof(OverGetVariableAudioSource); break;
                    case OverVariableType.AudioClip: nodeType = typeof(OverGetVariableAudioClip); break;
                    case OverVariableType.Video: nodeType = typeof(OverGetVariableVideoPlayer); break;
                    case OverVariableType.ImageStreamer: nodeType = typeof(OverGetVariableImageStreamer); break;
                    case OverVariableType.Animator: nodeType = typeof(OverGetVariableAnimator); break;
                    case OverVariableType.Light: nodeType = typeof(OverGetVariableLight); break;
                    case OverVariableType.NavMeshAgent: nodeType = typeof(OverGetVariableNavMeshAgent); break;
                    //case OverVariableType.NavMeshObstacle: nodeType = typeof(OverGetVariableNavMeshObstacle); break;
                    case OverVariableType.Text: nodeType = typeof(OverGetVariableText); break;
                    case OverVariableType.TextTMP: nodeType = typeof(OverGetVariableTextTMP); break;
                    case OverVariableType.TextTMP_3D: nodeType = typeof(OverGetVariableTextTMP_3D); break;
                    case OverVariableType.Image: nodeType = typeof(OverGetVariableImage); break;
                    case OverVariableType.RawImage: nodeType = typeof(OverGetVariableRawImage); break;
                    case OverVariableType.Color: nodeType = typeof(OverGetVariableColor); break;
                    case OverVariableType.List: nodeType = typeof(OverGetVariableOverDataList); break;
                    case OverVariableType.JSON: nodeType = typeof(OverGetVariableJSON); break;
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
                            Name = $"{variable.name}",
                            Path = new string[] { "Variables", scope, "GET", variable.type.ToString() },
                            UserData = tuple,
                            Icon = icon
                        };
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
                    node_i.guid = variable.GUID;
                    node_i.sublistIndex = variable.sublistIndex;
                    node_i._name = variable.name;
                    node_i.Icon = result.Icon;
                    node_i.isGlobal = variable.isGlobal;
                    return node_i;
                case OverVariableType.Float:
                    OverGetVariableFloat node_f = data.Item2.CreateInstance() as OverGetVariableFloat;
                    node_f.guid = variable.GUID;
                    node_f.sublistIndex = variable.sublistIndex;
                    node_f._name = variable.name;
                    //node_f.TypedVariable = variable.floatValue;
                    node_f.Icon = result.Icon;
                    node_f.isGlobal = variable.isGlobal;
                    return node_f;
                case OverVariableType.Bool:
                    OverGetVariableBool node_b = data.Item2.CreateInstance() as OverGetVariableBool;
                    node_b.guid = variable.GUID;
                    node_b.sublistIndex = variable.sublistIndex;
                    node_b._name = variable.name;
                    //node_b.TypedVariable = variable.boolValue;
                    node_b.Icon = result.Icon;
                    node_b.isGlobal = variable.isGlobal;
                    return node_b;
                case OverVariableType.String:
                    OverGetVariableString node_s = data.Item2.CreateInstance() as OverGetVariableString;
                    node_s.guid = variable.GUID;
                    node_s.sublistIndex = variable.sublistIndex;
                    node_s._name = variable.name;
                    //node_s.TypedVariable = variable.stringValue;
                    node_s.Icon = result.Icon;
                    node_s.isGlobal = variable.isGlobal;
                    return node_s;
                case OverVariableType.Vector2:
                    OverGetVariableVector2 node_v2 = data.Item2.CreateInstance() as OverGetVariableVector2;
                    node_v2.guid = variable.GUID;
                    node_v2.sublistIndex = variable.sublistIndex;
                    node_v2._name = variable.name;
                    //node_v2.TypedVariable = variable.vector2Value;
                    node_v2.Icon = result.Icon;
                    node_v2.isGlobal = variable.isGlobal;
                    return node_v2;
                case OverVariableType.Vector3:
                    OverGetVariableVector3 node_v3 = data.Item2.CreateInstance() as OverGetVariableVector3;
                    node_v3.guid = variable.GUID;
                    node_v3.sublistIndex = variable.sublistIndex;
                    node_v3._name = variable.name;
                    //node_v3.TypedVariable = variable.vector3Value;
                    node_v3.Icon = result.Icon;
                    node_v3.isGlobal = variable.isGlobal;
                    return node_v3;
                case OverVariableType.Quaternion:
                    OverGetVariableQuaternion node_q = data.Item2.CreateInstance() as OverGetVariableQuaternion;
                    node_q.guid = variable.GUID;
                    node_q.sublistIndex = variable.sublistIndex;
                    node_q._name = variable.name;
                    //node_q.TypedVariable = variable.QuaternionValue;
                    node_q.Icon = result.Icon;
                    node_q.isGlobal = variable.isGlobal;
                    return node_q;
                case OverVariableType.Transform:
                    OverGetVariableTransform node_tr = data.Item2.CreateInstance() as OverGetVariableTransform;
                    node_tr.guid = variable.GUID;
                    node_tr.sublistIndex = variable.sublistIndex;
                    node_tr._name = variable.name;
                    //node_tr.TypedVariable = variable.transformValue;
                    node_tr.Icon = result.Icon;
                    node_tr.isGlobal = variable.isGlobal;
                    return node_tr;
                case OverVariableType.RectTransform:
                    OverGetVariableRectTransform node_rtr = data.Item2.CreateInstance() as OverGetVariableRectTransform;
                    node_rtr.guid = variable.GUID;
                    node_rtr.sublistIndex = variable.sublistIndex;
                    node_rtr._name = variable.name;
                    //node_rtr.TypedVariable = variable.rectTransform;
                    node_rtr.Icon = result.Icon;
                    node_rtr.isGlobal = variable.isGlobal;
                    return node_rtr;
                case OverVariableType.Rigidbody:
                    OverGetVariableRigidbody node_rb = data.Item2.CreateInstance() as OverGetVariableRigidbody;
                    node_rb.guid = variable.GUID;
                    node_rb.sublistIndex = variable.sublistIndex;
                    node_rb._name = variable.name;
                    //node_rb.TypedVariable = variable.rigidbodyValue;
                    node_rb.Icon = result.Icon;
                    node_rb.isGlobal = variable.isGlobal;
                    return node_rb;
                case OverVariableType.Collider:
                    OverGetVariableCollider node_collider = data.Item2.CreateInstance() as OverGetVariableCollider;
                    node_collider.guid = variable.GUID;
                    node_collider.sublistIndex = variable.sublistIndex;
                    node_collider._name = variable.name;
                    node_collider.Icon = result.Icon;
                    node_collider.isGlobal = variable.isGlobal;
                    return node_collider;
                case OverVariableType.CharacterController:
                    OverGetVariableCharacterController node_characterController = data.Item2.CreateInstance() as OverGetVariableCharacterController;
                    node_characterController.guid = variable.GUID;
                    node_characterController.sublistIndex = variable.sublistIndex;
                    node_characterController._name = variable.name;
                    node_characterController.Icon = result.Icon;
                    node_characterController.isGlobal = variable.isGlobal;
                    return node_characterController;
                case OverVariableType.Object:
                    OverGetVariableGameObject node_obj = data.Item2.CreateInstance() as OverGetVariableGameObject;
                    node_obj.guid = variable.GUID;
                    node_obj.sublistIndex = variable.sublistIndex;
                    node_obj._name = variable.name;
                    //node_obj.TypedVariable = variable.gameObject;
                    node_obj.Icon = result.Icon;
                    node_obj.isGlobal = variable.isGlobal;
                    return node_obj;
                case OverVariableType.Renderer:
                    OverGetVariableRenderer node_renderer = data.Item2.CreateInstance() as OverGetVariableRenderer;
                    node_renderer.guid = variable.GUID;
                    node_renderer.sublistIndex = variable.sublistIndex;
                    node_renderer._name = variable.name;
                    //node_renderer.TypedVariable = variable.renderer;
                    node_renderer.Icon = result.Icon;
                    node_renderer.isGlobal = variable.isGlobal;
                    return node_renderer;
                case OverVariableType.LineRenderer:
                    OverGetVariableLineRenderer node_line_renderer = data.Item2.CreateInstance() as OverGetVariableLineRenderer;
                    node_line_renderer.guid = variable.GUID;
                    node_line_renderer.sublistIndex = variable.sublistIndex;
                    node_line_renderer._name = variable.name;
                    //node_line_renderer.TypedVariable = variable.lineRenderer;
                    node_line_renderer.Icon = result.Icon;
                    node_line_renderer.isGlobal = variable.isGlobal;
                    return node_line_renderer;
                case OverVariableType.Material:
                    OverGetVariableMaterial node_material = data.Item2.CreateInstance() as OverGetVariableMaterial;
                    node_material.guid = variable.GUID;
                    node_material.sublistIndex = variable.sublistIndex;
                    node_material._name = variable.name;
                    node_material.Icon = result.Icon;
                    node_material.isGlobal = variable.isGlobal;
                    return node_material;
                case OverVariableType.ParticleSystem:
                    OverGetVariableParticleSystem node_particleSystem = data.Item2.CreateInstance() as OverGetVariableParticleSystem;
                    node_particleSystem.guid = variable.GUID;
                    node_particleSystem.sublistIndex = variable.sublistIndex;
                    node_particleSystem._name = variable.name;
                    node_particleSystem.Icon = result.Icon;
                    node_particleSystem.isGlobal = variable.isGlobal;
                    return node_particleSystem;
                case OverVariableType.AudioSource:
                    OverGetVariableAudioSource node_as = data.Item2.CreateInstance() as OverGetVariableAudioSource;
                    node_as.guid = variable.GUID;
                    node_as.sublistIndex = variable.sublistIndex;
                    node_as._name = variable.name;
                    //node_as.TypedVariable = variable.audioSource;
                    node_as.Icon = result.Icon;
                    node_as.isGlobal = variable.isGlobal;
                    return node_as;
                case OverVariableType.AudioClip:
                    OverGetVariableAudioClip node_ac = data.Item2.CreateInstance() as OverGetVariableAudioClip;
                    node_ac.guid = variable.GUID;
                    node_ac.sublistIndex = variable.sublistIndex;
                    node_ac._name = variable.name;
                    //node_as.TypedVariable = variable.audioSource;
                    node_ac.Icon = result.Icon;
                    node_ac.isGlobal = variable.isGlobal;
                    return node_ac;
                case OverVariableType.Video:
                    OverGetVariableVideoPlayer node_vd = data.Item2.CreateInstance() as OverGetVariableVideoPlayer;
                    node_vd.guid = variable.GUID;
                    node_vd.sublistIndex = variable.sublistIndex;
                    node_vd._name = variable.name;
                    //node_vd.TypedVariable = variable.videoPlayer;
                    node_vd.Icon = result.Icon;
                    node_vd.isGlobal = variable.isGlobal;
                    return node_vd;
                case OverVariableType.ImageStreamer:
                    OverGetVariableImageStreamer node_is = data.Item2.CreateInstance() as OverGetVariableImageStreamer;
                    node_is.guid = variable.GUID;
                    node_is.sublistIndex = variable.sublistIndex;
                    node_is._name = variable.name;
                    node_is.Icon = result.Icon;
                    node_is.isGlobal = variable.isGlobal;
                    return node_is;
                case OverVariableType.Animator:
                    OverGetVariableAnimator node_anmtr = data.Item2.CreateInstance() as OverGetVariableAnimator;
                    node_anmtr.guid = variable.GUID;
                    node_anmtr.sublistIndex = variable.sublistIndex;
                    node_anmtr._name = variable.name;
                    node_anmtr.Icon = result.Icon;
                    node_anmtr.isGlobal = variable.isGlobal;
                    return node_anmtr;
                case OverVariableType.Light:
                    OverGetVariableLight node_lgt = data.Item2.CreateInstance() as OverGetVariableLight;
                    node_lgt.guid = variable.GUID;
                    node_lgt.sublistIndex = variable.sublistIndex;
                    node_lgt._name = variable.name;
                    node_lgt.Icon = result.Icon;
                    node_lgt.isGlobal = variable.isGlobal;
                    return node_lgt;
                case OverVariableType.NavMeshAgent:
                    OverGetVariableNavMeshAgent node_nmagt = data.Item2.CreateInstance() as OverGetVariableNavMeshAgent;
                    node_nmagt.guid = variable.GUID;
                    node_nmagt.sublistIndex = variable.sublistIndex;
                    node_nmagt._name = variable.name;
                    node_nmagt.Icon = result.Icon;
                    node_nmagt.isGlobal = variable.isGlobal;
                    return node_nmagt;
                //case OverVariableType.NavMeshObstacle:
                //    OverGetVariableNavMeshObstacle node_nmobs = data.Item2.CreateInstance() as OverGetVariableNavMeshObstacle;
                //    node_nmobs.guid = variable.GUID;
                //    node_nmobs._name = variable.name;
                //    node_nmobs.Icon = result.Icon;
                //    node_nmobs.isGlobal = variable.isGlobal;
                //    return node_nmobs;
                case OverVariableType.Text:
                    OverGetVariableText node_text = data.Item2.CreateInstance() as OverGetVariableText;
                    node_text.guid = variable.GUID;
                    node_text.sublistIndex = variable.sublistIndex;
                    node_text._name = variable.name;
                    node_text.Icon = result.Icon;
                    node_text.isGlobal = variable.isGlobal;
                    return node_text;
                case OverVariableType.TextTMP:
                    OverGetVariableTextTMP node_textTMP = data.Item2.CreateInstance() as OverGetVariableTextTMP;
                    node_textTMP.guid = variable.GUID;
                    node_textTMP.sublistIndex = variable.sublistIndex;
                    node_textTMP._name = variable.name;
                    node_textTMP.Icon = result.Icon;
                    node_textTMP.isGlobal = variable.isGlobal;
                    return node_textTMP;
                case OverVariableType.TextTMP_3D:
                    OverGetVariableTextTMP_3D node_textTMP_3D = data.Item2.CreateInstance() as OverGetVariableTextTMP_3D;
                    node_textTMP_3D.guid = variable.GUID;
                    node_textTMP_3D.sublistIndex = variable.sublistIndex;
                    node_textTMP_3D._name = variable.name;
                    node_textTMP_3D.Icon = result.Icon;
                    node_textTMP_3D.isGlobal = variable.isGlobal;
                    return node_textTMP_3D;
                case OverVariableType.Image:
                    OverGetVariableImage node_img = data.Item2.CreateInstance() as OverGetVariableImage;
                    node_img.guid = variable.GUID;
                    node_img.sublistIndex = variable.sublistIndex;
                    node_img._name = variable.name;
                    node_img.Icon = result.Icon;
                    node_img.isGlobal = variable.isGlobal;
                    return node_img;
                case OverVariableType.RawImage:
                    OverGetVariableRawImage node_rawImg = data.Item2.CreateInstance() as OverGetVariableRawImage;
                    node_rawImg.guid = variable.GUID;
                    node_rawImg.sublistIndex = variable.sublistIndex;
                    node_rawImg._name = variable.name;
                    node_rawImg.Icon = result.Icon;
                    node_rawImg.isGlobal = variable.isGlobal;
                    return node_rawImg;
                case OverVariableType.Color:
                    OverGetVariableColor node_color = data.Item2.CreateInstance() as OverGetVariableColor;
                    node_color.guid = variable.GUID;
                    node_color.sublistIndex = variable.sublistIndex;
                    node_color._name = variable.name;
                    node_color.Icon = result.Icon;
                    node_color.isGlobal = variable.isGlobal;
                    return node_color;
                case OverVariableType.List:
                    OverGetVariableOverDataList node_list = data.Item2.CreateInstance() as OverGetVariableOverDataList;
                    node_list.guid = variable.GUID;
                    node_list.sublistIndex = variable.sublistIndex;
                    node_list._name = variable.name;
                    node_list.Icon = result.Icon;
                    node_list.isGlobal = variable.isGlobal;
                    return node_list;
                case OverVariableType.JSON:
                    OverGetVariableJSON node_json = data.Item2.CreateInstance() as OverGetVariableJSON;
                    node_json.guid = variable.GUID;
                    node_json.sublistIndex = variable.sublistIndex;
                    node_json._name = variable.name;
                    node_json.Icon = result.Icon;
                    node_json.isGlobal = variable.isGlobal;
                    return node_json;
                default: return null;
            }
        }

        public bool IsSupported(IGraph graph)
        {
            this.graph = graph as OverGraph;

            //if (OverScriptManager.Main != null && !string.IsNullOrEmpty(this.graph.GUID) && OverScriptManager.Main.overDataMappings.ContainsKey(this.graph.GUID))
            //{
            //    return OverScriptManager.Main.overDataMappings[this.graph.GUID].overScript.Data.VariableDict.Count > 0 || OverScriptManager.Main.Data.VariableDict.Count > 0;
            //}
            //else
            //{
            //    return true;
            //}
            return this.graph.Data.TotalVariablesCount > 0 || OverScriptManager.Main.Data.TotalVariablesCount > 0;
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