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
using UnityEngine;
using UnityEngine.AI;

namespace OverSDK.VisualScripting 
{
    [Node(Path = "Component/Navigation", Name = "NavMeshAgent Exposer", Icon = "COMPONENT/NAVIGATION")]
    [Tags("Component")]
    [Output("Ref", typeof(NavMeshAgent), Multiple = true)]
    public class OverNavMeshAgent : OverNode
    {
        [Input("NavMeshAgent", Multiple = false)] public NavMeshAgent agent;

        [Output("Velocity", Multiple = true)] public Vector3 velocity;
        [Output("Desired Velocity", Multiple = true)] public Vector3 desiredVelocity;

        public override object OnRequestNodeValue(Port port)
        {
            NavMeshAgent _agent = GetInputValue("Transform", agent);

            switch (port.Name)
            {
                case "Ref":
                    return _agent;
                case "Velocity":
                    velocity = _agent.velocity;
                    return velocity;
                case "Desired Velocity":
                    desiredVelocity = _agent.desiredVelocity;
                    return desiredVelocity;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Tags("Component")]
    public abstract class OverNavigationHandlerNode : OverExecutionFlowNode { }

    [Node(Path = "Component/Navigation/Handlers", Name = "Set Destination", Icon = "COMPONENT/NAVIGATION")]
    [Output("Agent", typeof(NavMeshAgent), Multiple = true)]
    public class OverSetDestination : OverNavigationHandlerNode
    {
        [Input("NavMeshAgent")] public NavMeshAgent agent;
        [Input("Destination")] public Vector3 destination;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            NavMeshAgent _agent = GetInputValue("NavMeshAgent", agent);
            Vector3 _destination = GetInputValue("Destination", destination);

            _agent.SetDestination(_destination);
            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            NavMeshAgent _agent = GetInputValue("NavMeshAgent", agent);
            switch (port.Name)
            {
                case "Agent": return _agent;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Navigation/Handlers", Name = "Warp To", Icon = "COMPONENT/NAVIGATION")]
    [Output("Agent", typeof(NavMeshAgent), Multiple = true)]
    public class OverWarp : OverNavigationHandlerNode
    {
        [Input("NavMeshAgent")] public NavMeshAgent agent;
        [Input("Destination")] public Vector3 destination;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            NavMeshAgent _agent = GetInputValue("NavMeshAgent", agent);
            Vector3 _destination = GetInputValue("Destination", destination);

            _agent.Warp(_destination);
            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            NavMeshAgent _agent = GetInputValue("NavMeshAgent", agent);
            switch (port.Name)
            {
                case "Agent": return _agent;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Component/Navigation/Handlers", Name = "Move Agent", Icon = "COMPONENT/NAVIGATION")]
    [Output("Agent", typeof(NavMeshAgent), Multiple = true)]
    public class OverMoveAgent : OverNavigationHandlerNode
    {
        [Input("NavMeshAgent")] public NavMeshAgent agent;
        [Input("Offset")] public Vector3 destination;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            NavMeshAgent _agent = GetInputValue("NavMeshAgent", agent);
            Vector3 _destination = GetInputValue("Offset", destination);

            _agent.Move(_destination);
            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            NavMeshAgent _agent = GetInputValue("NavMeshAgent", agent);
            switch (port.Name)
            {
                case "Agent": return _agent;
            }

            return base.OnRequestNodeValue(port);
        }
    }
}

