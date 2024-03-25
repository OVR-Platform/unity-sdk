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

using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OverSDK.VisualScripting.Editor
{
    [CustomPropertyDrawer(typeof(OverListDataListAttribute))]
    public class OverListDataListDrawer : PropertyDrawer
    {
        float top = 5;
        float bottom = 5;
        //float spacing = 5;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float heigth = top + EditorGUIUtility.singleLineHeight + bottom;
            return heigth;
        }


        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            float x = rect.x;
            float y = rect.y;

            float width = rect.width;
            float height = EditorGUIUtility.singleLineHeight;

            object obj = property.GetTargetObjectOfProperty();
            OverListData val = obj as OverListData;
            if(val != null)
            {
                EditorGUI.BeginChangeCheck();
                {
                    OverList overList = property.serializedObject.targetObject as OverList;

                    if (overList != null)
                    {
                        val.type = (OverListDataType)overList.Type;
                    }

                    //string fieldLabel = "value:";
                    string fieldLabel = string.Empty;
                    switch (val.type)
                    {
                        //simple
                        case OverListDataType.Bool:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.boolValue = EditorGUI.Toggle(new Rect(x, y, width, height), fieldLabel, val.boolValue);
                            break;
                        case OverListDataType.String:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.stringValue = EditorGUI.TextField(new Rect(x, y, width, height), fieldLabel, val.stringValue);
                            break;
                        case OverListDataType.Float:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.floatValue = EditorGUI.FloatField(new Rect(x, y, width, height), fieldLabel, val.floatValue, new GUIStyle(GUI.skin.textField) { alignment = TextAnchor.MiddleRight });
                            break;
                        case OverListDataType.Int:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.integerValue = EditorGUI.IntField(new Rect(x, y, width, height), fieldLabel, val.integerValue, new GUIStyle(GUI.skin.textField) { alignment = TextAnchor.MiddleRight });
                            break;
                        case OverListDataType.Vector2:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.vector2Value = EditorGUI.Vector2Field(new Rect(x, y, width, height), fieldLabel, val.vector2Value);
                            break;
                        case OverListDataType.Vector3:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.vector3Value = EditorGUI.Vector3Field(new Rect(x, y, width, height), fieldLabel, val.vector3Value);
                            break;
                        case OverListDataType.Quaternion:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            Vector4 vector = new Vector4(val.QuaternionValue.x, val.QuaternionValue.y, val.QuaternionValue.z, val.QuaternionValue.w);
                            Vector4 temp_vector = EditorGUI.Vector4Field(new Rect(x, y, width, height), fieldLabel, vector);
                            if (vector != temp_vector)
                            {
                                vector = temp_vector;
                            }
                            val.QuaternionValue = new Quaternion(vector.x, vector.y, vector.z, vector.w);
                            break;
                        // Mono
                        case OverListDataType.Transform:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.transformValue = (Transform)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.transformValue, typeof(Transform), true);
                            break;
                        case OverListDataType.RectTransform:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.rectTransform = (RectTransform)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.rectTransform, typeof(RectTransform), true);
                            break;
                        case OverListDataType.Rigidbody:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.rigidbodyValue = (Rigidbody)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.rigidbodyValue, typeof(Rigidbody), true);
                            break;
                        case OverListDataType.Collider:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.colliderValue = (Collider)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.colliderValue, typeof(Collider), true);
                            break;
                        case OverListDataType.Object:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.gameObject = (GameObject)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.gameObject, typeof(GameObject), true);
                            break;
                        case OverListDataType.Renderer:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.renderer = (Renderer)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.renderer, typeof(Renderer), true);
                            break;
                        case OverListDataType.LineRenderer:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.lineRenderer = (LineRenderer)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.lineRenderer, typeof(LineRenderer), true);
                            break;
                        case OverListDataType.Material:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.material = (Material)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.material, typeof(Material), true);
                            break;
                        case OverListDataType.ParticleSystem:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.particleSystem = (ParticleSystem)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.particleSystem, typeof(ParticleSystem), true);
                            break;
                        case OverListDataType.AudioSource:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.audioSource = (AudioSource)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.audioSource, typeof(AudioSource), true);
                            break;
                        case OverListDataType.AudioClip:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.audioClip = (AudioClip)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.audioClip, typeof(AudioClip), true);
                            break;
                        case OverListDataType.Video:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.videoPlayer = (VideoPlayer)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.videoPlayer, typeof(VideoPlayer), true);
                            break;
                        case OverListDataType.Animator:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.animator = (Animator)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.animator, typeof(Animator), true);
                            break;
                        case OverListDataType.Light:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.light = (Light)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.light, typeof(Light), true);
                            break;
                        case OverListDataType.Text:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.text = (Text)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.text, typeof(Text), true);
                            break;
                        case OverListDataType.TextTMP:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.textTMP = (TextMeshProUGUI)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.textTMP, typeof(TextMeshProUGUI), true);
                            break;
                        case OverListDataType.TextTMP_3D:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.textTMP_3D = (TextMeshPro)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.textTMP_3D, typeof(TextMeshPro), true);
                            break;
                        case OverListDataType.Image:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.image = (Image)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.image, typeof(Image), true);
                            break;
                        case OverListDataType.RawImage:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.rawImage = (RawImage)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.rawImage, typeof(RawImage), true);
                            break;
                        case OverListDataType.Color:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            label.text = fieldLabel;
                            val.color = EditorGUI.ColorField(new Rect(x, y, width, height), label, val.color, true, true, false);
                            break;
                        case OverListDataType.List:
                            //y += EditorGUIUtility.singleLineHeight + spacing;
                            val.list = (OverList)EditorGUI.ObjectField(new Rect(x, y, width, height), fieldLabel, val.list, typeof(OverList), true);
                            break;
                        default: break;
                    }
                }

                if (EditorGUI.EndChangeCheck())
                {
                    property.serializedObject.ApplyModifiedProperties();
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }
            }
        }
    }
}

