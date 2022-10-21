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
    [CustomEditor(typeof(OvrLoopNode))]
    public class OvrLoopsCustom : Editor
    {
        public override void OnInspectorGUI()
        {
            var target = base.target as OvrLoopNode;

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("nodeId"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("preExecutionNodes"), true);
            this.serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("executeOnStart"), true);
            //EditorGUILayout.PropertyField(this.serializedObject.FindProperty("async"), true);
            this.serializedObject.ApplyModifiedProperties();

            if (target.forcedAsync)
            {
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("executionDelay"), true);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("endExecutionDelay"), true);

                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("timeBetweenEachNode"), true);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("timeBetweenEachIteration"), true);
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("loopType"), true);
            this.serializedObject.ApplyModifiedProperties();

            switch (target.loopType)
            {
                case OvrLoopType.IterationsCount:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("iterationsCount"), true);
                    goto default;

                case OvrLoopType.While:
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ovrCondition"), true);
                    goto default;

                default:



                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("nodes"), true);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("postExecutionNodes"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}