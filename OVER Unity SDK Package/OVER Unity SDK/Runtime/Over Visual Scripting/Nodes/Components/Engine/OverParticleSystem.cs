/**
 * OVR Unity SDK License
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

    //[Node(Path = "Component/Engine/ParticleSystem", Name = "ParticleSystem Exposer", Icon = "COMPONENT/ENGINE/RENDERER")]
    //[Tags("Component")]
    //[Output("Ref", typeof(ParticleSystem), Multiple = true)]
    //public class OverParticleSystem : OverNode
    //{
    //    [Input("ParticleSystem", Multiple = false)] public ParticleSystem particleSystem;

    //    [Output("Main", Multiple = true)] public ParticleSystem.MainModule main;
    //    [Output("Emission", Multiple = true)] public ParticleSystem.EmissionModule emission;
    //    [Output("Shape", Multiple = true)] public ParticleSystem.ShapeModule shape;
    //    [Output("VelocityOverLifetime", Multiple = true)] public ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime;
    //    [Output("SizeOverLifetime", Multiple = true)] public ParticleSystem.SizeOverLifetimeModule sizeOverLifetime;
    //    [Output("ColorOverLifetime", Multiple = true)] public ParticleSystem.ColorOverLifetimeModule colorOverLifetime;
    //    [Output("SubEmitters", Multiple = true)] public ParticleSystem.SubEmittersModule subEmitters;
    //    [Output("Collision", Multiple = true)] public ParticleSystem.CollisionModule collision;
    //    [Output("Noise", Multiple = true)] public ParticleSystem.NoiseModule noise;
    //    [Output("Lights", Multiple = true)] public ParticleSystem.LightsModule lights;

    //    public override object OnRequestNodeValue(Port port)
    //    {
    //        ParticleSystem _particleSystem = GetInputValue("ParticleSystem", particleSystem);

    //        switch (port.Name)
    //        {
    //            case "Ref": return _particleSystem;
    //            case "Main": main = _particleSystem.main; return main;
    //            case "Emission": emission = _particleSystem.emission; return emission;
    //            case "Shape": shape = _particleSystem.shape; return shape;
    //            case "VelocityOverLifetime": velocityOverLifetime = _particleSystem.velocityOverLifetime; return velocityOverLifetime;
    //            case "SizeOverLifetime": sizeOverLifetime = _particleSystem.sizeOverLifetime; return sizeOverLifetime;
    //            case "ColorOverLifetime": colorOverLifetime = _particleSystem.colorOverLifetime; return colorOverLifetime;
    //            case "SubEmitters": subEmitters = _particleSystem.subEmitters; return subEmitters;
    //            case "Collision": collision = _particleSystem.collision; return collision;
    //            case "Noise": noise = _particleSystem.noise; return noise;
    //            case "Lights": lights = _particleSystem.lights; return lights;
    //        }

    //        return base.OnRequestNodeValue(port);
    //    }
    //}

    [Tags("Component")]
    public abstract class OverParticleSystemHandlerNode : OverExecutionFlowNode { }

    [Node(Path = "Component/Engine/ParticleSystem/Handlers", Name = "Play Particles", Icon = "COMPONENT/ENGINE/RENDERER")]
    public class OverPlayParticleSystem : OverRendererHandlerNode
    {
        [Input("ParticleSystem", Multiple = false)] public ParticleSystem particleSystem;
        [Input("Include Children", Multiple = false)] public bool includeChildren;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            ParticleSystem _particleSystem = GetInputValue("ParticleSystem", particleSystem);
            bool _includeChildren = GetInputValue("Include Children", includeChildren);

            _particleSystem?.Play(_includeChildren);

            return base.Execute(data);
        }
    }

    [Node(Path = "Component/Engine/ParticleSystem/Handlers", Name = "Stop Particles", Icon = "COMPONENT/ENGINE/RENDERER")]
    public class OverStopParticleSystem : OverRendererHandlerNode
    {
        [Input("ParticleSystem", Multiple = false)] public ParticleSystem particleSystem;
        [Input("Include Children", Multiple = false)] public bool includeChildren;

        [Editable("Stop Behaviour")] public ParticleSystemStopBehavior stopBehaviour;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            ParticleSystem _particleSystem = GetInputValue("ParticleSystem", particleSystem);
            bool _includeChildren = GetInputValue("Include Children", includeChildren);

            _particleSystem?.Stop(_includeChildren, stopBehaviour);

            return base.Execute(data);
        }
    }
}