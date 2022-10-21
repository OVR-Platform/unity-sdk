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

namespace Over
{
    public enum OvrRandomType { Between }
    public enum OvrRandomTypeVector2 { Between, InsideUnitCircle }
    public enum OvrRandomTypeVector3 { Between, InsideUnitSphere,  OnUnitSphere }
    public enum OvrRandomTypeQuaternion { Rotation, RotationUniform }

    public class OvrRandom : OvrNode
    {
        //OvrRandom
        public OvrVariableType variableType;
        public OvrRandomType ovrRandomType;
        public OvrRandomTypeVector2 ovrRandomTypeVector2;
        public OvrRandomTypeVector3 ovrRandomTypeVector3;
        public OvrRandomTypeQuaternion ovrRandomTypeQuaternion;

        //Int
        [OvrVariable]
        public OvrInt minInt;
        [OvrVariable]
        public OvrInt maxInt;
        [OvrVariable]
        public OvrInt intResult;

        //Float
        [OvrVariable]
        public OvrFloat minFloat;
        [OvrVariable]
        public OvrFloat maxFloat;
        [OvrVariable]
        public OvrFloat floatResult;

        //Vector2
        [OvrVariable]
        public OvrVector2 minVector2;
        [OvrVariable]
        public OvrVector2 maxVector2;
        [OvrVariable]
        public OvrVector2 vector2Result;

        //Vector3
        [OvrVariable]
        public OvrVector3 minVector3;
        [OvrVariable]
        public OvrVector3 maxVector3;
        [OvrVariable]
        public OvrVector3 vector3Result;

        //Quaternion
        [OvrVariable]
        public OvrQuaternion quaternionResult;

        protected override void Execution()
        {
            switch (variableType)
            {
                case OvrVariableType.Int:

                    if (intResult == null || minInt == null || maxInt == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }

                    break;
                case OvrVariableType.Float:

                    if (floatResult == null || minFloat == null || maxFloat == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }

                    break;
                case OvrVariableType.Vector2:

                    if (vector2Result == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }

                    break;
                case OvrVariableType.Vector3:

                    if (vector3Result == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }

                    break;
                case OvrVariableType.Quaternion:

                    if (quaternionResult == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }

                    break;
                case OvrVariableType.String:
                    break;
            }


            switch (variableType)
            {
                case OvrVariableType.Int:

                    switch (ovrRandomType)
                    {
                        case OvrRandomType.Between:
                            intResult.TypedVariable = Random.Range(minInt.TypedVariable, maxInt.TypedVariable);
                            break;
                        default:
                            intResult.TypedVariable = 0;
                            break;
                    }

                    break;
                case OvrVariableType.Float:

                    switch (ovrRandomType)
                    {
                        case OvrRandomType.Between:
                            floatResult.TypedVariable = Random.Range(minFloat.TypedVariable, maxFloat.TypedVariable);
                            break;
                        default:
                            floatResult.TypedVariable = 0;
                            break;
                    }

                    break;
                case OvrVariableType.Vector2:

                    switch (ovrRandomTypeVector2)
                    {
                        case OvrRandomTypeVector2.Between:

                            if (minVector2 == null || maxVector2 == null)
                            {
                                if (Application.isEditor)
                                    Debug.LogError("Null reference at gameObject " + gameObject.name);
                                return;
                            }

                            vector2Result.TypedVariable = new Vector2(Random.Range(minVector2.TypedVariable.x, maxVector2.TypedVariable.x), Random.Range(minVector2.TypedVariable.y, maxVector2.TypedVariable.y));
                            break;
                        case OvrRandomTypeVector2.InsideUnitCircle:

                            if (minFloat == null || maxFloat == null)
                            {
                                if (Application.isEditor)
                                    Debug.LogError("Null reference at gameObject " + gameObject.name);
                                return;
                            }

                            vector2Result.TypedVariable = (minFloat.TypedVariable * Vector2.one) + Random.insideUnitCircle * (maxFloat.TypedVariable - minFloat.TypedVariable);
                            break;
                        default:
                            vector2Result.TypedVariable = Vector2.zero;
                            break;
                    }

                    break;
                case OvrVariableType.Vector3:

                    switch (ovrRandomTypeVector3)
                    {
                        case OvrRandomTypeVector3.Between:

                            if (minVector3 == null || maxVector3 == null)
                            {
                                if (Application.isEditor)
                                    Debug.LogError("Null reference at gameObject " + gameObject.name);
                                return;
                            }
                            vector3Result.TypedVariable = new Vector3(Random.Range(minVector3.TypedVariable.x, maxVector3.TypedVariable.x), Random.Range(minVector3.TypedVariable.y, maxVector3.TypedVariable.y), Random.Range(minVector3.TypedVariable.z, maxVector3.TypedVariable.z));

                            break;
                        case OvrRandomTypeVector3.InsideUnitSphere:

                            if (minFloat == null || maxFloat == null)
                            {
                                if (Application.isEditor)
                                    Debug.LogError("Null reference at gameObject " + gameObject.name);
                                return;
                            }
                            vector3Result.TypedVariable = (minFloat.TypedVariable * Vector3.one) + Random.insideUnitSphere * (maxFloat.TypedVariable - minFloat.TypedVariable);

                            break;
                        case OvrRandomTypeVector3.OnUnitSphere:

                            if (minFloat == null || maxFloat == null)
                            {
                                if (Application.isEditor)
                                    Debug.LogError("Null reference at gameObject " + gameObject.name);
                                return;
                            }
                            vector3Result.TypedVariable = (minFloat.TypedVariable * Vector3.one) + Random.onUnitSphere * (maxFloat.TypedVariable - minFloat.TypedVariable);

                            break;
                        default:
                            vector3Result.TypedVariable = Vector3.zero;
                            break;
                    }

                    break;
                case OvrVariableType.Quaternion:

                    switch (ovrRandomTypeQuaternion)
                    {
                        case OvrRandomTypeQuaternion.Rotation:
                            quaternionResult.TypedVariable = Random.rotation;
                            break;
                        case OvrRandomTypeQuaternion.RotationUniform:
                            quaternionResult.TypedVariable = Random.rotationUniform;
                            break;
                        default:
                            quaternionResult.TypedVariable = Quaternion.identity;
                            break;
                    }

                    break;
            }
        }
    }
}