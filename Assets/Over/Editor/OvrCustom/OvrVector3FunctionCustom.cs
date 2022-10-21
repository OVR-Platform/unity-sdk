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

using UnityEditor;
using UnityEngine;

namespace Over
{
    [CustomEditor(typeof(OvrVector3Function))]
    public class OvrVector3FunctionCustom : Editor
    {
        public override void OnInspectorGUI()
        {
            var target = base.target as OvrVector3Function;

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("nodeId"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("preExecutionNodes"), true);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("vector3FunctionType"), true);

            switch (target.vector3FunctionType)
            {
                case Vector3FunctionType.GetDistance:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("variable1"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("variable2"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("result"), true);
                    
                    break;
                default:
                    break;
            }                  

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("postExecutionNodes"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}