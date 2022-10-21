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
    [CustomEditor(typeof(OvrTransform))]
    public class OvrTransformCustom : Editor
    {
        public override void OnInspectorGUI()
        {
            var target = base.target as OvrTransform;

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("nodeId"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("preExecutionNodes"), true);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("target"), true);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("actionType"), true);
            this.serializedObject.ApplyModifiedProperties();

            switch (target.actionType)
            {
                case OvrTransformActionType.SetLocalPosition:
                case OvrTransformActionType.SetWorldPosition:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("targetPosition"), true);
                    break;

                case OvrTransformActionType.SetLocalRotation:
                case OvrTransformActionType.SetWorldRotation:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("targetRotation"), true);
                    break;

                case OvrTransformActionType.SetLocalScale:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("targetScale"), true);
                    break;

                case OvrTransformActionType.GetPosition:
                case OvrTransformActionType.GetLocalPosition:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("targetPosition"), true);
                    break;

                case OvrTransformActionType.GetRotation:
                case OvrTransformActionType.GetLocalRotation:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("targetRotation"), true);
                    break;

                case OvrTransformActionType.GetLocalScale:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("targetScale"), true);
                    break;

                case OvrTransformActionType.GetForward:
                case OvrTransformActionType.GetUp:
                case OvrTransformActionType.GetRight:
                case OvrTransformActionType.GetLocalForward:
                case OvrTransformActionType.GetLocalUp:
                case OvrTransformActionType.GetLocalRight:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("targetDir"), true);
                    break;

                case OvrTransformActionType.UnityAction:

                    this.serializedObject.Update();
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("unityAction"), true);
                    this.serializedObject.ApplyModifiedProperties();

                    break;
             
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("postExecutionNodes"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}