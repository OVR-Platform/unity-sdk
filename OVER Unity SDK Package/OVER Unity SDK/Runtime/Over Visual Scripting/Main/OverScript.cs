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

using OverSimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OverSDK.VisualScripting
{
    public enum OverVariableType
    {
        None = 0,
        Bool = 1,
        Int = 2,
        Float = 3,
        Vector2 = 4,
        Vector3 = 5,
        Quaternion = 6,
        String = 7,

        Transform = 10,
        Object = 11,
        Renderer = 12,
        RectTransform = 13,
        LineRenderer = 14,
        Material = 15,
        ParticleSystem = 16,

        Rigidbody = 20,
        Collider = 21,
        CharacterController = 22,

        Light = 30,

        NavMeshAgent = 40,
        //NavMeshObstacle = 41,

        AudioSource = 50,
        Video = 51,
        Animator = 52,
        AudioClip = 53,
        ImageStreamer = 54,

        Text = 101,
        TextTMP = 102,
        TextTMP_3D = 106,
        Image = 103,
        RawImage = 104,
        Color = 105,

        List = 200,
        JSON = 201
    }

    public enum OverListDataType
    {
        None = 0,
        Bool = 1,
        Int = 2,
        Float = 3,
        Vector2 = 4,
        Vector3 = 5,
        Quaternion = 6,
        String = 7,

        Transform = 10,
        Object = 11,
        Renderer = 12,
        RectTransform = 13,
        LineRenderer = 14,
        Material = 15,
        ParticleSystem = 16,

        Rigidbody = 20,
        Collider = 21,
        CharacterController = 22,

        Light = 30,

        AudioSource = 50,
        Video = 51,
        Animator = 52,
        AudioClip = 53,

        //Button = 100,
        Text = 101,
        TextTMP = 102,
        TextTMP_3D = 103,
        Image = 104,
        RawImage = 105,
        Color = 106,

        List = 200
    }

    [Serializable]
    public class OverVariableData
    {
        public string GUID;
        public int sublistIndex;

        public string name;
        public OverVariableType type;

        public bool isGlobal;

        public int integerValue;
        public float floatValue;
        public string stringValue;
        public bool boolValue;

        public Vector2 vector2Value;
        public Vector3 vector3Value;
        public Quaternion QuaternionValue;

        public Transform transformValue;
        public GameObject gameObject;
        public Renderer renderer;
        public RectTransform rectTransform;
        public LineRenderer lineRenderer;

        public Material material;
        public ParticleSystem particleSystem;

        public Rigidbody rigidbodyValue;
        public Collider colliderValue;
        public CharacterController characterController;

        public Light light;

        public NavMeshAgent navMeshAgent;

        public AudioSource audioSource;
        public AudioClip audioClip;
        public VideoPlayer videoPlayer;
        public ImageStreamer imageStreamer;
        public Animator animator;

        public Text text;
        public TMPro.TextMeshProUGUI textTMP;
        public TMPro.TextMeshPro textTMP_3D;

        public Image image;
        public RawImage rawImage;
        public Color color;

        public OverList list;
        public string json;

        public OverVariableData(int sublistIndex)
        {
            GUID = Guid.NewGuid().ToString();
            type = OverVariableType.Bool;
            this.sublistIndex = sublistIndex;
        }
        public OverVariableData(string GUID, string name, OverVariableType type, bool isGlobal, int sublistIndex)
        {
            this.GUID = GUID;
            this.sublistIndex = sublistIndex;
            this.name = name;
            this.type = type;
            this.isGlobal = isGlobal;
        }

        public OverVariableData Clone()
        {
            OverVariableData overVariableData = new OverVariableData(0);

            overVariableData.GUID = GUID;
            overVariableData.sublistIndex = sublistIndex;
            overVariableData.name = name;
            overVariableData.type = type;
            overVariableData.isGlobal = isGlobal;
            overVariableData.integerValue = integerValue;
            overVariableData.floatValue = floatValue;
            overVariableData.stringValue = stringValue;
            overVariableData.boolValue = boolValue;
            overVariableData.vector2Value = vector2Value;
            overVariableData.vector3Value = vector3Value;
            overVariableData.QuaternionValue = QuaternionValue;
            overVariableData.transformValue = transformValue;
            overVariableData.gameObject = gameObject;
            overVariableData.renderer = renderer;
            overVariableData.rectTransform = rectTransform;
            overVariableData.lineRenderer = lineRenderer;
            overVariableData.material = material;
            overVariableData.particleSystem = particleSystem;

            overVariableData.audioSource = audioSource;
            overVariableData.audioClip = audioClip;
            overVariableData.videoPlayer = videoPlayer;
            overVariableData.imageStreamer = imageStreamer;
            overVariableData.animator = animator;

            overVariableData.rigidbodyValue = rigidbodyValue;
            overVariableData.colliderValue = colliderValue;
            overVariableData.characterController = characterController;
            overVariableData.light = light;

            overVariableData.navMeshAgent = navMeshAgent;

            overVariableData.text = text;
            overVariableData.textTMP = textTMP;
            overVariableData.image = image;
            overVariableData.rawImage = rawImage;
            overVariableData.color = color;

            overVariableData.list = list;
            overVariableData.json = json;

            return overVariableData;

        }

        public OverGraphVariableData ToGraphData()
        {
            return new OverGraphVariableData()
            {
                GUID = GUID,
                sublistIndex = sublistIndex,
                name = name,
                type = type,
                isGlobal = isGlobal
            };
        }
    }

    [Serializable]
    public class OverScriptSubListData
    {
        public string groupName = "Sub List Group";
        public List<OverVariableData> variables = new List<OverVariableData>();
    }

    [Serializable]
    public class OverScriptData
    {
        public enum VariableListType { None = 0, Added, Removed }

        // Old variables list, keep it for backwards-compatibility
        [HideInInspector] public List<OverVariableData> variables = new List<OverVariableData>();

        public event Action<VariableListType, OverVariableData> OnVariablesListChanged;

        public List<OverScriptSubListData> SubLists { get => subLists; private set => subLists = value; }
        [SerializeField] private List<OverScriptSubListData> subLists = new List<OverScriptSubListData>();

        private SerializableDictionary<string, int> lookupDictionary = new SerializableDictionary<string, int>();

        public int TotalVariablesCount => SubLists.Sum(subList => subList.variables.Count);

        public OverScriptData() { }

        /// <summary>
        /// Used for backwards-compatibility.
        /// </summary>
        public int UpdateOldVariableDataList()
        {
            int lastVariableCount = variables.Count;
            if (lastVariableCount > 0)
            {
                OverScriptSubListData sublist;
                if (SubLists.Count == 0)
                    sublist = new OverScriptSubListData();
                else
                    sublist = SubLists[0];//Prendo il primo sublist

                foreach (OverVariableData item in variables)
                {
                    item.sublistIndex = 0;
                    sublist.variables.Add(item);
                }

                if (SubLists.Count == 0)
                    SubLists.Add(sublist);

                //If not new not server update that is a class the sublist reference

                variables.Clear();

                RebuildLookupDictionary();
            }

            return lastVariableCount;
        }
        public bool HasSomeOldVariable => variables != null && variables.Count > 0;


        public void AddNewVariableToSubList(OverVariableData newVariable, bool rebuildAndNotify)
        {
            var subListData = GetOrCreateSubListData(newVariable.sublistIndex);

            subListData.variables.Add(newVariable);

            if (rebuildAndNotify)
            {
                RebuildLookupDictionary();
                OnVariablesListChanged?.Invoke(VariableListType.Added, newVariable);
            }
        }

        public void AddNewVariableToSubList(OverVariableData newVariable, int subListIndex)
        {
            if (!ContainsVariable(newVariable) &&
                subListIndex < SubLists.Count)
            {
                SubLists[subListIndex].variables.Add(newVariable);

                RebuildLookupDictionary();

                OnVariablesListChanged?.Invoke(VariableListType.Added, newVariable);
            }
        }

        public void AddEmptyVariableAtSubList(int sublistIndex, bool isGlobal = false)
        {
            OverVariableData emptyVariable = new OverVariableData(sublistIndex);
            emptyVariable.isGlobal = isGlobal;

            SubLists[sublistIndex].variables.Add(emptyVariable);

            RebuildLookupDictionary();

            OnVariablesListChanged?.Invoke(VariableListType.Added, emptyVariable);
        }


        public void AddNewSubListWithEmptyVariable(bool isGlobal = false)
        {
            OverScriptSubListData newSublist = new OverScriptSubListData();

            int newSublistIndex = SubLists.Count;//You don't need + one because it's an index
            OverVariableData emptyVariable = new OverVariableData(newSublistIndex);
            emptyVariable.isGlobal = isGlobal;

            newSublist.variables.Add(emptyVariable);

            SubLists.Add(newSublist);
            lookupDictionary.Add(emptyVariable.GUID, SubLists.Count - 1);

            OnVariablesListChanged?.Invoke(VariableListType.Added, emptyVariable);
        }

        public void RemoveElementAtSubList(int rootIndex, int childIndex)
        {
            if (rootIndex >= 0 && rootIndex < SubLists.Count)
            {
                var sublist = SubLists[rootIndex];
                if (childIndex >= 0 && childIndex < sublist.variables.Count)
                {
                    // Remove the element from the sublist
                    OverVariableData variableToRemove = sublist.variables[childIndex];
                    sublist.variables.RemoveAt(childIndex);

                    // Remove the element's entry from the dictionary
                    lookupDictionary.Remove(variableToRemove.GUID);

                    // Check if the sublist is now empty and remove it if so
                    if (sublist.variables.Count == 0)
                    {
                        SubLists.RemoveAt(rootIndex);
                        ResetAllVariableSublistIndex();

                        // Since a sublist was removed, update the dictionary to reflect the new indices
                        RebuildLookupDictionary();
                    }

                    OnVariablesListChanged?.Invoke(VariableListType.Removed, variableToRemove);
                }
            }
        }

        public void RemoveSubListElement(int sublistIndex)
        {
            var listToRemove = SubLists[sublistIndex];

            for (int i = 0; i < listToRemove.variables.Count; i++)
            {
                lookupDictionary.Remove(listToRemove.variables[i].GUID);
            }

            // Trigger the event for all variables that will be removed
            for (int i = 0; i < listToRemove.variables.Count; i++)
            {
                RemoveElementAtSubList(sublistIndex, i);
                //OnVariablesListChanged?.Invoke(VariableListType.Removed, listToRemove.variables[i]);
            }

            // Remove item from subLists
            SubLists.RemoveAt(sublistIndex);
            ResetAllVariableSublistIndex();

            UpdateDictionaryIndexes(sublistIndex);
        }

        private void UpdateDictionaryIndexes(int removedSublistIndex)
        {
            // Iterate over all sublists that come after the removed one
            for (int i = removedSublistIndex; i < SubLists.Count; i++)
            {
                var sublist = SubLists[i];
                foreach (var variable in sublist.variables)
                {
                    // Decrement the index of each variable in the dictionary
                    if (lookupDictionary.ContainsKey(variable.GUID))
                    {
                        lookupDictionary[variable.GUID]--;
                    }
                    else
                    {
                        Debug.LogError($"[Over] Variable not present in the dictionary.");
                    }
                }
            }
        }

        public void ReorderSublist(int oldIndex, int newIndex)
        {
            Debug.Log("Not implemented");

            if (false)
            {
                // Step 1: Reorder the sublist
                var item = SubLists[oldIndex];
                SubLists.RemoveAt(oldIndex);
                ResetAllVariableSublistIndex();
                SubLists.Insert(newIndex, item);

                // Step 2: Rebuild the lookup dictionary to reflect the new order
                RebuildLookupDictionary();
            }
        }

        public void ResetAllVariableSublistIndex()
        {
            for (int i = 0; i < SubLists.Count; i++)
            {
                for (int j = 0; j < SubLists[i].variables.Count; j++)
                {
                    SubLists[i].variables[j].sublistIndex = i;
                }
            }
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

        public List<OverVariableData> GetVariablesList()
        {
            var listToReturn = new List<OverVariableData>();

            foreach (var variableList in SubLists)
            {
                listToReturn.AddRange(variableList.variables);
            }

            return listToReturn;
        }

        private OverScriptSubListData GetOrCreateSubListData(int sublistIndex)
        {
            // Ensure the index is within the bounds of the list size
            if (sublistIndex >= SubLists.Count)
            {
                // Add new OverScriptSubListData to extend the list to the desired index
                SubLists.Add(new OverScriptSubListData());
            }

            // Return the OverScriptSubListData at the specified index
            // This will return the newly added element if the index was previously out of bounds
            return SubLists[sublistIndex];
        }

        public bool TryGetVariable(string variableGUID, out OverVariableData foundVariable)
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

        public bool ContainsVariable(OverVariableData variableTocheck)
        {
            return lookupDictionary.ContainsKey(variableTocheck.GUID);
        }

        public bool ContainsVariable(string variableGUID)
        {
            return lookupDictionary.ContainsKey(variableGUID);
        }

        public List<OverGraphVariableData> GetGraphList()
        {
            List<OverGraphVariableData> listToReturn = new List<OverGraphVariableData>();

            foreach (var variableList in SubLists)
            {
                IEnumerable<OverGraphVariableData> dataToAdd = variableList.variables.Select(item => item.ToGraphData());
                listToReturn.AddRange(dataToAdd);
            }

            return listToReturn;
        }

        /// <summary>
        /// Check if a specific delegate is subscribed
        /// </summary>
        /// <returns>True if we are subscribed to OnVariablesListChanged event, otherwise false.</returns>
        public bool IsDelegateSubscribed(Action<VariableListType, OverVariableData> delegateToCheck)
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
            SubLists.Clear();
            lookupDictionary.Clear();
        }
    }

    public class OverScript : MonoBehaviour
    {
        [ReadOnly] public string GUID;

        [SerializeField][ReadOnly] OverGraph overGraph;
        public OverGraph OverGraph { get { return overGraph; } set { overGraph = value; OnOverGraphChanged(); } }

        [SerializeField] private OverScriptData data;
        public OverScriptData Data
        {
            get
            {
                if (data == null) data = new OverScriptData();
                return data;
            }
            set { data = value; }
        }

        public bool MarkedAsDirty { get; set; }
        private bool isReloading = false;

        private bool variableListChangInPending = false;

        // MONOBEHAVIOURS
        private void OnValidate()
        {
            CheckIfUpdateReferenceAndOldVariables();
            SetVariablesToGraph();
        }

        void Awake()
        {
            //Data.UpdateOldVariableList();
            Debug.Log("Awake OverScript");

            //updating mappings
            if (string.IsNullOrEmpty(GUID))
                GUID = Guid.NewGuid().ToString();

            if (OverScriptManager.Main != null)
            {
                if (!OverScriptManager.Main.overScriptsReferences.ContainsKey(GUID))
                {
                    OverScriptManager.Main.UpdateScriptReferences();
                    UpdateOldVariableList();
                }
                else if (OverScriptManager.Main.overScriptsReferences[GUID].overScript != this) // OverScriptManager.Main.overScriptsReferences[GUID].overGraphAsset.GUID == OverGraph.GUID)
                {
                    GUID = Guid.NewGuid().ToString();
                    OverScriptManager.Main.UpdateScriptReferences();
                    UpdateOldVariableList();
                }


                // Because Unity reselialize when changing play mode
                Data.RebuildLookupDictionary();

                overGraph = overGraph.Clone();

                if (OverGraph != null && !OverScriptManager.Main.IsError)
                    OverGraph.OnBehaviourAwake(this);
            }
        }

        void Start()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourStart(this);
        }

        void OnEnable()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourEnable(this);
        }

        void OnDisable()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourDisable(this);
        }

        void OnDestroy()
        {
            if (OverScriptManager.Main != null)
            {
                OverScriptManager.Main.UpdateScriptReferences();

                if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourDestroy(this);
            }
        }

        // Updates
        void Update()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourUpdate(this);
        }

        void LateUpdate()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourLateUpdate(this);
        }

        void FixedUpdate()
        {
            if (OverGraph != null && !OverScriptManager.Main.IsError) OverGraph.OnBehaviourFixedUpdate(this);
        }

        private void OnOverGraphChanged()
        {
            if (OverGraph != null && string.IsNullOrEmpty(OverGraph.GUID))
                OverGraph.GUID = System.Guid.NewGuid().ToString();

            if (OverScriptManager.Main == null)
            {
                Debug.LogError("No manager found! Create one first in order to use variables.");
            }

            ReloadVariableFormGraph();
        }

        private void ReloadVariableFormGraph()
        {
            isReloading = true;

            if (OverGraph != null &&
                string.IsNullOrEmpty(OverGraph.GUID))
            {
                OverGraph.GUID = Guid.NewGuid().ToString();
            }

            if (OverGraph != null)
            {
                //verify locals
                List<OverGraphVariableData> variableNodes = OverGraph.Data.GetVariablesList();

                foreach (OverGraphVariableData variableNode in variableNodes)
                {
                    OverVariableData data = variableNode.ToScriptData();

                    if (!Data.ContainsVariable(data.GUID) && !data.isGlobal)
                    {
                        Data.AddNewVariableToSubList(data, rebuildAndNotify: false);
                    }
                }

                Data.RebuildLookupDictionary();
            }

            isReloading = false;
            MarkedAsDirty = true;
        }

        private void SetVariablesToGraph(bool forceUpdate = false)
        {
            if (!isReloading && (variableListChangInPending || forceUpdate))
            {
                // Update graph variables using this script
                if (overGraph != null)
                {
                    overGraph.UpdateDataFromScript(this);

                    if (Data.TotalVariablesCount == 0)
                        overGraph.Data.ClearLists();
                }

                variableListChangInPending = false; //Che schifo
            }

        }

        private void CheckIfUpdateReferenceAndOldVariables()
        {
            if (string.IsNullOrEmpty(GUID))
                GUID = Guid.NewGuid().ToString();

            if (OverScriptManager.Main != null)
            {
                if (gameObject != null && !string.IsNullOrEmpty(gameObject.scene.name) && gameObject.scene.name.Equals(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name))
                {
                    //Sono sicuro non sia un prefab
                    if (!OverScriptManager.Main.overScriptsReferences.ContainsKey(GUID))
                    {
                        OverScriptManager.Main.UpdateScriptReferences();
                        UpdateOldVariableList();//Lo faccio solo in questo caso perche so che l'ha gia fatto altrimenti il manager
                    }
                    else if (OverScriptManager.Main.overScriptsReferences[GUID].overScript != this) // OverScriptManager.Main.overScriptsReferences[GUID].overGraphAsset.GUID == OverGraph.GUID)
                    {
                        GUID = Guid.NewGuid().ToString();

                        OverScriptManager.Main.UpdateScriptReferences();
                        UpdateOldVariableList();
                    }
                    else
                    {
                        if (Application.isEditor)
                            UpdateOldVariableList(forceRestoreOverScriptManager: true);
                    }
                }
                else
                {
                    if (Application.isEditor)
                        UpdateOldVariableList(forceRestoreOverScriptManager: true);
                }
            }
            else
            {
                if (Application.isEditor)
                    UpdateOldVariableList(forceRestoreOverScriptManager: true);

            }
        }
        public void UpdateOldVariableList(bool forceRestoreOverScriptManager = false)
        {
            if (Data.HasSomeOldVariable)
            {
                if (gameObject != null && !string.IsNullOrEmpty(gameObject.scene.name) && gameObject.scene.name.Equals(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name))
                {
                    if (forceRestoreOverScriptManager)
                        OverScriptManager.Main.UpdateScriptReferences();

                    int result = Data.UpdateOldVariableDataList();

                    if (result > 0)
                    {
                        SetVariablesToGraph(forceUpdate: true);  //Change graph variables
                        ReloadVariableFormGraph();//Reload from graph to align
                        Data.RebuildLookupDictionary();
                    }
                }
                else
                {
                    Debug.Log("Some variables from an old version of OVER SDK found, please open this object in the scene to restore it");
                }
            }
        }

        public void ClearData()
        {
            Data.ClearLists();
        }

        public void OnUpdateGraphData(OverGraphData graphData)
        {
            Data.ClearLists();

            List<OverGraphVariableData> variablesList = graphData.GetVariablesList();

            foreach (OverGraphVariableData data in variablesList)
            {
                if (data.isGlobal)
                    continue;

                if (!Data.TryGetVariable(data.GUID, out OverVariableData variableData))
                {
                    OverVariableData overVariableData = data.ToScriptData();
                    Data.AddNewVariableToSubList(overVariableData, rebuildAndNotify: false);
                }
            }
            MarkedAsDirty = true;
        }

        public void ApplyVariableChangesTo(OverVariableData variable, OverGraph graph)
        {
            bool hasChanged = graph.Data.EditVariable(variable.ToGraphData());
            if (hasChanged is false)
            {
                graph.Data.AddNewVariableToSubList(variable.ToGraphData(), rebuildAndNotify: true);
            }

            graph.UpdateDataFromScript(this);

            MarkedAsDirty = true;
        }

        public void SendWebRequest(IEnumerator webRequestCoroutine)
        {
            StartCoroutine(webRequestCoroutine);
        }

        public void UpdateVariableChangedSubscription(bool subscribe)
        {
            if (data != null)
            {
                bool isSubscribed = data.IsDelegateSubscribed(OverScriptData_OnVariablesListChanged);

                if (subscribe is true &&
                    isSubscribed is false)
                {
                    // Subscribe to the event
                    data.OnVariablesListChanged -= OverScriptData_OnVariablesListChanged;
                    data.OnVariablesListChanged += OverScriptData_OnVariablesListChanged;
                }
                else if (subscribe is false &&
                         isSubscribed is true)
                {
                    // Unsubscribe from the event
                    data.OnVariablesListChanged -= OverScriptData_OnVariablesListChanged;
                }
            }
        }

        private void OverScriptData_OnVariablesListChanged(OverScriptData.VariableListType variableListType, OverVariableData overVariableData)
        {
            if (!isReloading && overGraph != null)
            {
                overGraph.UpdateDataFromScript(this);
                // Not sure is needed
                //overGraph.ValidateInternalNodes();
            }
            else
            {
                variableListChangInPending = true;
            }

            ApplyVariableChangesTo(overVariableData, OverGraph);
        }


    }
}
