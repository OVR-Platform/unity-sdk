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
    [CustomNodeView(typeof(OverTexturePreviewNode))]
    [CustomNodeView(typeof(OverSpritePreviewNode))]
    public class OverTextureNodeView : NodeView
    {
        Texture2D previewTexture;
        const int TEXTURE_SIZE = 256;

        protected override void OnInitialize()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("OverTextureNodeView"));
            AddToClassList("overTextureNodeView");

            var titleContainer = this.Q("title");
            titleContainer.style.paddingLeft = 0;
            InitializeIcon(titleContainer);


            styleSheets.Add(Resources.Load<StyleSheet>("OverDataNodeView"));

            AddToClassList("overDataNodeView");

            previewTexture = new Texture2D(TEXTURE_SIZE, TEXTURE_SIZE, TextureFormat.ARGB32, false)
            {
                filterMode = FilterMode.Bilinear,
                hideFlags = HideFlags.HideAndDontSave
            };

            var preview = new VisualElement();
            preview.style.backgroundImage = new StyleBackground(previewTexture);
            preview.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;

            preview.style.minWidth = TEXTURE_SIZE;
            preview.style.minHeight = TEXTURE_SIZE;

            extensionContainer.Add(preview);
            RefreshExpandedState();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void OnPropertyChange()
        {
            base.OnPropertyChange();

            if (Target is OverTexturePreviewNode previewTextureNode)
            {
                UpdateTexture(previewTextureNode.GetPort("TexIn").GetValue(previewTextureNode.texIn));
            }

            if (Target is OverSpritePreviewNode previewSpriteNode)
            {
                UpdateTexture(previewSpriteNode.GetPort("SpriteIn").GetValue(previewSpriteNode.spriteIn.texture));
            }

        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (Target is OverTexturePreviewNode previewNode)
            {
                UpdateTexture(previewNode.GetPort("TexIn").GetValue(previewNode.texIn));
            }

            if (Target is OverSpritePreviewNode previewSpriteNode)
            {
                if (previewSpriteNode.spriteIn != null)
                {
                    UpdateTexture(previewSpriteNode.GetPort("SpriteIn").GetValue(previewSpriteNode.spriteIn.texture));
                }
            }

        }

        protected void UpdateTexture(Texture2D texture)
        {
            // TODO: Some sort of prettier "NO TEXTURE" output?
            if (texture == null)
            {
                return;
            }

            Texture2D temp = Resize(texture, TEXTURE_SIZE, TEXTURE_SIZE);
            Color[] c = temp.GetPixels();

            previewTexture.SetPixels(c);
            previewTexture.Apply();
        }

        Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
        {
            RenderTexture rt = new RenderTexture(targetX, targetY, 24);
            RenderTexture.active = rt;
            Graphics.Blit(texture2D, rt);
            Texture2D result = new Texture2D(targetX, targetY);
            result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
            result.Apply();
            return result;
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