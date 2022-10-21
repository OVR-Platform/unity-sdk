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
    [CustomEditor(typeof(OvrRandom))]
    public class OvrRandomCustom : Editor
    {
        public override void OnInspectorGUI()
        {
            var target = base.target as OvrRandom;

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("nodeId"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("preExecutionNodes"), true);
            EditorGUILayout.Space();


            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("variableType"), true);

            switch (target.variableType)
            {
                case OvrVariableType.Int:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ovrRandomType"), true);
                    switch (target.ovrRandomType)
                    {
                        case OvrRandomType.Between:
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("minInt"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("maxInt"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("intResult"), true);
                            break;
                        default:
                            break;
                    }
                    break;

                case OvrVariableType.Float:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ovrRandomType"), true);
                    switch (target.ovrRandomType)
                    {
                        case OvrRandomType.Between:
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("minFloat"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("maxFloat"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("floatResult"), true);
                            break;
                        default:
                            break;
                    }
                    break;

                case OvrVariableType.Vector2:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ovrRandomTypeVector2"), true);

                    switch (target.ovrRandomTypeVector2)
                    {
                        case OvrRandomTypeVector2.Between:
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("minVector2"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("maxVector2"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("vector2Result"), true);
                            break;

                        case OvrRandomTypeVector2.InsideUnitCircle:

                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("minFloat"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("maxFloat"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("vector2Result"), true);

                            break;
                        default:
                            break;
                    }

                    break;
                case OvrVariableType.Vector3:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ovrRandomTypeVector3"), true);
                    switch (target.ovrRandomTypeVector3)
                    {
                        case OvrRandomTypeVector3.Between:

                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("minVector3"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("maxVector3"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("vector3Result"), true);

                            break;

                        case OvrRandomTypeVector3.InsideUnitSphere:
                        case OvrRandomTypeVector3.OnUnitSphere:

                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("minFloat"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("maxFloat"), true);
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("vector3Result"), true);

                            break;
                        default:
                            break;
                    }

                    break;
                case OvrVariableType.Quaternion:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("ovrRandomTypeQuaternion"), true);

                    switch (target.ovrRandomTypeQuaternion)
                    {
                        case OvrRandomTypeQuaternion.RotationUniform:
                        case OvrRandomTypeQuaternion.Rotation:
                            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("quaternionResult"), true);
                            break;
                        default:
                            break;
                    }

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