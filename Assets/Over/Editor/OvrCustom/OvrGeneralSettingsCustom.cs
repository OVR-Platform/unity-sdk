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

using UnityEngine;
using UnityEditor;

namespace Over
{
    [CustomEditor(typeof(OvrGeneralSettings))]
    public class OvrGeneralSettingsCustom : Editor
    {
        bool arExperienceFoldout = true;
        bool remoteFoldout = true;

        public override void OnInspectorGUI()
        {
            var target = base.target as OvrGeneralSettings;

            //AR ---------------------------------------------------------------------------------------------------------------------------------------------
            EditorGUILayout.LabelField("Horizontal line", GUI.skin.horizontalSlider);
            arExperienceFoldout = EditorGUILayout.Foldout(arExperienceFoldout, "Geolocated Experience Settings", EditorStyles.foldoutHeader);
            EditorGUILayout.Space();
            EditorGUI.indentLevel++;

            if (arExperienceFoldout)
            {
                EditorGUILayout.LabelField("Android Occlusion Settings", EditorStyles.whiteLargeLabel);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("environmentOcclusionAR"), new GUIContent("Environment Occlusion"));
                if(target.environmentOcclusionAR)
                {
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("automaticStartEnvironmentOcclusionAR"), new GUIContent("Automatic Start"));
                }
                else
                {
                    target.automaticStartEnvironmentOcclusionAR = false;
                }

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("iOS Occlusion Settings", EditorStyles.whiteLargeLabel);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("humanOcclusionAR"), new GUIContent("Human Occlusion"));
                if (target.humanOcclusionAR)
                {
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("automaticStartHumanOcclusionAR"), new GUIContent("Automatic Start"));
                }
                else
                {
                    target.automaticStartHumanOcclusionAR = false;
                }

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("iOS Lidar Occlusion Settings", EditorStyles.whiteLargeLabel);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("meshOcclusionAR"), new GUIContent("Mesh Occlusion"));
                if (target.meshOcclusionAR)
                {
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("automaticStartMeshOcclusionAR"), new GUIContent("Automatic Start"));
                }
                else
                {
                    target.automaticStartMeshOcclusionAR = false;
                }
            }

            EditorGUILayout.Space();
            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField("Horizontal line", GUI.skin.horizontalSlider);

            //REMOTE ------------------------------------------------------------------------------------------------------------------------------------------
            remoteFoldout = EditorGUILayout.Foldout(remoteFoldout, "Remote Experience Settings", EditorStyles.foldoutHeader);
            EditorGUILayout.Space();
            EditorGUI.indentLevel++;

            if (remoteFoldout)
            {
                EditorGUILayout.LabelField("Android Occlusion Settings", EditorStyles.whiteLargeLabel);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("environmentOcclusionRemote"), new GUIContent("Environment Occlusion"));
                if (target.environmentOcclusionRemote)
                {
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("automaticStartEnvironmentOcclusionRemote"), new GUIContent("Automatic Start"));
                }
                else
                {
                    target.automaticStartEnvironmentOcclusionRemote = false;
                }

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("iOS Occlusion Settings", EditorStyles.whiteLargeLabel);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("humanOcclusionRemote"), new GUIContent("Human Occlusion"));
                if (target.humanOcclusionRemote)
                {
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("automaticStartHumanOcclusionRemote"), new GUIContent("Automatic Start"));
                }
                else
                {
                    target.automaticStartHumanOcclusionRemote = false;
                }

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("iOS Lidar Occlusion Settings", EditorStyles.whiteLargeLabel);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("meshOcclusionRemote"), new GUIContent("Mesh Occlusion"));
                if (target.meshOcclusionRemote)
                {
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("automaticStartMeshOcclusionRemote"), new GUIContent("Automatic Start"));
                }
                else
                {
                    target.automaticStartMeshOcclusionRemote = false;
                }

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Enable Walk Mode Toggle", EditorStyles.whiteLargeLabel);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("walkModeButton"), new GUIContent("Walk Mode"));
                //if (target.walkModeButton)
                //{
                //    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("automaticStartWalkMode"), new GUIContent("Automatic Start"));
                //}
                //else
                //{
                //    target.automaticStartWalkMode = false;
                //}
            }

            EditorGUILayout.Space();
            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField("Horizontal line", GUI.skin.horizontalSlider);

            this.serializedObject.ApplyModifiedProperties();
        }
    }
}