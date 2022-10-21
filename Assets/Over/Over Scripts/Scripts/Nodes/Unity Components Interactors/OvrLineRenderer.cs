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
using UnityEngine.Events;

namespace Over
{
    public enum OvrLineRendererActionType { SetPosition, UnityAction }

    public class OvrLineRenderer : OvrNode
    {
        public LineRenderer lineRenderer;
        public OvrLineRendererActionType actionType;

        //Set Position
        [OvrVariable]
        public OvrInt index;
        [OvrVariable]
        public OvrVector3 position;

        //UnityEvent
        public UnityEvent unityAction;

        protected override void Execution()
        {
            switch (actionType)
            {
                case OvrLineRendererActionType.SetPosition:
                    if (lineRenderer == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
            }

            switch (actionType)
            {
                case OvrLineRendererActionType.SetPosition:

                    if (index != null && position != null)
                        lineRenderer.SetPosition(index.TypedVariable, position.TypedVariable);
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    break;
                case OvrLineRendererActionType.UnityAction:
                    unityAction?.Invoke();
                    break;
            }
        }

        //public void BakeMesh(Mesh mesh, bool useTransform = false) { lineRenderer.BakeMesh(mesh, useTransform); }
    }
}