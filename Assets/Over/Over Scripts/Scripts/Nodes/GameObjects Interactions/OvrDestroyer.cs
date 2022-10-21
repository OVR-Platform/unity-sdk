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

using System.Collections.Generic;
using UnityEngine;

namespace Over
{
    public enum OvrDestroyType { DestroyTarget, DestroyAllTargetChildren };

    public class OvrDestroyer : OvrNode
    {
        public OvrDestroyType destroyType;
        public GameObject target;

        [OvrVariable]
        public OvrFloat timeBeforeDestroy;

        protected override void Execution()
        {
            if (target == null || timeBeforeDestroy == null)
            {
                if (Application.isEditor)
                    Debug.LogError("Null reference at gameObject " + gameObject.name);
                return;
            }
            

            switch (destroyType)
            {
                case OvrDestroyType.DestroyTarget:
                    if (timeBeforeDestroy.TypedVariable > 0)
                        Destroy(target, timeBeforeDestroy.TypedVariable);
                    else
                        Destroy(target);
                    break;
                case OvrDestroyType.DestroyAllTargetChildren:

                    foreach (Transform _child in target.transform)
                    {
                        if (timeBeforeDestroy.TypedVariable > 0)
                            Destroy(_child.gameObject, timeBeforeDestroy.TypedVariable);
                        else
                            Destroy(_child.gameObject);
                    }

                    break;
            }
        }

        public override void AddNode(OvrNode ovrNode)
        {
            target = ovrNode.gameObject;
        }
    }
}