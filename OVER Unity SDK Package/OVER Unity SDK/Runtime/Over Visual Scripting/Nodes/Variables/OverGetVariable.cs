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
using OverSimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OverSDK.VisualScripting
{
    [Node]
    public abstract class OverGetVariable : OverNode
    {
        public string guid;

        public int sublistIndex;

        public string _name;

        public bool isGlobal;

        public abstract OverVariableType Type { get; }

        public override void OnEnable()
        {
            Name = $"{_name}";
            base.OnEnable();
        }
    }

    [Tags("Variable")]
    [Output("Int", typeof(int), Multiple = true)]
    public class OverGetVariableInt : OverGetVariable
    {
        protected int variable;
        public override OverVariableType Type => OverVariableType.Int;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.integerValue;
            }

            return 0;
        }
    }

    [Tags("Variable")]
    [Output("Float", typeof(float), Multiple = true)]
    public class OverGetVariableFloat : OverGetVariable
    {
        protected float variable;
        public override OverVariableType Type => OverVariableType.Float;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.floatValue;
            }

            return 0f;
        }
    }

    [Tags("Variable")]
    [Output("Bool", typeof(bool), Multiple = true)]
    public class OverGetVariableBool : OverGetVariable
    {
        public bool variable;
        public override OverVariableType Type => OverVariableType.Bool;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.boolValue;
            }

            return false;
        }
    }

    [Tags("Variable")]
    [Output("String", typeof(string), Multiple = true)]
    public class OverGetVariableString : OverGetVariable
    {
        public string variable;
        public override OverVariableType Type => OverVariableType.String;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.stringValue;
            }

            return "";
        }
    }

    [Tags("Variable")]
    [Output("Vector 2D", typeof(Vector2), Multiple = true)]
    public class OverGetVariableVector2 : OverGetVariable
    {
        public Vector2 variable;
        public override OverVariableType Type => OverVariableType.Vector2;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.vector2Value;
            }

            return Vector2.zero;
        }
    }

    [Tags("Variable")]
    [Output("Vector 3D", typeof(Vector3), Multiple = true)]
    public class OverGetVariableVector3 : OverGetVariable
    {
        public Vector3 variable;
        public override OverVariableType Type => OverVariableType.Vector3;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.vector3Value;
            }

            return Vector3.zero;
        }
    }

    [Tags("Variable")]
    [Output("Quaternion", typeof(Quaternion), Multiple = true)]
    public class OverGetVariableQuaternion : OverGetVariable
    {
        public Quaternion variable;
        public override OverVariableType Type => OverVariableType.Quaternion;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.QuaternionValue;
            }

            return Quaternion.identity;
        }
    }


    [Tags("Variable")]
    [Output("Transform", typeof(Transform), Multiple = true)]
    public class OverGetVariableTransform : OverGetVariable
    {
        public Transform variable;
        public override OverVariableType Type => OverVariableType.Transform;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.transformValue;
            }

            return null;
        }

    }


    [Tags("Variable")]
    [Output("RectTransform", typeof(RectTransform), Multiple = true)]
    public class OverGetVariableRectTransform : OverGetVariable
    {
        public RectTransform variable;
        public override OverVariableType Type => OverVariableType.RectTransform;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.rectTransform;
            }

            return null;
        }

    }

    [Tags("Variable")]
    [Output("Rigidbody", typeof(Rigidbody), Multiple = true)]
    public class OverGetVariableRigidbody : OverGetVariable
    {
        public Rigidbody variable;
        public override OverVariableType Type => OverVariableType.Rigidbody;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.rigidbodyValue;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("Collider", typeof(Collider), Multiple = true)]
    public class OverGetVariableCollider : OverGetVariable
    {
        public Collider variable;
        public override OverVariableType Type => OverVariableType.Collider;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.colliderValue;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("CharacterController", typeof(CharacterController), Multiple = true)]
    public class OverGetVariableCharacterController : OverGetVariable
    {
        public CharacterController variable;
        public override OverVariableType Type => OverVariableType.CharacterController;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.characterController;
            }

            return null;
        }
    }


    [Tags("Variable")]
    [Output("GameObject", typeof(GameObject), Multiple = true)]
    public class OverGetVariableGameObject : OverGetVariable
    {
        public GameObject variable;
        public override OverVariableType Type => OverVariableType.Object;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.gameObject;
            }

            return null;
        }

    }


    [Tags("Variable")]
    [Output("Renderer", typeof(Renderer), Multiple = true)]
    public class OverGetVariableRenderer : OverGetVariable
    {
        public Renderer variable;
        public override OverVariableType Type => OverVariableType.Renderer;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.renderer;
            }

            return null;
        }

    }


    [Tags("Variable")]
    [Output("LineRenderer", typeof(LineRenderer), Multiple = true)]
    public class OverGetVariableLineRenderer : OverGetVariable
    {
        public LineRenderer variable;
        public override OverVariableType Type => OverVariableType.LineRenderer;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.lineRenderer;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("Material", typeof(Material), Multiple = true)]
    public class OverGetVariableMaterial : OverGetVariable
    {
        public Material variable;
        public override OverVariableType Type => OverVariableType.Material;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.material;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("ParticleSystem", typeof(ParticleSystem), Multiple = true)]
    public class OverGetVariableParticleSystem : OverGetVariable
    {
        public ParticleSystem variable;
        public override OverVariableType Type => OverVariableType.ParticleSystem;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.particleSystem;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("AudioSource", typeof(AudioSource), Multiple = true)]
    public class OverGetVariableAudioSource : OverGetVariable
    {
        public AudioSource variable;
        public override OverVariableType Type => OverVariableType.AudioSource;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.audioSource;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("AudioClip", typeof(AudioClip), Multiple = true)]
    public class OverGetVariableAudioClip : OverGetVariable
    {
        public AudioClip variable;
        public override OverVariableType Type => OverVariableType.AudioClip;

        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.audioClip;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("VideoPlayer", typeof(VideoPlayer), Multiple = true)]
    public class OverGetVariableVideoPlayer : OverGetVariable
    {
        public VideoPlayer variable;
        public override OverVariableType Type => OverVariableType.Video;

        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.videoPlayer;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("ImageStreamer", typeof(ImageStreamer), Multiple = true)]
    public class OverGetVariableImageStreamer : OverGetVariable
    {
        public ImageStreamer variable;
        public override OverVariableType Type => OverVariableType.ImageStreamer;

        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.imageStreamer;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("Animator", typeof(Animator), Multiple = true)]
    public class OverGetVariableAnimator : OverGetVariable
    {
        public Animator variable;
        public override OverVariableType Type => OverVariableType.Animator;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.animator;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("Light", typeof(Light), Multiple = true)]
    public class OverGetVariableLight : OverGetVariable
    {
        public Light variable;
        public override OverVariableType Type => OverVariableType.Light;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.light;
            }

            return null;
        }

    }

    [Tags("Variable")]
    [Output("NavMeshAgent", typeof(NavMeshAgent), Multiple = true)]
    public class OverGetVariableNavMeshAgent : OverGetVariable
    {
        public NavMeshAgent variable;
        public override OverVariableType Type => OverVariableType.NavMeshAgent;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.navMeshAgent;
            }

            return null;
        }

    }

    //[Tags("Variable")]
    //[Output("NavMeshObstacle", typeof(NavMeshObstacle), Multiple = true)]
    //public class OverGetVariableNavMeshObstacle : OverGetVariable
    //{
    //    public NavMeshAgent variable;
    //    public override OverVariableType Type => OverVariableType.NavMeshObstacle;
    //    public override object OnRequestNodeValue(Port port)
    //    {
    //        Dictionary<string, OverVariableData> variableDict = new Dictionary<string, OverVariableData>();
    //        if (isGlobal)
    //        {
    //            variableDict = OverScriptManager.Main.Data.VariableDict;
    //        }
    //        else
    //        {
    //            if (!string.IsNullOrEmpty(sharedContext.scriptGUID))
    //            {
    //                variableDict = OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;
    //            }
    //            else
    //            {
    //                Debug.LogWarning($"ATTENTION! Application is not in PLAY Mode, the values you see on screen may be incorrectly calculated.");
    //                OverScript script = OverScriptManager.Main.managedScripts.Where(x => x.Data.VariableDict.ContainsKey(guid)).FirstOrDefault();
    //                variableDict = script.Data.VariableDict;
    //            }
    //        }

    //        if (variableDict.TryGetValue(guid, out var variableData))
    //        {
    //            return variableData?.navMeshObstacle;
    //        }

    //        return null;
    //    }

    //}

    [Tags("Variable")]
    [Output("Text", typeof(Text), Multiple = true)]
    public class OverGetVariableText : OverGetVariable
    {
        public Text variable;
        public override OverVariableType Type => OverVariableType.Text;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.text;
            }

            return null;
        }

    }

    [Tags("Variable")]
    [Output("Text (TMP UGUI)", typeof(TMPro.TextMeshProUGUI), Multiple = true)]
    public class OverGetVariableTextTMP : OverGetVariable
    {
        public TMPro.TextMeshProUGUI variable;
        public override OverVariableType Type => OverVariableType.TextTMP;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.textTMP;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("Text (TMP)", typeof(TMPro.TextMeshPro), Multiple = true)]
    public class OverGetVariableTextTMP_3D : OverGetVariable
    {
        public TMPro.TextMeshPro variable;
        public override OverVariableType Type => OverVariableType.TextTMP_3D;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.textTMP_3D;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("Image", typeof(Image), Multiple = true)]
    public class OverGetVariableImage : OverGetVariable
    {
        public Image variable;
        public override OverVariableType Type => OverVariableType.Image;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.image;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("RawImage", typeof(RawImage), Multiple = true)]
    public class OverGetVariableRawImage : OverGetVariable
    {
        public RawImage variable;
        public override OverVariableType Type => OverVariableType.RawImage;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.rawImage;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("Color", typeof(Color), Multiple = true)]
    public class OverGetVariableColor : OverGetVariable
    {
        protected Color variable;
        public override OverVariableType Type => OverVariableType.Color;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.color;
            }

            return Color.white;
        }
    }

    [Tags("Variable")]
    [Output("List", typeof(IList), Multiple = true)]
    public class OverGetVariableOverDataList : OverGetVariable
    {
        protected OverList variable;
        public override OverVariableType Type => OverVariableType.List;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return CreateListFromOverListData(varaibleFound.list);
            }

            return null;
        }

        private IList CreateListFromOverListData(OverList overList)
        {
            IList result = OverList.GetList(OverList.ResolveType(overList));
            foreach (OverListData element in overList.Elements)
            {
                object item = element.GetValue(overList.Type);
                if (item != null)
                {
                    if (item.GetType() == typeof(OverList))
                    {
                        // Recursively create nested lists
                        item = CreateListFromOverListData(item as OverList);
                    }

                    result.Add(item);
                }
            }

            return result;
        }
    }

    [Tags("Variable")]
    [Output("OverList", typeof(OverList), Multiple = true)]
    public class OverGetVariableOverDataOver : OverGetVariable
    {
        public override OverVariableType Type => OverVariableType.List;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                return varaibleFound.list;
            }

            return null;
        }
    }

    [Tags("Variable")]
    [Output("JSON", typeof(JSONNode), Multiple = true)]
    public class OverGetVariableJSON : OverGetVariable
    {
        protected JSONNode variable;
        public override OverVariableType Type => OverVariableType.JSON;
        public override object OnRequestNodeValue(Port port)
        {
            OverScriptData data = isGlobal ?
                                  OverScriptManager.Main.Data :
                                  OverScriptManager.Main.overScriptsReferences[sharedContext.scriptGUID].overScript.Data;

            if (data.TryGetVariable(guid, out OverVariableData varaibleFound))
            {
                string jsonData = varaibleFound.json;

                if (varaibleFound != null &&
                    !string.IsNullOrEmpty(jsonData) &&
                    !string.IsNullOrWhiteSpace(jsonData))
                {
                    return JSONNode.Parse(jsonData);
                }
            }

            return JSONNode.Parse("{}");
        }
    }
}