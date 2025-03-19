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

using BlueGraph;
using BlueGraph.Editor;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OverSDK.VisualScripting.Editor
{
    [CustomNodeView(typeof(OverTransform))]
    [CustomNodeView(typeof(OverTransformHandlerNode))]
    [CustomNodeView(typeof(OverObject))]
    [CustomNodeView(typeof(OverObjectHandlerNode))]
    [CustomNodeView(typeof(OverRigidbody))]
    [CustomNodeView(typeof(OverRigidbodyHandlerNode))]
    [CustomNodeView(typeof(OverRenderer))]
    [CustomNodeView(typeof(OverRendererHandlerNode))]
    [CustomNodeView(typeof(OverLineRenderer))]
    [CustomNodeView(typeof(OverLineRendererHandlerNode))]
    [CustomNodeView(typeof(OverParticleSystemHandlerNode))]
    [CustomNodeView(typeof(OverAnimator))]
    [CustomNodeView(typeof(OverAnimatorHandler))]
    [CustomNodeView(typeof(OverAudioSource))]
    [CustomNodeView(typeof(OverAudioHandlerNode))]
    [CustomNodeView(typeof(OverLight))]
    [CustomNodeView(typeof(OverLightHandlerNode))]
    [CustomNodeView(typeof(OverVideoPlayer))]
    [CustomNodeView(typeof(OverVideoHandlerNode))]
    [CustomNodeView(typeof(OverRectTransform))]
    [CustomNodeView(typeof(OverUIImage))]
    [CustomNodeView(typeof(OverUIRawImage))]
    [CustomNodeView(typeof(OverImageHandlerNode))]
    [CustomNodeView(typeof(OverUIText))]
    [CustomNodeView(typeof(OverUITextTMP))]
    [CustomNodeView(typeof(OverUITextTMP_3D))]
    [CustomNodeView(typeof(OverTextHandlerNode))]
    [CustomNodeView(typeof(OverMaterial))]
    [CustomNodeView(typeof(OverMaterialHandlerNode))] 
    [CustomNodeView(typeof(OverImageStreamerCommon))]
    [CustomNodeView(typeof(OverImageStreamerHandler))] 
    public class OverComponentNodeView : NodeView
    {
        protected override void OnInitialize()
        {
            var titleContainer = this.Q("title");
            titleContainer.style.paddingLeft = 0;
            InitializeIcon(titleContainer);

            styleSheets.Add(Resources.Load<StyleSheet>("OverComponentNodeView"));

            if (Target is OverExecutionFlowNode)
            {
                styleSheets.Add(Resources.Load<StyleSheet>("OverExecutableNodeView"));
                AddToClassList("overExecutableNodeView");

                PortView inView = GetInputPort("ExecIn");
                PortView outView = GetOutputPort("ExecOut");

                if (inView != null) inView.AddToClassList("execInPortView");

                if (outView != null) outView.AddToClassList("execOutPortView");
            }

            AddToClassList("overComponentNodeView");

        }

        void InitializeIcon(VisualElement titleContainer)
        {
            if (!string.IsNullOrEmpty(Target.Icon))
            {
                var iconContainer = new VisualElement { name = "icon" };

                var background = iconContainer.style.backgroundImage;
                var backgroundVal = background.value;
                string iconPath = Path.Combine("Packages/com.over.over-unity-sdk", "Editor Default Resources", $"Visual Scripting Icons/{Target.Icon}.png");
                backgroundVal.texture = EditorGUIUtility.Load(iconPath) as Texture2D;
                background.value = backgroundVal;

                iconContainer.style.backgroundImage = background;

                titleContainer.Insert(0, iconContainer);
            }

            if (Target is OverExecutionFlowNode)
            {
                titleContainer.style.paddingLeft = 18;
                if (!string.IsNullOrEmpty(Target.Icon))
                {
                    titleContainer.style.paddingLeft = 38;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Target.Icon))
                {
                    titleContainer.style.paddingLeft = 18;
                }
            }
        }

    }
}