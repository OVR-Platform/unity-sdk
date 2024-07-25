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
        public int sublistIndex;
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
                sublistIndex = sublistIndex,
            };
        }

        public OverVariableData ToScriptData()
        {
            return new OverVariableData(GUID, name, type, isGlobal, sublistIndex);
        }
    }

    [Serializable]
    public class OverGraphSubListData
    {
        public string groupName = "Sub List Group";
        public List<OverGraphVariableData> variables = new List<OverGraphVariableData>();
    }

    [Serializable]
    public class OverGraphData
    {
        public List<OverGraphVariableData> variables = new List<OverGraphVariableData>();

        public enum VariableListType { None = 0, Added, Removed }

        public event Action<VariableListType, OverGraphVariableData> OnVariablesListChanged;

        public List<OverGraphSubListData> SubLists { get => subLists; private set => subLists = value; }
        [SerializeField] private List<OverGraphSubListData> subLists = new List<OverGraphSubListData>();

        // ? The property is for debug only
        public SerializableDictionary<string, int> LookupDictionary { get => lookupDictionary; private set => lookupDictionary = value; }
        private SerializableDictionary<string, int> lookupDictionary = new SerializableDictionary<string, int>();

        public int TotalVariablesCount => SubLists.Sum(subList => subList.variables.Count);

        public OverGraphData() { }

        // Used for backwards-compatibility
        public void UpdateOldVariableList()
        {
            if (variables.Count > 0)
            {
                OverGraphSubListData newSublist = new OverGraphSubListData();
                SubLists.Add(newSublist);

                foreach (var item in variables)
                {
                    newSublist.variables.Add(item);
                    lookupDictionary.Add(item.GUID, SubLists.Count - 1);
                }

                variables.Clear();
            }
        }

        public void RebuildAndNotofyList()
        {
            RebuildLookupDictionary();
        }

        public void AddNewVariableToSubList(OverGraphVariableData newVariable, bool rebuildAndNotify)
        {
            //const int FIRST_INDEX = 0;
            OverGraphSubListData subListData = GetOrCreateSubListData(newVariable.sublistIndex);

            subListData.variables.Add(newVariable);

            if (rebuildAndNotify)
            {
                RebuildLookupDictionary();
                OnVariablesListChanged?.Invoke(VariableListType.Added, newVariable);
            }
        }

        public void AddNewVariableToSubList(OverGraphVariableData newVariable, int subListIndex)
        {
            if (!ContainsVariable(newVariable) && subListIndex < SubLists.Count)
            {
                SubLists[subListIndex].variables.Add(newVariable);

                RebuildLookupDictionary();

                OnVariablesListChanged?.Invoke(VariableListType.Added, newVariable);
            }
        }
        public void AddOrReplaceNewVariableToSubList(OverGraphVariableData newVariable, int subListIndex)
        {
            if (!ContainsVariable(newVariable) && subListIndex < SubLists.Count)
            {
                SubLists[subListIndex].variables.Add(newVariable);

                RebuildLookupDictionary();

                OnVariablesListChanged?.Invoke(VariableListType.Added, newVariable);
            }
            else
            {
                for (int i = 0; i < SubLists[subListIndex].variables.Count; i++)
                {
                    OverGraphVariableData item = SubLists[subListIndex].variables[i];
                    if (item.GUID == newVariable.GUID)
                    {
                        SubLists[subListIndex].variables[i] = newVariable;
                        break;
                    }
                }
            }
        }       

        public bool RemoveVariableFromSubList(string variableGUID)
        {
            int sublistIndex = GetSubListIndex(variableGUID);

            if (sublistIndex < 0)
                return false;

            int elementsRemoved = SubLists[sublistIndex].variables.RemoveAll(x => x.GUID.Contains(variableGUID));

            return elementsRemoved > 0;
        }

        public bool EditVariable(OverGraphVariableData variableToEdit)
        {
            bool foundVariable = GetVariableIndex(variableToEdit.GUID, out int sublistIndex, out int variableIndex);

            if (foundVariable)
            {
                SubLists[sublistIndex].variables[variableIndex] = variableToEdit;

                return true;
            }

            return false;
        }
      

        public void RebuildLookupDictionary()
        {
            lookupDictionary.Clear(); // Clear the existing dictionary to rebuild it

            for (int sublistIndex = 0; sublistIndex < SubLists.Count; sublistIndex++)
            {
                var sublist = SubLists[sublistIndex];
                foreach (var variable in sublist.variables)
                {
                    lookupDictionary[variable.GUID] = sublistIndex;
                }
            }
        }

        public List<OverGraphVariableData> GetVariablesList()
        {
            List<OverGraphVariableData> listToReturn = new List<OverGraphVariableData>();

            foreach (OverGraphSubListData variableList in SubLists)
            {
                listToReturn.AddRange(variableList.variables);
            }

            return listToReturn;
        }

        private OverGraphSubListData GetOrCreateSubListData(int sublistIndex)
        {
            // Ensure the index is within the bounds of the list size
            if (sublistIndex >= SubLists.Count)
            {
                int diff = (sublistIndex + 1) - SubLists.Count;
                // Add new OverGraphSubListData to extend the list to the desired index
                for (int i = 0; i < diff; i++)
                {
                    SubLists.Add(new OverGraphSubListData());
                }
            }

            // Return the OverGraphSubListData at the specified index
            // This will return the newly added element if the index was previously out of bounds
            return SubLists[sublistIndex];
        }

        public bool TryGetVariable(string variableGUID, out OverGraphVariableData foundVariable)
        {
            foundVariable = null; // Initialize the out parameter to null.

            // Attempt to get the index of the sublist containing the variable.
            if (lookupDictionary.TryGetValue(variableGUID, out int index))
            {
                // Ensure the index is within bounds of the SubLists.
                if (index < SubLists.Count)
                {
                    // Attempt to find the variable in the sub-list.
                    foundVariable = SubLists[index].variables.FirstOrDefault(x => x.GUID.Equals(variableGUID));

                    // Check if the variable was found.
                    return foundVariable != null;
                }
            }

            // Return false if the variable was not found.
            return false;
        }

        public bool ContainsVariable(OverGraphVariableData variableTocheck)
        {
            return lookupDictionary.ContainsKey(variableTocheck.GUID);
        }

        public bool ContainsVariable(string variableGUID)
        {
            return lookupDictionary.ContainsKey(variableGUID);
        }

        public List<OverVariableData> GetScriptList()
        {
            var listToReturn = new List<OverVariableData>();

            foreach (var variableList in SubLists)
            {
                IEnumerable<OverVariableData> dataToAdd = variableList.variables.Select(item => item.ToScriptData());
                listToReturn.AddRange(dataToAdd);
            }

            return listToReturn;
        }

        /// <summary>
        /// Check if a specific delegate is subscribed
        /// </summary>
        /// <returns>True if we are subscribed to OnVariablesListChanged event, otherwise false.</returns>
        public bool IsDelegateSubscribed(Action<VariableListType, OverGraphVariableData> delegateToCheck)
        {
            if (OnVariablesListChanged != null)
            {
                foreach (Delegate existingDelegate in OnVariablesListChanged.GetInvocationList())
                {
                    if (existingDelegate == (Delegate)delegateToCheck)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void ClearLists()
        {
            variables.Clear();
            SubLists.Clear();
            lookupDictionary.Clear();
        }

        public int GetSubListIndex(string variableGUID)
        {
            if (lookupDictionary.TryGetValue(variableGUID, out int variableIndex))
            {
                return variableIndex;
            }

            return -1;
        }

        public bool GetVariableIndex(string variableGUID, out int sublistIndex, out int variableIndex)
        {
            variableIndex = -1;
            sublistIndex = GetSubListIndex(variableGUID);

            if (sublistIndex < 0/* || SubLists.Count <= 0*/)
                return false;

            variableIndex = SubLists[sublistIndex].variables.FindIndex(x => x.GUID.Equals(variableGUID));

            return variableIndex >= 0;
        }
    }

    /******************************** OverGraph  **************************************/

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

        public void UpdateDataFromScript(OverScript script)
        {
            // Clear the variables of the graph
            Data.ClearLists();
            Data.RebuildLookupDictionary();

            // Add all script variables to the graph
            var dataToAdd = script.Data.GetGraphList();
            foreach (var data in dataToAdd)
            {
                int variableIndex = Data.GetSubListIndex(data.GUID);
                if (variableIndex >= 0)
                {
                    Data.AddNewVariableToSubList(data, variableIndex);
                }
                else
                {
                    Data.AddNewVariableToSubList(data, rebuildAndNotify: true);
                }
            }
            Data.RebuildLookupDictionary();

            //Data.variables.AddRange(dataToAdd);

            // Togli tutte le variabili che nel grafo non sono presenti nello script
            //remove surplus
            //RemoveSurplusLocalVariables(script);

            //Reset all globals as well.
            UpdateDataFromManager();

            //Notify to other scripts that use the same graph (serve quando vengono aggiunte variabili)
            if(Application.isEditor && !Application.isPlaying)
            {
                OverScriptManager scriptManager = OverScriptManager.Main;
                string graphGuid = GUID;

                if (scriptManager != null)
                {
                    foreach (OverScript overscript in scriptManager.managedScripts)
                    {
                        if (overscript.OverGraph.GUID == graphGuid && overscript.GUID != script.GUID)
                        {
                            overscript.OnUpdateFromGraphData(Data);
                        }
                    }
                }
            }          

            Data.RebuildLookupDictionary();

            ValidateInternalNodes();
        }

        public void UpdateDataFromManager()
        {
            List<OverGraphVariableData> managerData = OverScriptManager.Main.Data.GetGraphList();

            //For all current Data remove those not present in managerData (GUID).      
            List<OverGraphVariableData> toRemove = new List<OverGraphVariableData>();
            foreach (var variable in Data.GetVariablesList())
            {
                if (variable.isGlobal && !managerData.Any(x => x.GUID == variable.GUID && x.sublistIndex == variable.sublistIndex))
                {
                    toRemove.Add(variable);
                }
            }
            foreach (var variable in toRemove)
            {
                Data.RemoveVariableFromSubList(variable.GUID);
            }
            //RebuildlookApp
            Data.RebuildLookupDictionary();

            //
            foreach (var data in managerData)
            {
                int variableIndex = Data.GetSubListIndex(data.GUID);
                if (variableIndex >= 0)
                {
                    Data.AddOrReplaceNewVariableToSubList(data, variableIndex);
                }
                else
                {
                    Data.AddNewVariableToSubList(data, rebuildAndNotify: true);
                }
            }

            Data.RebuildLookupDictionary();

            ValidateInternalNodes();
        }

        public void RemoveSurplusLocalVariables(OverScript script)
        {
            var variableData = Data.GetVariablesList();
            var variablesToRemove = variableData
                .Where(variable => !script.Data.ContainsVariable(variable.GUID))
                .ToList();

            foreach (var variable in variablesToRemove)
            {
                Data.RemoveVariableFromSubList(variable.GUID);
            }
        }

        private void ValidateInternalNodes()
        {
            if (Application.isPlaying)
            {
                if (Application.isEditor)
                    Debug.Log("Non needed in play, but commment return for debug");

                return;
            }

            List<OverGetVariable> getNodes = GetGraphNodes<OverGetVariable>();
            List<OverSetVariable> setNodes = GetGraphNodes<OverSetVariable>();

            foreach (OverGetVariable node in getNodes)
            {
                if (node.Type == OverVariableType.None)
                {
                    Debug.LogError($"[OVER] None Type not supported!\nSource: [GET] {node.Name} ({node.guid})\nTo fix this error you need to change the node type in OverScript.", this);
                    node.Error = "[Error] None Type not supported!";
                    continue;
                }

                if (Data.TryGetVariable(node.guid, out var variable))
                {
                    if (node.Name != variable.name)
                    {
                        node.Name = variable.name;
                    }

                    if (node.Type != variable.type)
                    {
                        Debug.LogError($"[OVER] Node Type change detected!\nSource:[GET] {node.Name} ({node.guid})\nTo fix this error you need to manually reimport it back.", this);
                        node.Error = "[Error] Type mismatch. You need to reimport this variable";
                    }
                }
                else
                {
                    if (OverScriptManager.Main.Data.TryGetVariable(node.guid, out var globalVariable))
                    {
                        if (node.Name != globalVariable.name)
                        {
                            node.Name = globalVariable.name;
                        }

                        if (node.Type != globalVariable.type)
                        {
                            Debug.LogError($"[OVER] Node Type change detected!\nSource: [GET] {node.Name} ({node.guid})\nTo fix this error you need to manually reimport it back.", this);
                            node.Error = "[Error] Type mismatch. You need to reimport this variable";
                        }
                    }
                    else
                    {
                        Debug.LogError($"[OVER] Node Removed\nSource: [GET] {node.Name} ({node.guid})\nTo avoid inconsistencies erase this node from the OverGraph.", this);
                        node.Error = "[Error] This node has been removed from the Graph (Node GUID mismatch)";
                    }
                }
            }

            foreach (OverSetVariable node in setNodes)
            {
                if (node.Type == OverVariableType.None)
                {
                    Debug.LogError($"[OVER] None Type not supported!\nSource: [SET] {node.Name} ({node.guid})\nTo fix this error you need to change the node type in OverScript.", this);
                    node.Error = "[Error] None Type not supported!";
                    continue;
                }

                if (Data.TryGetVariable(node.guid, out var variable))
                {
                    if (node.Name != variable.name)
                    {
                        node.Name = variable.name;
                    }

                    if (node.Type != variable.type)
                    {
                        Debug.LogError($"[OVER] Node Type change detected!\nSource: [SET] {node.Name} ({node.guid})\nTo fix this error you need to manually reimport it back.", this);
                        node.Error = "[Error] Type mismatch. You need to reimport this variable";
                    }
                }
                else
                {
                    if (OverScriptManager.Main.Data.TryGetVariable(node.guid, out var globalVariable))
                    {
                        if (node.Name != globalVariable.name)
                        {
                            node.Name = globalVariable.name;
                        }

                        if (node.Type != globalVariable.type)
                        {
                            Debug.LogError($"[OVER] Node Type change detected!\nSource: [SET] {node.Name} ({node.guid})\nTo fix this error you need to manually reimport it back.", this);
                            node.Error = "[Error] Type mismatch. You need to reimport this variable";
                        }
                    }
                    else
                    {
                        Debug.LogError($"[OVER] Node Removed!\nSource: [SET] {node.Name} ({node.guid})\nTo avoid inconsistencies erase this node from the OverGraph.", this);
                        node.Error = "[Error] This node has been removed from the Graph (Node GUID mismatch)";
                    }
                }
            }
        }
    }
}