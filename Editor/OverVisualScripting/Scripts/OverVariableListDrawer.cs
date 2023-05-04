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
            val.id = EditorGUI.TextField(new Rect(x, y, width / 2, height), val.id);

            val.type = (OverVariableType)EditorGUI.EnumPopup(new Rect(x + width / 2, y, width / 2, height), val.type);

            switch (val.type)
            {
                //simple
                case OverVariableType.Bool:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    bool temp_bool = EditorGUI.Toggle(new Rect(x, y, width, height), "value:", val.boolValue);
                    if (val.boolValue != temp_bool)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.boolValue = temp_bool;
                    }
                    break;
                case OverVariableType.String:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    string temp_string = EditorGUI.TextField(new Rect(x, y, width, height), "value:", val.stringValue);
                    if (val.stringValue != temp_string)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.stringValue = temp_string;
                    }
                    break;
                case OverVariableType.Float:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    float temp_float = EditorGUI.FloatField(new Rect(x, y, width, height), "value:", val.floatValue, new GUIStyle(GUI.skin.textField) { alignment = TextAnchor.MiddleRight });
                    if (val.floatValue != temp_float)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.floatValue = temp_float;
                    }
                    break;
                case OverVariableType.Int:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    int temp_int = EditorGUI.IntField(new Rect(x, y, width, height), "value:", val.integerValue, new GUIStyle(GUI.skin.textField) { alignment = TextAnchor.MiddleRight });
                    if (val.integerValue != temp_int)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.integerValue = temp_int;
                    }
                    break;
                case OverVariableType.Vector2:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Vector2 temp_v2 = EditorGUI.Vector2Field(new Rect(x, y, width, height), "value:", val.vector2Value);
                    if (val.vector2Value != temp_v2)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.vector2Value = temp_v2;
                    }
                    break;
                case OverVariableType.Vector3:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Vector3 temp_v3 = EditorGUI.Vector3Field(new Rect(x, y, width, height), "value:", val.vector3Value);
                    if (val.vector3Value != temp_v3)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.vector3Value = temp_v3;
                    }
                    break;
                case OverVariableType.Quaternion:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Vector4 vector = new Vector4(val.QuaternionValue.x, val.QuaternionValue.y, val.QuaternionValue.z, val.QuaternionValue.w);
                    Vector4 temp_vector = EditorGUI.Vector4Field(new Rect(x, y, width, height), "value:", vector);
                    if (vector != temp_vector)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        vector = temp_vector;
                    }
                    val.QuaternionValue = new Quaternion(vector.x, vector.y, vector.z, vector.w);
                    break;
                // Mono
                case OverVariableType.Transform:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Transform temp_tr = (Transform)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.transformValue, typeof(Transform), true);
                    if (val.transformValue != temp_tr)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.transformValue = temp_tr;
                    }
                    break;
                case OverVariableType.RectTransform:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    RectTransform temp_rtr = (RectTransform)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.rectTransform, typeof(RectTransform), true);
                    if (val.rectTransform != temp_rtr)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.rectTransform = temp_rtr;
                    }
                    break;
                case OverVariableType.Rigidbody:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Rigidbody temp_rgbd = (Rigidbody)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.rigidbodyValue, typeof(Rigidbody), true);
                    if (val.rigidbodyValue != temp_rgbd)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.rigidbodyValue = temp_rgbd;
                    }
                    break;
                case OverVariableType.Object:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    GameObject temp_obj = (GameObject)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.gameObject, typeof(GameObject), true);
                    if (val.gameObject != temp_obj)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.gameObject = temp_obj;
                    }
                    break;
                case OverVariableType.Renderer:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Renderer temp_rndr = (Renderer)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.renderer, typeof(Renderer), true);
                    if (val.renderer != temp_rndr)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.renderer = temp_rndr;
                    }
                    break;
                case OverVariableType.LineRenderer:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    LineRenderer temp_lrndr = (LineRenderer)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.lineRenderer, typeof(LineRenderer), true);
                    if (val.lineRenderer != temp_lrndr)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.lineRenderer = temp_lrndr;
                    }
                    break;
                case OverVariableType.Audio:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    AudioSource temp_aud = (AudioSource)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.audioSource, typeof(AudioSource), true);
                    if (val.audioSource != temp_aud)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.audioSource = temp_aud;
                    }
                    break;
                case OverVariableType.Video:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    VideoPlayer temp_vid = (VideoPlayer)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.videoPlayer, typeof(VideoPlayer), true);
                    if (val.videoPlayer != temp_vid)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.videoPlayer = temp_vid;
                    }
                    break;
                case OverVariableType.Animator:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Animator temp_anim = (Animator)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.animator, typeof(Animator), true);
                    if (val.animator != temp_anim)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.animator = temp_anim;
                    }
                    break;
                case OverVariableType.Light:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Light temp_lgt = (Light)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.light, typeof(Light), true);
                    if (val.light != temp_lgt)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.light = temp_lgt;
                    }
                    break;
                //case OverVariableType.Button:
                //    y += EditorGUIUtility.singleLineHeight + spacing;
                //    Button temp_btn = (Button)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.button, typeof(Button), true);
                //    if (val.button != temp_btn)
                //    {
                //        EditorUtility.SetDirty(property.serializedObject.targetObject);
                //        val.button = temp_btn;
                //    }
                //    break;
                case OverVariableType.Text:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Text temp_txt = (Text)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.text, typeof(Text), true);
                    if (val.text != temp_txt)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.text = temp_txt;
                    }
                    break;
                case OverVariableType.TextTMP:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    TextMeshProUGUI temp_textTMP = (TextMeshProUGUI)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.textTMP, typeof(TextMeshProUGUI), true);
                    if (val.textTMP != temp_textTMP)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.textTMP = temp_textTMP;
                    }
                    break;
                case OverVariableType.Image:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    Image temp_img = (Image)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.image, typeof(Image), true);
                    if (val.image != temp_img)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.image = temp_img;
                    }
                    break;
                case OverVariableType.RawImage:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    RawImage temp_rimg = (RawImage)EditorGUI.ObjectField(new Rect(x, y, width, height), "value:", val.rawImage, typeof(RawImage), true);
                    if (val.rawImage != temp_rimg)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.rawImage = temp_rimg;
                    }
                    break;
                case OverVariableType.Color:
                    y += EditorGUIUtility.singleLineHeight + spacing;
                    label.text = "value:";
                    Color temp_col = EditorGUI.ColorField(new Rect(x, y, width, height), label, val.color, true, true, false);
                    if (val.color != temp_col)
                    {
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                        val.color = temp_col;
                    }
                    break;
                default: break;
            }

        }
    }
}
