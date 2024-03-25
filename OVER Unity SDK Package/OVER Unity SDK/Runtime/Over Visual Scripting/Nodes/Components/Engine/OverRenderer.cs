/**
 * OVER Unity SDK License
 *
 * Copyright 2021 OVR
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
 * with services provided by OVR.
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
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [Node(Path = "Component/Engine/Renderer", Name = "Renderer Exposer", Icon = "COMPONENT/ENGINE/RENDERER")]
    [Tags("Component")]
    [Output("Ref", typeof(Renderer), Multiple = true)]
    public class OverRenderer : OverNode
    {
        [Input("Renderer", Multiple = false)] public Renderer renderer;

        [Output("Material", Multiple = true)] public Material material;
        [Output("Shared Material", Multiple = true)] public Material sharedMaterial;

        [Output("Materials", Multiple = true)] public Material[] materials;
        [Output("Shared Materials", Multiple = true)] public Material[] sharedMaterials;

        public override object OnRequestNodeValue(Port port)
        {
            Renderer _renderer = GetInputValue("Renderer", renderer);

            switch (port.Name)
            {
                case "Ref": return _renderer;
                case "Material": material = _renderer.material; return material;
                case "Shared Material": sharedMaterial = _renderer.sharedMaterial; return sharedMaterial;
                case "Materials": materials = _renderer.materials; return materials;
                case "Shared Materials": sharedMaterials = _renderer.sharedMaterials; return sharedMaterials;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverRendererHandlerNode : OverExecutionFlowNode { }

    public enum RendererInteractionType { New, Shared }

    [Node(Path = "Component/Engine/Renderer/Handlers", Name = "Set Material", Icon = "COMPONENT/ENGINE/RENDERER")]
    [Output("Output", typeof(Renderer), Multiple = true)]
    public class OverSetMaterial : OverRendererHandlerNode
    {
        [Input("Renderer", Multiple = false)] public Renderer renderer;
        [Input("Material")] public Material material;

        [Editable("Material Type")] public RendererInteractionType type;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Renderer _renderer = GetInputValue("Renderer", renderer);
            Material _material = GetInputValue("Material", material);


            if (_renderer != null && _material != null)
            {
                if (type == RendererInteractionType.New)
                    _renderer.material = _material;

                if (type == RendererInteractionType.Shared)
                    _renderer.sharedMaterial = _material;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            Renderer _renderer = GetInputValue("Renderer", renderer);

            switch (port.Name)
            {
                case "Output": return _renderer;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Engine/Renderer/AddOns", Name = "Set Mesh", Icon = "COMPONENT/ENGINE/RENDERER")]
    [Output("Output", typeof(Renderer), Multiple = true)]
    public class OverSetMesh : OverRendererHandlerNode
    {
        [Input("Renderer", Multiple = false)] public Renderer renderer;
        [Input("Mesh")] public Mesh mesh;

        [Editable("Mesh Type")] public RendererInteractionType type;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Renderer _renderer = GetInputValue("Renderer", renderer);
            Mesh _mesh = GetInputValue("Mesh", mesh);


            if (_renderer != null && _mesh != null)
            {
                MeshFilter _meshFilter = _renderer.gameObject.GetComponent<MeshFilter>();

                if (_meshFilter == null)
                {
                    _meshFilter = _renderer.gameObject.AddComponent<MeshFilter>();
                }

                if (type == RendererInteractionType.New)
                    _meshFilter.mesh = _mesh;

                if (type == RendererInteractionType.Shared)
                    _meshFilter.sharedMesh = _mesh;
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            Renderer _renderer = GetInputValue("Renderer", renderer);

            switch (port.Name)
            {
                case "Output": return _renderer;
            }

            return base.OnRequestNodeValue(port);
        }
    }
}