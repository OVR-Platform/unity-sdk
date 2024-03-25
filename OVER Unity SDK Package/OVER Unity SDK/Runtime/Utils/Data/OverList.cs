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

using OverSDK.VisualScripting;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OverSDK
{
    [Serializable]
    public class OverListData
    {
        public OverListDataType type;

        public int integerValue;
        public float floatValue;
        public string stringValue;
        public bool boolValue;

        public Vector2 vector2Value;
        public Vector3 vector3Value;
        public Quaternion QuaternionValue;

        public Transform transformValue;
        public GameObject gameObject;
        public Renderer renderer;
        public RectTransform rectTransform;
        public LineRenderer lineRenderer;

        public Material material;
        public ParticleSystem particleSystem;

        public Rigidbody rigidbodyValue;
        public Collider colliderValue;

        public Light light;

        public AudioSource audioSource;
        public AudioClip audioClip;
        public VideoPlayer videoPlayer;
        public Animator animator;

        public Text text;
        public TMPro.TextMeshProUGUI textTMP;
        public TMPro.TextMeshPro textTMP_3D;

        public Image image;
        public RawImage rawImage;
        public Color color;
        public OverList list;

        public object GetValue(OverListDataType type)
        {
            switch (type)
            {
                case OverListDataType.Int: return integerValue;
                case OverListDataType.Float: return floatValue;
                case OverListDataType.Bool: return boolValue;
                case OverListDataType.Vector2: return vector2Value;
                case OverListDataType.String: return stringValue;
                case OverListDataType.Vector3: return vector3Value;
                case OverListDataType.Quaternion: return QuaternionValue;
                case OverListDataType.Transform: return transformValue;
                case OverListDataType.Object: return gameObject;
                case OverListDataType.Rigidbody: return rigidbodyValue;
                case OverListDataType.RectTransform: return rectTransform;
                case OverListDataType.LineRenderer: return lineRenderer;
                case OverListDataType.Material: return material;
                case OverListDataType.ParticleSystem: return particleSystem;
                case OverListDataType.Collider: return colliderValue;
                case OverListDataType.Light: return light;
                case OverListDataType.AudioClip: return audioClip;
                case OverListDataType.AudioSource: return audioSource;
                case OverListDataType.Video: return videoPlayer;
                case OverListDataType.Animator: return animator;
                case OverListDataType.Text: return text;
                case OverListDataType.TextTMP: return textTMP;
                case OverListDataType.TextTMP_3D: return textTMP_3D;
                case OverListDataType.Image: return image;
                case OverListDataType.RawImage: return rawImage;
                case OverListDataType.Color: return color;
                case OverListDataType.List: return list;

                default: return null;
            }
        }

        public void SetValue(object value)
        {
            switch (type)
            {
                case OverListDataType.Int: integerValue = (int)value; break;
                case OverListDataType.Float: floatValue = (float)value; break;
                case OverListDataType.Bool: boolValue = (bool)value; break;
                case OverListDataType.Vector2: vector2Value = (Vector2)value; break;
                case OverListDataType.Vector3: vector3Value = (Vector3)value; break;
                case OverListDataType.String: stringValue = (string)value; break;
                case OverListDataType.Quaternion: QuaternionValue = (Quaternion)value; break;
                case OverListDataType.Transform: transformValue = (Transform)value; break;
                case OverListDataType.RectTransform: rectTransform = (RectTransform)value; break;
                case OverListDataType.Object: gameObject = (GameObject)value; break;
                case OverListDataType.Rigidbody: rigidbodyValue = (Rigidbody)value; break;
                case OverListDataType.LineRenderer: lineRenderer = (LineRenderer)value; break;
                case OverListDataType.Material: material = (Material)value; break;
                case OverListDataType.ParticleSystem: particleSystem = (ParticleSystem)value; break;
                case OverListDataType.Collider: colliderValue = (Collider)value; break;
                case OverListDataType.Light: light = (Light)value; break;
                case OverListDataType.AudioClip: audioClip = (AudioClip)value; break;
                case OverListDataType.AudioSource: audioSource = (AudioSource)value; break;
                case OverListDataType.Video: videoPlayer = (VideoPlayer)value; break;
                case OverListDataType.Animator: animator = (Animator)value; break;
                case OverListDataType.Text: text = (Text)value; break;
                case OverListDataType.TextTMP: textTMP = (TextMeshProUGUI)value; break;
                case OverListDataType.TextTMP_3D: textTMP_3D = (TextMeshPro)value; break;
                case OverListDataType.Image: image = (Image)value; break;
                case OverListDataType.RawImage: rawImage = (RawImage)value; break;
                case OverListDataType.Color: color = (Color)value; break;
                case OverListDataType.List: list = (OverList)value; break;

                default: break;
            }
        }

        public OverListData Clone()
        {
            return new OverListData()
            {
                type = type,
                integerValue = integerValue,
                floatValue = floatValue,
                stringValue = stringValue,
                boolValue = boolValue,
                vector2Value = vector2Value,
                vector3Value = vector3Value,
                QuaternionValue = QuaternionValue,
                transformValue = transformValue,
                gameObject = gameObject,
                renderer = renderer,
                rectTransform = rectTransform,
                lineRenderer = lineRenderer,
                material = material,
                particleSystem = particleSystem,

                audioSource = audioSource,
                audioClip = audioClip,
                videoPlayer = videoPlayer,
                animator = animator,

                rigidbodyValue = rigidbodyValue,
                colliderValue = colliderValue,
                light = light,

                text = text,
                textTMP = textTMP,
                textTMP_3D = textTMP_3D,
                image = image,
                rawImage = rawImage,
                color = color,
                list = list
            };
        }
    }

    public class OverList : MonoBehaviour
    {
        [SerializeField] OverListDataType _type;
        public OverListDataType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [SerializeField][HideInInspector] protected OverListDataType oldType;

        [SerializeField, OverListDataList] List<OverListData> elements = new List<OverListData>();

        public List<OverListData> Elements { get { return elements; } set { elements = value; } }

        //Mono
        
        private void OnValidate()
        {
            if(Type != oldType)
            {
                oldType = Type;
                Elements.Clear();
            }
        }

        //Inner

        public static Type ResolveType (OverList list)
        {
            switch (list.Type)
            {
                case OverListDataType.Bool: return typeof(bool);
                case OverListDataType.Int: return typeof(int);
                case OverListDataType.Float: return typeof(float);
                case OverListDataType.Vector2: return typeof(Vector2);
                case OverListDataType.Vector3: return typeof(Vector3);
                case OverListDataType.Quaternion: return typeof(Quaternion);
                case OverListDataType.String: return typeof(string);
                case OverListDataType.Color: return typeof(Color);

                case OverListDataType.Transform: return typeof(Transform);
                case OverListDataType.Object: return typeof(GameObject);
                case OverListDataType.Renderer: return typeof(Renderer);
                case OverListDataType.LineRenderer: return typeof(LineRenderer);
                case OverListDataType.Material: return typeof(Material);
                case OverListDataType.ParticleSystem: return typeof(ParticleSystem);
                case OverListDataType.Rigidbody: return typeof(Rigidbody);
                case OverListDataType.Collider: return typeof(Collider);
                case OverListDataType.Light: return typeof(Light);
                case OverListDataType.AudioSource: return typeof(AudioSource);
                case OverListDataType.AudioClip: return typeof(AudioClip);
                case OverListDataType.Animator: return typeof(Animator);
                case OverListDataType.Text: return typeof(Text);
                case OverListDataType.TextTMP: return typeof(TextMeshProUGUI);
                case OverListDataType.TextTMP_3D: return typeof(TextMeshPro);
                case OverListDataType.Image: return typeof(Image);
                case OverListDataType.RawImage: return typeof(RawImage);
                case OverListDataType.List: return typeof(IList);

                //lista?????

                default: return typeof(object);
            }
        }

        public static IList GetList(Type type)
        {
            return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
        }
    }
}