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
    [CustomEditor(typeof(OvrRigidBody))]
    public class OvrRigidBodyCustom : Editor
    {
        public override void OnInspectorGUI()
        {
            var target = base.target as OvrRigidBody;
         
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("nodeId"), true);
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("preExecutionNodes"), true);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("rigidBody"), true);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("actionType"), true);
            this.serializedObject.ApplyModifiedProperties();

            switch (target.actionType)
            {
              
                case OvrRigidbodyActionType.ExplosiveForce:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("space"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("direction"), true);
                    this.serializedObject.ApplyModifiedProperties();
                    if (target.direction == OvrDirection.Custom)
                        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("customDirection"), true);

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("forceValue"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("upwardsModifier"), true);

                    break;

                case OvrRigidbodyActionType.Force:
                case OvrRigidbodyActionType.RelativeForce:
                case OvrRigidbodyActionType.RelativeTorque:
                case OvrRigidbodyActionType.Torque:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("space"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("direction"), true);
                    this.serializedObject.ApplyModifiedProperties();
                    if (target.direction == OvrDirection.Custom)
                        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("customDirection"), true);

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("forceValue"), true);

                    break;

                case OvrRigidbodyActionType.UnityAction:

                    serializedObject.Update();
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("unityAction"), true);
                    serializedObject.ApplyModifiedProperties();

                    break;

                case OvrRigidbodyActionType.MovePosition:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("space"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("direction"), true);
                    this.serializedObject.ApplyModifiedProperties();
                    if (target.direction == OvrDirection.Custom)
                        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("customDirection"), true);

                    break;

                case OvrRigidbodyActionType.MoveRotation:

                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("space"), true);
                    EditorGUILayout.PropertyField(this.serializedObject.FindProperty("quaternion"), true);

                    break;

                case OvrRigidbodyActionType.ResetVelocity:
                            

                    break;                    
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("postExecutionNodes"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}