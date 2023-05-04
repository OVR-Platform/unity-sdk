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

using BlueGraph;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OverSDK.VisualScripting
{
    [Node]
    public abstract class OverGetVariable : OverNode
    {
        public string _id;
        public abstract OverVariableType Type { get; }

        public abstract object Value { get; set; }

        public override void OnEnable()
        {
            Name = $"{_id}";
            base.OnEnable();
        }
    }

    [Tags("Variable")]
    [Output("Int", typeof(System.Single), Multiple = true)]
    public class OverGetVariableInt : OverGetVariable
    {
        public override OverVariableType Type => OverVariableType.Int;

        protected System.Single variable;
        public virtual int TypedVariable { get => (int)variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (int)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.integerValue;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.integerValue;
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
        public virtual float TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (float)value; }


        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.floatValue;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.floatValue;
            }

            return 0.0f;
        }
    }

    [Tags("Variable")]
    [Output("Bool", typeof(bool), Multiple = true)]
    public class OverGetVariableBool : OverGetVariable
    {
        public bool variable;
        public override OverVariableType Type => OverVariableType.Bool;
        public virtual bool TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (bool)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.boolValue;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.boolValue;
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
        public virtual string TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (string)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.stringValue;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.stringValue;
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
        public virtual Vector2 TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Vector2)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.vector2Value;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.vector2Value;
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
        public virtual Vector3 TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Vector3)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.vector3Value;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.vector3Value;
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
        public virtual Quaternion TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Quaternion)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.QuaternionValue;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.QuaternionValue;
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
        public virtual Transform TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Transform)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.transformValue;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.transformValue;
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
        public virtual RectTransform TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (RectTransform)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.rectTransform;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.rectTransform;
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
        public virtual Rigidbody TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Rigidbody)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.rigidbodyValue;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.rigidbodyValue;
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
        public virtual GameObject TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (GameObject)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.gameObject;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.gameObject;
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
        public virtual Renderer TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Renderer)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.renderer;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.renderer;
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
        public virtual LineRenderer TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (LineRenderer)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.lineRenderer;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.lineRenderer;
            }

            return null;
        }

    }

    [Tags("Variable")]
    [Output("AudioSource", typeof(AudioSource), Multiple = true)]
    public class OverGetVariableAudioSource : OverGetVariable
    {
        public AudioSource variable;
        public override OverVariableType Type => OverVariableType.Audio;
        public virtual AudioSource TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (AudioSource)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.audioSource;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.audioSource;
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
        public virtual VideoPlayer TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (VideoPlayer)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.videoPlayer;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.videoPlayer;
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
        public virtual Animator TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Animator)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.animator;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.animator;
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
        public virtual Light TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Light)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.light;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.light;
            }

            return null;
        }

    }

    //[Tags("Variable")]
    //[Output("Button", typeof(Button), Multiple = true)]
    //public class OverGetVariableButton : OverGetVariable
    //{
    //    public Button variable;
    //    public override OverVariableType Type => OverVariableType.Button;
    //    public virtual Button TypedVariable { get => variable; set => variable = value; }
    //    public override object Value { get => variable; set => variable = (Button)value; }

    //    public override object OnRequestValue(Port port)
    //    {
    //        OverGraph overGraph = Graph as OverGraph;

    //        Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
    //        Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

    //        if (localVariableDict.ContainsKey(_id))
    //        {
    //            OverVariableData data = localVariableDict[_id];
    //            if (data != null)
    //            {
    //                return data.button;
    //            }
    //        }

    //        if (globalVariableDict.ContainsKey(_id))
    //        {
    //            OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
    //            if (global_data != null)
    //                return global_data.button;
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
        public virtual Text TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Text)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.text;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.text;
            }

            return null;
        }

    }

    [Tags("Variable")]
    [Output("Text (TMP)", typeof(TMPro.TextMeshProUGUI), Multiple = true)]
    public class OverGetVariableTextTMP : OverGetVariable
    {
        public TMPro.TextMeshProUGUI variable;
        public override OverVariableType Type => OverVariableType.TextTMP;
        public virtual TMPro.TextMeshProUGUI TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (TMPro.TextMeshProUGUI)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.textTMP;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.textTMP;
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
        public virtual Image TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Image)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.image;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.image;
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
        public virtual RawImage TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (RawImage)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.rawImage;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.rawImage;
            }

            return null;
        }

    }

    [Tags("Variable")]
    [Output("Color", typeof(Color), Multiple = true)]
    public class OverGetVariableColor : OverGetVariable
    {
        public override OverVariableType Type => OverVariableType.Color;

        protected Color variable;
        public virtual Color TypedVariable { get => variable; set => variable = value; }
        public override object Value { get => variable; set => variable = (Color)value; }

        public override object OnRequestValue(Port port)
        {
            OverGraph overGraph = Graph as OverGraph;

            Dictionary<string, OverVariableData> localVariableDict = OverScriptManager.Main.overDataMappings[overGraph.GUID].overScript.data.VariableDict;
            Dictionary<string, OverVariableData> globalVariableDict = OverScriptManager.Main.globalVariables.VariableDict;

            if (localVariableDict.ContainsKey(_id))
            {
                OverVariableData data = localVariableDict[_id];
                if (data != null)
                {
                    return data.color;
                }
            }

            if (globalVariableDict.ContainsKey(_id))
            {
                OverVariableData global_data = OverScriptManager.Main.globalVariables.VariableDict[_id];
                if (global_data != null)
                    return global_data.color;
            }

            return Color.white;
        }
    }
}