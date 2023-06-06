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
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OverSDK.VisualScripting
{
    [Node]
    public abstract class OverGetVariable : OverNode
    {
        public string guid;
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
    [Output("Int", typeof(System.Single), Multiple = true)]
    public class OverGetVariableInt : OverGetVariable
    {
        protected System.Single variable; 
        public override OverVariableType Type => OverVariableType.Int;
        public override object OnRequestNodeValue(Port port)
        {
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.integerValue;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.floatValue;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.boolValue;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.stringValue;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.vector2Value;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.vector3Value;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.QuaternionValue;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.transformValue;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.rectTransform;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.rigidbodyValue;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.colliderValue;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.gameObject;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.renderer;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.lineRenderer;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.material;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.particleSystem;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.audioSource;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.audioClip;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.videoPlayer;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.animator;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.light;
            }

            return null;
        }

    }

    [Tags("Variable")]
    [Output("Text", typeof(Text), Multiple = true)]
    public class OverGetVariableText : OverGetVariable
    {
        public Text variable;
        public override OverVariableType Type => OverVariableType.Text;
        public override object OnRequestNodeValue(Port port)
        {
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.text;
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
        public override object OnRequestNodeValue(Port port)
        {
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.textTMP;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.image;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.rawImage;
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
            var variableDict = isGlobal
                ? OverScriptManager.Main.Data.VariableDict
                : OverScriptManager.Main.overDataMappings[sharedContext.scriptGUID].overScript.Data.VariableDict;

            if (variableDict.TryGetValue(guid, out var variableData))
            {
                return variableData?.color;
            }

            return Color.white;
        }
    }
}