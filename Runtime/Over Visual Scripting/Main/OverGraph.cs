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
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using OverSimpleJSON;

namespace OverSDK.VisualScripting
{
    public class OverExecutionFlowData 
    {
        public string scritpGUID;
        public JSONNode others;
    }

    [Serializable]
    public class OverGraphVariableData
    {
        public string GUID;
        public string name;
        public OverVariableType type;
        public bool isGlobal;

        public OverGraphVariableData Clone()
        {
            return new OverGraphVariableData()
            {
                GUID = GUID,
                name = name,
                type = type,
                isGlobal = isGlobal,
            };
        }

        public OverVariableData ToScriptData()
        {
            return new OverVariableData()
            {
                GUID = GUID,
                name = name,
                type = type,
                isGlobal = isGlobal
            };
        }
    }

    /// <summary>
    /// Data class holding values relative to the Graph, more specifically local variables used by the graph
    /// </summary>
    [Serializable]
    public class OverGraphData
    {
        [OverGraphVariableList] public List<OverGraphVariableData> variables = new List<OverGraphVariableData>();
        private Dictionary<string, OverGraphVariableData> variableDict = new Dictionary<string, OverGraphVariableData>();
        public Dictionary<string, OverGraphVariableData> VariableDict
        {
            get
            {
                variableDict = new Dictionary<string, OverGraphVariableData>();
                foreach (OverGraphVariableData data in variables)
                {
                    variableDict.Add(data.GUID, data);
                }
                return variableDict;
            }
        }
    }

    [CreateAssetMenu(
        menuName = "OVER Unity SDK/OverGraph",
        fileName = "New OverGraph"
    )]
    [IncludeTags("Executable", "Monobehaviour", "Component", "Variable", "Data", "Common", "Event", "Utils", "Web", "Log")]
    public class OverGraph : Graph, IExecutableOverGraph
    {
        [SerializeField, HideInInspector] public string GUID;
        [SerializeField] public string GraphName => name;

        [SerializeField][ReadOnly] OverGraphData data;
        public OverGraphData Data
        {
            get
            {
                if (data == null) data = new OverGraphData();
                return data;
            }
            set { data = value; }
        }

        OverUpdate overUpdate;
        OverFixedUpdate overFixedUpdate;
        OverLateUpdate overLateUpdate;

        // OVERRIDE
        public override string Title => GraphName;

        // MonoBehaviour Nodes
        public void OnBehaviourAwake(OverScript script)
        {
            overUpdate = GetNode<OverUpdate>();
            overLateUpdate = GetNode<OverLateUpdate>();
            overFixedUpdate = GetNode<OverFixedUpdate>();
            List<OverEventNode> nodes = GetGraphNodes<OverEventNode>();
            foreach (var node in nodes)
            {
                node.Register(new OverExecutionFlowData
                {
                    scritpGUID = script.GUID
                });
            }

            Execute(GetNode<OverAwake>(), new OverExecutionFlowData
            {
                scritpGUID = script.GUID
            }); ;
        }

        public List<T> GetGraphNodes<T>() where T : Node
        {
            return GetNodes<T>().ToList();
        }

        public void OnBehaviourStart(OverScript script)
        {
            Execute(GetNode<OverStart>(), new OverExecutionFlowData
            {
                scritpGUID = script.GUID
            });
        }

        public void OnBehaviourEnable(OverScript script)
        {
            Execute(GetNode<OverEnable>(), new OverExecutionFlowData
            {
                scritpGUID = script.GUID
            });
        }

        public void OnBehaviourDisable(OverScript script)
        {
            Execute(GetNode<OverDisable>(), new OverExecutionFlowData
            {
                scritpGUID = script.GUID
            });
        }

        public void OnBehaviourDestroy(OverScript script)
        {
            Execute(GetNode<OverDestroy>(), new OverExecutionFlowData
            {
                scritpGUID = script.GUID
            });
        }

        public void OnBehaviourUpdate(OverScript script)
        {
            Execute(overUpdate, new OverExecutionFlowData
            {
                scritpGUID = script.GUID
            });
        }
        public void OnBehaviourLateUpdate(OverScript script)
        {
            Execute(overLateUpdate, new OverExecutionFlowData
            {
                scritpGUID = script.GUID
            });
        }
        public void OnBehaviourFixedUpdate(OverScript script)
        {
            Execute(overFixedUpdate, new OverExecutionFlowData
            {
                scritpGUID = script.GUID
            });
        }

        public override void OnGraphValidate()
        {
            List<OverGetVariable> getNodes = GetGraphNodes<OverGetVariable>();
            List<OverSetVariable> setNodes = GetGraphNodes<OverSetVariable>();

            foreach(var get in getNodes)
            {
                if (!Data.VariableDict.ContainsKey(get.guid) && get.Type != OverVariableType.None)
                {
                    Data.variables.Add(new OverGraphVariableData()
                    {
                        name = get.Name,
                        GUID = get.guid,
                        type = get.Type,
                        isGlobal = get.isGlobal
                    });
                }
            }

            foreach (var set in setNodes)
            {
                if (!Data.VariableDict.ContainsKey(set.guid) && set.Type != OverVariableType.None)
                {
                    Data.variables.Add(new OverGraphVariableData()
                    {
                        name = set.Name,
                        GUID = set.guid,
                        type = set.Type,
                        isGlobal = set.isGlobal
                    });
                }
            }

            base.OnGraphValidate();
        }

        //Interface

        public void Execute(IExecutableOverNode root, OverExecutionFlowData data)
        {
            IExecutableOverNode next = root;
            int iterations = 0;
            while (next != null)
            {
                next.PropagateFlowData(data);
                next = next.Execute(data);

                iterations++;
                if (iterations > 2000)
                {
                    Debug.LogError("Potential infinite loop detected. Stopping early.", this);
                    break;
                }
            }
        }

        ///
        public void UpdateDataFromScript(OverScript script)
        {
            this.Data.variables.Clear();
            this.Data.variables.AddRange(script.Data.variables.Select(item => item.ToGraphData()));

            //remove surplus
            RemoveSurplusLocalVariables(script);

            OverScriptManager scriptManager = OverScriptManager.Main;
            string graphGuid = this.GUID;

            if (scriptManager != null)
            {
                foreach(var overscript in scriptManager.managedScripts)
                {
                    if(overscript.OverGraph.GUID == graphGuid && overscript.GUID != script.GUID)
                    {
                        overscript.OnUpdateGraphData(Data);
                    }
                }
            }

            ValidateInternalNodes();
        }

        public void RemoveSurplusLocalVariables(OverScript script)
        {
            var variablesToRemove = Data.variables
                .Where(variable => !script.Data.VariableDict.ContainsKey(variable.GUID))
                .ToList();

            foreach (var variable in variablesToRemove)
            {
                Data.variables.Remove(variable);
            }
        }

        public void ValidateInternalNodes()
        {
            List<OverGetVariable> getNodes = GetGraphNodes<OverGetVariable>();
            List<OverSetVariable> setNodes = GetGraphNodes<OverSetVariable>();

            foreach (OverGetVariable node in getNodes)
            {
                if (node.Type == OverVariableType.None)
                {
                    Debug.LogError($"None Type not supported!\nSource: [GET] {node.Name} ({node.guid})\nTo fix this error you need to change the node type in OverScript.", this);
                    node.Error = "[Error] None Type not supported!";
                    continue;
                }

                if (Data.VariableDict.TryGetValue(node.guid, out var variable))
                {
                    if (node.Name != variable.name)
                    {
                        node.Name = variable.name;
                    }

                    if (node.Type != variable.type)
                    {
                        Debug.LogError($"Node Type change detected!\nSource:[GET] {node.Name} ({node.guid})\nTo fix this error you need to manually reimport it back.", this);
                        node.Error = "[Error] Type mismatch. You need to reimport this variable";
                    }
                }
                else
                {
                    if (OverScriptManager.Main.Data.VariableDict.TryGetValue(node.guid, out var globalVariable))
                    {
                        if (node.Name != globalVariable.name)
                        {
                            node.Name = globalVariable.name;
                        }

                        if (node.Type != globalVariable.type)
                        {
                            Debug.LogError($"Node Type change detected!\nSource: [GET] {node.Name} ({node.guid})\nTo fix this error you need to manually reimport it back.", this);
                            node.Error = "[Error] Type mismatch. You need to reimport this variable";
                        }
                    }
                    else
                    {
                        Debug.LogError($"Node Removed!\nSource: [GET] {node.Name} ({node.guid})\nTo avoid inconsistencies erase this node from the OverGraph.", this);
                        node.Error = "[Error] This node has been removed from the Graph (Node GUID mismatch)";
                    }
                }
            }

            foreach (OverSetVariable node in setNodes)
            {
                if (node.Type == OverVariableType.None)
                {
                    Debug.LogError($"None Type not supported!\nSource: [SET] {node.Name} ({node.guid})\nTo fix this error you need to change the node type in OverScript.", this);
                    node.Error = "[Error] None Type not supported!";
                    continue;
                }

                if (Data.VariableDict.TryGetValue(node.guid, out var variable))
                {
                    if (node.Name != variable.name)
                    {
                        node.Name = variable.name;
                    }

                    if (node.Type != variable.type)
                    {
                        Debug.LogError($"Node Type change detected!\nSource: [SET] {node.Name} ({node.guid})\nTo fix this error you need to manually reimport it back.", this);
                        node.Error = "[Error] Type mismatch. You need to reimport this variable";
                    }
                }
                else
                {
                    if(OverScriptManager.Main.Data.VariableDict.TryGetValue(node.guid, out var globalVariable))
                    {
                        if (node.Name != globalVariable.name)
                        {
                            node.Name = globalVariable.name;
                        }

                        if (node.Type != globalVariable.type)
                        {
                            Debug.LogError($"Node Type change detected!\nSource: [SET] {node.Name} ({node.guid})\nTo fix this error you need to manually reimport it back.", this);
                            node.Error = "[Error] Type mismatch. You need to reimport this variable";
                        }
                    }
                    else
                    {
                        Debug.LogError($"Node Removed!\nSource: [SET] {node.Name} ({node.guid})\nTo avoid inconsistencies erase this node from the OverGraph.", this);
                        node.Error = "[Error] This node has been removed from the Graph (Node GUID mismatch)";
                    }
                }
            }
        }
    }
}