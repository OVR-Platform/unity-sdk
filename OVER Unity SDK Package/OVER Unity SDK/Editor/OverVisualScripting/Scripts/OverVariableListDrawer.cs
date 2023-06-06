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

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

namespace OverSDK.VisualScripting.Editor
{
    [CustomPropertyDrawer(typeof(OverVariableListAttribute))]
    public class OverVariableListDrawer : PropertyDrawer
    {
        float top = 5;
        float bottom = 5;
        float spacing = 5;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float heigth = top + EditorGUIUtility.singleLineHeight + bottom;

            object obj = property.GetTargetObjectOfProperty();
            OverVariableData val = obj as OverVariableData;

            //guid
            heigth += EditorGUIUtility.singleLineHeight + spacing;

            if (val.type != OverVariableType.None)
                heigth += EditorGUIUtility.singleLineHeight + spacing;
            return heigth;
        }

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            float x = rect.x;
            float y = rect.y;

            float width = rect.width;
            float height = EditorGUIUtility.singleLineHeight;

            object obj = property.GetTargetObjectOfProperty();
            OverVariableData val = obj as OverVariableData;

            if (!string.IsNullOrEmpty(val.GUID))
            {
                EditorGUI.LabelField(new Rect(x, y, width, height), val.GUID);
                y += EditorGUIUtility.singleLineHeight + spacing;
            }

            OverScript overscript = property.serializedObject.targetObject as OverScript;
            if (overscript != null)
            {
                string newVariableName = EditorGUI.TextField(new Rect(x, y, width / 2, height), val.name);
                if (val.name != newVariableName)
                {          
                    val.name = newVariableName;
                    overscript.ApplyVariableChangesTo(val, overscript.OverGraph);
                }

                OverVariableType newVariableType = (OverVariableType)EditorGUI.EnumPopup(new Rect(x + width / 2, y, width / 2, height), val.type);
                if(val.type != newVariableType)
                {
                    val.type = newVariableType;
                    overscript.ApplyVariableChangesTo(val, overscript.OverGraph);
                }
            }
            else
            {
                string newVariableName = EditorGUI.TextField(new Rect(x, y, width / 2, height), val.name);
                if (val.name != newVariableName)
                {
                    val.name = newVariableName;
                    OverScriptManager.Main.ApplyVariableChangesTo(val);
                }

                OverVariableType newVariableType = (OverVariableType)EditorGUI.EnumPopup(new Rect(x + width / 2, y, width / 2, height), val.type);
                if (val.type != newVariableType)
                {
                    val.type = newVariableType;
                    OverScriptManager.Main.ApplyVariableChangesTo(val);
                }
            }

            switch (val.type)
            {
                //simple
                case OverVariableType.Bool:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.boolValue = EditorGUI.Toggle(new Rect(x, y, width, height), "value:", val.boolValue);
                    break;
                case OverVariableType.String:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.stringValue = EditorGUI.TextField(new Rect(x, y, width, height), "value:", val.stringValue);
                    break;
                case OverVariableType.Float:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.floatValue = EditorGUI.FloatField(new Rect(x, y, width, height), "value:", val.floatValue, new GUIStyle(GUI.skin.textField) { alignment = TextAnchor.MiddleRight });
                    break;
                case OverVariableType.Int:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.integerValue = EditorGUI.IntField(new Rect(x, y, width, height), "value:", val.integerValue, new GUIStyle(GUI.skin.textField) { alignment = TextAnchor.MiddleRight });
                    break;
                case OverVariableType.Vector2:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.vector2Value = EditorGUI.Vector2Field(new Rect(x, y, width, height), "value:", val.vector2Value);
                    break;
                case OverVariableType.Vector3:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.vector3Value = EditorGUI.Vector3Field(new Rect(x, y, width, height), "value:", val.vector3Value);
                    break;
                case OverVariableType.Quaternion:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Vector4 vector = new Vector4(val.QuaternionValue.x, val.QuaternionValue.y, val.QuaternionValue.z, val.QuaternionValue.w);
                    Vector4 temp_vector = EditorGUI.Vector4Field(new Rect(x, y, width, height), "value:", vector);
                    if (vector != temp_vector)
                    {
                        vector = temp_vector;
                    }
                    val.QuaternionValue = new Quaternion(vector.x, vector.y, vector.z, vector.w);
                    break;
                // Mono
                case OverVariableType.Transform:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.transformValue = (Transform)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.transformValue, typeof(Transform), true);
                    break;
                case OverVariableType.RectTransform:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.rectTransform = (RectTransform)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.rectTransform, typeof(RectTransform), true);
                    break;
                case OverVariableType.Rigidbody:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.rigidbodyValue = (Rigidbody)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.rigidbodyValue, typeof(Rigidbody), true);
                    break;
                case OverVariableType.Collider:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.colliderValue = (Collider)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.colliderValue, typeof(Collider), true);
                    break;
                case OverVariableType.Object:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.gameObject = (GameObject)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.gameObject, typeof(GameObject), true);
                    break;
                case OverVariableType.Renderer:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.renderer = (Renderer)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.renderer, typeof(Renderer), true);
                    break;
                case OverVariableType.LineRenderer:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.lineRenderer = (LineRenderer)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.lineRenderer, typeof(LineRenderer), true);
                    break;
                case OverVariableType.Material:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.material = (Material)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.material, typeof(Material), true);
                    break;
                case OverVariableType.ParticleSystem:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.particleSystem = (ParticleSystem)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.particleSystem, typeof(ParticleSystem), true);
                    break;
                case OverVariableType.AudioSource:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.audioSource = (AudioSource)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.audioSource, typeof(AudioSource), true);
                    break;
                case OverVariableType.AudioClip:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.audioClip = (AudioClip)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.audioClip, typeof(AudioClip), true);
                    break;
                case OverVariableType.Video:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.videoPlayer = (VideoPlayer)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.videoPlayer, typeof(VideoPlayer), true);
                    break;
                case OverVariableType.Animator:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.animator = (Animator)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.animator, typeof(Animator), true);
                    break;
                case OverVariableType.Light:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.light = (Light)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.light, typeof(Light), true);
                    break;
                case OverVariableType.Text:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.text = (Text)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.text, typeof(Text), true);
                    break;
                case OverVariableType.TextTMP:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.textTMP = (TextMeshProUGUI)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.textTMP, typeof(TextMeshProUGUI), true);
                    break;
                case OverVariableType.Image:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.image = (Image)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.image, typeof(Image), true);
                    break;
                case OverVariableType.RawImage:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    val.rawImage = (RawImage)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.rawImage, typeof(RawImage), true);
                    break;
                case OverVariableType.Color:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    label.text = "value:";
                    val.color = EditorGUI.ColorField(new Rect(x, y, width, height), label, val.color, true, true, false);
                    break;
                default: break;
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }

            if (property.serializedObject.ApplyModifiedProperties() || (Event.current.type == EventType.ValidateCommand && Event.current.commandName == "UndoRedoPerformed"))
            {
                if(overscript != null)
                {
                    overscript.ApplyVariableChangesTo(val, overscript.OverGraph);
                }
                else
                {
                    OverScriptManager.Main.ApplyVariableChangesTo(val);
                }
            }
        }

    }

    [CustomPropertyDrawer(typeof(OverGraphVariableListAttribute))]
    public class OverGraphVariableListDrawer : PropertyDrawer 
    {
        float top = 5;
        float bottom = 5;
        float spacing = 5;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float heigth = top + EditorGUIUtility.singleLineHeight + bottom;

            object obj = property.GetTargetObjectOfProperty();
            OverGraphVariableData val = obj as OverGraphVariableData;

            //guid
            heigth += EditorGUIUtility.singleLineHeight + spacing;
            return heigth;
        }

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            float x = rect.x;
            float y = rect.y;

            float width = rect.width;
            float height = EditorGUIUtility.singleLineHeight;

            object obj = property.GetTargetObjectOfProperty();
            OverGraphVariableData val = obj as OverGraphVariableData;

            if (!string.IsNullOrEmpty(val.GUID))
            {
                EditorGUI.LabelField(new Rect(x, y, width, height), val.GUID);
                y += EditorGUIUtility.singleLineHeight + spacing;
            }

            EditorGUI.LabelField(new Rect(x, y, width / 2, height), val.name);
            val.type = (OverVariableType)EditorGUI.EnumPopup(new Rect(x + width / 2, y, width / 2, height), val.type);
        }
    }

}
