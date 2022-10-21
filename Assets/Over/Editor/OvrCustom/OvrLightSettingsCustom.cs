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

namespace Over
{
    [CustomEditor(typeof(OvrLightSettings))]
    public class OvrLightSettingsCustom : Editor
    {
        public override void OnInspectorGUI()
        {
            var target = base.target as OvrLightSettings;

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("enableOvrDefaultLight"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("enableARLightSystem"), true);
            //EditorGUILayout.Space();

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("customAmbientLight"), true);
            this.serializedObject.ApplyModifiedProperties();

            switch (target.customAmbientLight)
            {
                case LightPreferenceSetting.Ignore:
                case LightPreferenceSetting.Off:
                    break;
                default:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ambientColorCorrection"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ambientIntensity"), true);
                    break;
            }

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("customShadowSettings"), true);
            this.serializedObject.ApplyModifiedProperties();

            switch (target.customShadowSettings)
            {
                case LightPreferenceSetting.Ignore:
                case LightPreferenceSetting.Off:
                    break;
                default:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("setDistance"), true);
                    this.serializedObject.ApplyModifiedProperties();
                    if(target.setDistance)
                        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("shadowDistance"), true);

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("setProjection"), true);
                    this.serializedObject.ApplyModifiedProperties();
                    if (target.setProjection)
                        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("shadowProjection"), true);


                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("setResolution"), true);
                    this.serializedObject.ApplyModifiedProperties();
                    if (target.setResolution)
                        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("shadowResolution"), true);

                    break;
            }                      

            this.serializedObject.ApplyModifiedProperties();
        }
    }
}