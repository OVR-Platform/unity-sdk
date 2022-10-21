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
using System.Collections;
using System.Collections.Generic;

namespace Over
{
    public enum OvrLoopType { IterationsCount, While }

    public class OvrLoopNode : OvrAsyncNode
    {
        public OvrLoopType loopType;
        public bool executeOnStart;

        [OvrNodeList]
        public List<OvrNode> nodes = new List<OvrNode>();

        //Async
        public readonly bool forcedAsync = true;
        [OvrVariable]
        public OvrFloat executionDelay;
        [OvrVariable]
        public OvrFloat endExecutionDelay;
        [OvrVariable]
        public OvrFloat timeBetweenEachNode;
        [OvrVariable]
        public OvrFloat timeBetweenEachIteration;

        //IterationsCount
        [OvrVariable]
        public OvrInt iterationsCount;

        //Condiction
        [OvrNode]
        public OvrCondition ovrCondition;


        public void Start()
        {
            if (executeOnStart)
                Execute();
        }

        protected override void Execution()
        {
            switch (loopType)
            {
                case OvrLoopType.IterationsCount:

                    if (iterationsCount == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }

                    if (!forcedAsync)
                    {
                        int iteration = iterationsCount.TypedVariable;
                        while (iteration > 0)
                        {
                            foreach (var node in nodes)
                            {
                                if (node != null)
                                    node.Execute();
                                else if (Application.isEditor)
                                    Debug.LogError("Null reference at gameObject " + gameObject.name);
                            }
                            iteration--;
                        }

                        PostExecute();
                    }
                    else
                    {
                        if (executionDelay == null || endExecutionDelay == null || timeBetweenEachNode == null || timeBetweenEachIteration == null)
                        {
                            if (Application.isEditor)
                                Debug.LogError("Null reference at gameObject " + gameObject.name);
                            return;
                        }

                        if (executionCoroutine != null)
                            StopCoroutine(executionCoroutine);
                        executionCoroutine = StartCoroutine(NoConditionCoroutine());
                    }

                    break;
                case OvrLoopType.While:

                    if (ovrCondition == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }

                    if (!forcedAsync)
                    {
                        while (ovrCondition.IsSatisfied())
                        {
                            foreach (var node in nodes)
                            {
                                if (node != null)
                                    node.Execute();
                                else if (Application.isEditor)
                                    Debug.LogError("Null reference at gameObject " + gameObject.name);
                            }
                        }

                        PostExecute();
                    }
                    else
                    {
                        if (executionDelay == null || endExecutionDelay == null || timeBetweenEachNode == null || timeBetweenEachIteration == null)
                        {
                            if (Application.isEditor)
                                Debug.LogError("Null reference at gameObject " + gameObject.name);
                            return;
                        }

                        if (executionCoroutine != null)
                            StopCoroutine(executionCoroutine);
                        executionCoroutine = StartCoroutine(WhileCoroutine());
                    }

                    break;

            }
        }

        IEnumerator NoConditionCoroutine()
        {
            if (executionDelay.TypedVariable != 0)
                yield return new WaitForSeconds(executionDelay.TypedVariable);

            int iteration = iterationsCount.TypedVariable;
            while (iteration > 0)
            {
                foreach (var node in nodes)
                {
                    if (node != null)
                        node.Execute();
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    yield return new WaitForSeconds(timeBetweenEachNode.TypedVariable);
                }
                iteration--;

                yield return new WaitForSeconds(timeBetweenEachIteration.TypedVariable);
            }

            if (endExecutionDelay.TypedVariable != 0)
                yield return new WaitForSeconds(endExecutionDelay.TypedVariable);

            PostExecute();
        }

        IEnumerator WhileCoroutine()
        {
            if (executionDelay.TypedVariable != 0)
                yield return new WaitForSeconds(executionDelay.TypedVariable);

            while (ovrCondition.IsSatisfied())
            {
                foreach (var node in nodes)
                {
                    if (node != null)
                        node.Execute();
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    yield return new WaitForSeconds(timeBetweenEachNode.TypedVariable);
                }

                yield return new WaitForSeconds(timeBetweenEachIteration.TypedVariable);
            }

            if (endExecutionDelay.TypedVariable != 0)
                yield return new WaitForSeconds(endExecutionDelay.TypedVariable);

            PostExecute();
        }

        public override void StopExecution()
        {
            if (executionCoroutine != null)
                StopCoroutine(executionCoroutine);
            base.StopExecution();
        }

        public override void AddNode(OvrNode ovrNode)
        {
            nodes.Add(ovrNode);
        }
    }
}