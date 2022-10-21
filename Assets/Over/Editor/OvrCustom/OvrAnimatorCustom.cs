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
    [CustomEditor(typeof(OvrAnimator))]
    public class OvrAnimatorCustom : Editor
    {
        public override void OnInspectorGUI()
        {
            var target = base.target as OvrAnimator;

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("nodeId"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("preExecutionNodes"), true);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("animator"), true);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("actionType"), true);
            this.serializedObject.ApplyModifiedProperties();

            switch (target.actionType)
            {
                case OvrAnimatorActionType.CrossFadeInt:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("stateHashName"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("transitionDuration"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("layer"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("timeOffset"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("transitionTime"), true);
                    this.serializedObject.ApplyModifiedProperties();
                    break;

                case OvrAnimatorActionType.CrossFadeString:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("stateName"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("transitionDuration"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("layer"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("timeOffset"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("transitionTime"), true);
                    break;

                case OvrAnimatorActionType.SetLayerWeight:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("layerIndex"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("weight"), true);
                    break;

                case OvrAnimatorActionType.SetLookAtPosition:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("lookAtPosition"), true);
                    break;

                case OvrAnimatorActionType.SetTarget:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("targetIndex"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("tNormalizedTime"), true);
                    break;

                case OvrAnimatorActionType.SetInteger:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("parameterName"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("intValue"), true);
                    break;

                case OvrAnimatorActionType.SetBool:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("parameterName"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("boolValue"), true);
                    break;

                case OvrAnimatorActionType.SetFloat:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("parameterName"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("floatValue"), true);
                    break;

                case OvrAnimatorActionType.UnityAction:
                    serializedObject.Update();
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("unityAction"), true);
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("postExecutionNodes"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}