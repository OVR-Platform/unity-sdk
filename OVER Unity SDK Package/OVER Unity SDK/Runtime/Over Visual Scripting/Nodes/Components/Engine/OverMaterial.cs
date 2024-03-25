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
    [Node(Path = "Component/Engine/Material", Name = "Material Exposer", Icon = "COMPONENT/ENGINE/MATERIAL")]
    [Tags("Component")]
    [Output("Ref", typeof(Material), Multiple = true)]
    public class OverMaterial : OverNode
    {
        [Input("Material", Multiple = false)] public Material material;

        [Output("Color")] public Color color;
        [Output("Albedo")] public Texture2D albedo;
        [Output("Metallic")] public float metallic;
        [Output("Metallic Map")] public Texture2D metallicMap;
        [Output("Metallic Smoothness")] public float metallicSmoothness;
        [Output("Normal Map")] public Texture2D normalMap;
        [Output("Height Map")] public Texture2D heightMap;
        [Output("Height Value")] public float heightScale;
        [Output("Occlusion")] public Texture2D occlusion;
        [Output("Detail Mask")] public Texture2D detailMask;
        [Output("Emission")] public Texture2D emissionMap;
        [Output("Emission Color")] public Color emissionColor;

        public override object OnRequestNodeValue(Port port)
        {
            Material _material = GetInputValue("Material", material);

            switch (port.Name)
            {
                case "Ref": return _material;
                case "Color": color = _material.GetColor("_Color"); return color;
                case "Albedo": albedo = _material.GetTexture("_MainTex") as Texture2D; return albedo;
                case "Metallic": metallic = _material.GetFloat("_Metallic"); return metallic;
                case "Metallic Map": metallicMap = _material.GetTexture("_MetallicGlossMap") as Texture2D; return metallicMap;
                case "Metallic Smoothness": metallicSmoothness = _material.GetFloat("_Glossiness"); return metallicSmoothness;
                case "Normal Map": normalMap = _material.GetTexture("_BumpMap") as Texture2D; return normalMap;
                case "Height Map": heightMap = _material.GetTexture("_ParallaxMap") as Texture2D; return heightMap;
                case "Height Value": heightScale = _material.GetFloat("_Parallax"); return heightScale;
                case "Occlusion": occlusion = _material.GetTexture("_OcclusionMap") as Texture2D; return occlusion;
                case "Detail Mask": detailMask = _material.GetTexture("_DetailMask") as Texture2D; return detailMask;
                case "Emission": emissionMap = _material.GetTexture("_EmissionMap") as Texture2D; return emissionMap;
                case "Emission Color": emissionColor = _material.GetColor("_EmissionColor"); return emissionColor;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverMaterialHandlerNode : OverExecutionFlowNode { }

    [Node(Path = "Component/Engine/Material/Handlers", Name = "Set Albedo", Icon = "COMPONENT/ENGINE/MATERIAL")]
    [Output("Output", typeof(Material), Multiple = true)]
    public class OverSetAlbedo : OverMaterialHandlerNode
    {
        [Input("Material", Multiple = false)] public Material material;
        [Input("Color")] public Color color = Color.white;
        [Input("Albedo")] public Texture2D albedo;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Material _material = GetInputValue("Material", material);
            Texture2D _albedo = GetInputValue("Albedo", albedo);
            Color _color = GetInputValue("Color", color);


            if (_material != null)
            {
                if (_albedo != null)
                {
                    _material.SetTexture("_MainTex", _albedo);
                }

                _material.SetColor("_Color", _color);
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            Material _material = GetInputValue("Material", material);

            switch (port.Name)
            {
                case "Output": return _material;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Engine/Material/Handlers", Name = "Set Metallic", Icon = "COMPONENT/ENGINE/MATERIAL")]
    [Output("Output", typeof(Material), Multiple = true)]
    public class OverSetMetallic : OverMaterialHandlerNode
    {
        [Input("Material", Multiple = false)] public Material material;

        [Input("Metallic")] public float metallic;
        [Input("Metallic Map")] public Texture2D metallicMap;
        [Input("Metallic Smoothness")] public float metallicSmoothness = .5f;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Material _material = GetInputValue("Material", material);
            float _metallic = GetInputValue("Metallic", metallic);
            float _metallicSmoothness = GetInputValue("Metallic Smoothness", metallicSmoothness);
            Texture2D _metallicMap = GetInputValue("Metallic Map", metallicMap);


            if (_material != null)
            {
                if (_metallicMap != null)
                {
                    _material.SetTexture("_MetallicGlossMap", _metallicMap);
                }

                _material.SetFloat("_Metallic", _metallic);
                _material.SetFloat("_Glossiness", _metallicSmoothness);
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            Material _material = GetInputValue("Material", material);

            switch (port.Name)
            {
                case "Output": return _material;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Engine/Material/Handlers", Name = "Set Normal Map", Icon = "COMPONENT/ENGINE/MATERIAL")]
    [Output("Output", typeof(Material), Multiple = true)]
    public class OverSetNormal : OverMaterialHandlerNode
    {
        [Input("Material", Multiple = false)] public Material material;
        [Input("Normal Map")] public Texture2D normalMap;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Material _material = GetInputValue("Material", material);
            Texture2D _normalMap = GetInputValue("Normal Map", normalMap);

            if (_material != null)
            {
                if (_normalMap != null)
                {
                    _material.SetTexture("_BumpMap", _normalMap);
                }
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            Material _material = GetInputValue("Material", material);

            switch (port.Name)
            {
                case "Output": return _material;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Engine/Material/Handlers", Name = "Set Height Map", Icon = "COMPONENT/ENGINE/MATERIAL")]
    [Output("Output", typeof(Material), Multiple = true)]
    public class OverSetHeight : OverMaterialHandlerNode
    {
        [Input("Material", Multiple = false)] public Material material;
        [Input("Height Map")] public Texture2D heightMap;
        [Input("Height Value")] public float heightScale;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Material _material = GetInputValue("Material", material);
            Texture2D _heightMap = GetInputValue("Height Map", heightMap);
            float _heightScale = GetInputValue("Height Value", heightScale);

            if (_material != null)
            {
                if (_heightMap != null)
                {
                    _material.SetTexture("_ParallaxMap", _heightMap);
                    _material.SetFloat("_Parallax", Mathf.Clamp(_heightScale, 0, 1));
                }
            }

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            Material _material = GetInputValue("Material", material);

            switch (port.Name)
            {
                case "Output": return _material;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Engine/Material/Handlers", Name = "Set Occlusion Map", Icon = "COMPONENT/ENGINE/MATERIAL")]
    [Output("Output", typeof(Material), Multiple = true)]
    public class OverSetOcclusion : OverMaterialHandlerNode
    {
        [Input("Material", Multiple = false)] public Material material;
        [Input("Occlusion Map")] public Texture2D occlusionMap;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Material _material = GetInputValue("Material", material);
            Texture2D _occlusionMap = GetInputValue("Occlusion Map", occlusionMap);

            if (_material != null)
            {
                if (_occlusionMap != null)
                {
                    _material.SetTexture("_OcclusionMap", _occlusionMap);
                }
            }

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            Material _material = GetInputValue("Material", material);

            switch (port.Name)
            {
                case "Output": return _material;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Engine/Material/Handlers", Name = "Set Detail Mask", Icon = "COMPONENT/ENGINE/MATERIAL")]
    [Output("Output", typeof(Material), Multiple = true)]
    public class OverSetDetail : OverMaterialHandlerNode
    {
        [Input("Material", Multiple = false)] public Material material;
        [Input("Detail Mask")] public Texture2D detailMask;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Material _material = GetInputValue("Material", material);
            Texture2D _detailMask = GetInputValue("Detail Mask", detailMask);

            if (_material != null)
            {
                if (_detailMask != null)
                {
                    _material.SetTexture("_DetailMask", _detailMask);
                }
            }

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            Material _material = GetInputValue("Material", material);

            switch (port.Name)
            {
                case "Output": return _material;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Engine/Material/Handlers", Name = "Set Emission Map", Icon = "COMPONENT/ENGINE/MATERIAL")]
    [Output("Output", typeof(Material), Multiple = true)]
    public class OverSetEmission : OverMaterialHandlerNode
    {
        [Input("Material", Multiple = false)] public Material material;
        [Input("Emission Map")] public Texture2D emissionMap;
        [Input("Emission Color")] public Color emissionColor;
        [Input("Intensity")] public float intensity = 1;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Material _material = GetInputValue("Material", material);
            Texture2D _emissionMap = GetInputValue("Emission Map", emissionMap);
            Color _emissionColor = GetInputValue("Emission Color", emissionColor);
            float _intensity = GetInputValue("Intensity", intensity);

            if (_material != null && _material.IsKeywordEnabled("_EMISSION"))
            {
                if (_emissionMap != null)
                {
                    _material.SetTexture("_EmissionMap", _emissionMap);
                }
                _material.SetColor("_EmissionColor", _emissionColor * _intensity);
            }

            return base.Execute(data);
        }
        public override object OnRequestNodeValue(Port port)
        {
            Material _material = GetInputValue("Material", material);

            switch (port.Name)
            {
                case "Output": return _material;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Engine/Material/Handlers", Name = "Set Emission Flag", Icon = "COMPONENT/ENGINE/MATERIAL")]
    [Output("Output", typeof(Material), Multiple = true)]
    public class OverSetEmissionFlag : OverMaterialHandlerNode
    {
        [Input("Material", Multiple = false)] public Material material;
        [Input("Emission")] public bool emissionFlag;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            Material _material = GetInputValue("Material", material);
            bool _emissionFlag = GetInputValue("Emission", emissionFlag);

            if (_material != null)
            {
                if (_emissionFlag)
                {
                    _material.EnableKeyword("_EMISSION");
                }
                else
                {
                    _material.DisableKeyword("_EMISSION");
                }
            }

            material = _material;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            if (port.Name == "Output")
            {
                return material;
            }
            return base.OnRequestNodeValue(port);
        }
    }
}