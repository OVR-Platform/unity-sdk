using BlueGraph.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OverSDK.VisualScripting.Editor
{
    [CustomEditor(typeof(OverScriptManager))]
    public class OverScriptManagerEditor : UnityEditor.Editor
    {
        string dir = "Assets/OverScripts";
        private bool mRunningInEditor;

        private static OverScriptManager overScriptManager;
        private ReorderableList mainList;
        private Dictionary<int, ReorderableList> subLists = new Dictionary<int, ReorderableList>();

        private bool isEditingHeader = false;
        private string editingHeaderName = "";
        private int editingIndex = -1;

        private static bool isPlayModeChanging = false;

        private SerializedProperty managedScripts;
        private bool showManagedScriptsFoldout = true;

        /******************** Mono ********************/

        //GUI
        public override void OnInspectorGUI()
        {

            serializedObject.Update();
          
            {
                DrawManagedScripts();

                bool isInActualScene = overScriptManager != null && overScriptManager.gameObject != null && !string.IsNullOrEmpty(overScriptManager.gameObject.scene.name) && overScriptManager.gameObject.scene.name.Equals(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

                if (!isInActualScene && overScriptManager.Data != null && overScriptManager.Data.HasSomeOldVariable)
                {
                    GUI.enabled = false;
                    if (overScriptManager != null)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            // Use PrefixLabel to ensure proper alignment
                            EditorGUILayout.HelpBox("Some variables from an old version of OVER SDK found, please open this object in the scene to restore it", MessageType.Warning);

                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    GUI.enabled = true;
                }
                else
                {
                    RefreshReorderableList();
                }

            }

            EditorUtility.SetDirty(overScriptManager);

            serializedObject.ApplyModifiedProperties();
        }

        //MONO
        void Awake()
        {
            mRunningInEditor = Application.isEditor && !Application.isPlaying;

            if (mRunningInEditor)
            {
                if (OverScriptManager.Main == null)
                {
                    OverVisualScriptingInstantiator.InstantiateOverScriptManager();
                }
            }
        }

        private void OnEnable()
        {
            InitializeManagedScripts();
            InitializeReordableList();
        }

        public void OnDestroy()
        {
            if (mRunningInEditor &&
                target == null)
            {
                if (OverScriptManager.Main != null)
                    OverScriptManager.Main.UpdateScriptReferences();
            }
        }

        /******************** Init ********************/

        private void InitializeManagedScripts()
        {
            managedScripts = serializedObject.FindProperty("managedScripts");
        }

        private void InitializeReordableList()
        {
            overScriptManager = (OverScriptManager)target;
            DrawMainList();
        }

        [InitializeOnLoadMethod]
        static void OnProjectLoadedInEditor()
        {
            EditorApplication.hierarchyChanged -= OnHierarchyChanged;
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        /******************** Reordable Lists ********************/

        private void DrawMainList()
        {
            mainList = new ReorderableList(serializedObject, serializedObject.FindProperty("data").FindPropertyRelative("subLists"), true, true, true, true)
            {
                drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, "Variables");
                },
                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = mainList.serializedProperty.GetArrayElementAtIndex(index);

                    // Ensure sublist is initialized for this index
                    if (!subLists.ContainsKey(index))
                    {
                        CreateSubReorderableList(index, element.FindPropertyRelative("variables"));
                    }

                    // Draw the sublist
                    DrawSubList(rect, index, subLists[index]);
                },
                elementHeightCallback = (int index) =>
                {
                    float height = EditorGUIUtility.singleLineHeight; // Basic height for each element
                    if (subLists.ContainsKey(index))
                    {
                        height += subLists[index].GetHeight(); // Add the height of the sublist
                    }
                    return height;
                },
                onAddCallback = (ReorderableList l) =>
                {
                    overScriptManager.Data.AddNewSubListWithEmptyVariable(isGlobal: true);
                },
                onRemoveCallback = (ReorderableList l) =>
                {
                    overScriptManager.Data.RemoveSubListElement(l.index);
                },
                onReorderCallbackWithDetails = (ReorderableList list, int oldIndex, int newIndex) =>
                {
                    overScriptManager.Data.ReorderSublist(oldIndex, newIndex);
                }
            };
        }

        private void DrawSubList(Rect rect, int index, ReorderableList sublist)
        {
            sublist.elementHeight = EditorGUIUtility.singleLineHeight + 40;
            sublist.drawHeaderCallback = (Rect rect) =>
            {
                DrawRenamerHeader(index, rect);
            };
            sublist.drawElementCallback = (Rect r, int i, bool active, bool focused) =>
            {
                DrawVariableElement(r, index, i);
            };

            sublist.onAddCallback = (ReorderableList l) =>
            {
                overScriptManager.Data.AddEmptyVariableAtSubList(index, isGlobal: true);
            };
            sublist.onRemoveCallback = (ReorderableList l) =>
            {
                overScriptManager.Data.RemoveElementAtSubList(index, l.index);
            };
            sublist.DoList(new Rect(rect.x, rect.y + 4, rect.width, sublist.GetHeight()));
        }

        /******************** GUI ********************/

        private void DrawManagedScripts()
        {
            // Toggle the foldout to show or hide the managed scripts list
            showManagedScriptsFoldout = EditorGUILayout.Foldout(showManagedScriptsFoldout, "Script References", true);

            if (showManagedScriptsFoldout)
            {
                using (new EditorGUI.DisabledGroupScope(true))
                {
                    // Loop through each element in the list
                    for (int i = 0; i < managedScripts.arraySize; i++)
                    {
                        SerializedProperty listElement = managedScripts.GetArrayElementAtIndex(i);
                        EditorGUILayout.ObjectField(listElement.objectReferenceValue, typeof(MonoBehaviour), allowSceneObjects: true);
                    }
                }
            }

            EditorGUILayout.Space(12f);
        }

        private void RefreshReorderableList()
        {
            if (mainList != null && !mainList.Equals(null))
                mainList.DoLayoutList();
        }

        private void DrawRenamerHeader(int index, Rect rect)
        {
            string controlName = "HeaderTextField" + index;

            // Check if currently editing this header
            if (isEditingHeader && editingIndex == index)
            {
                GUI.SetNextControlName(controlName);
                editingHeaderName = EditorGUI.TextField(rect, editingHeaderName);

                if (Event.current.type == EventType.Repaint)
                {
                    GUI.FocusControl(controlName);
                }

                if ((Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return) ||
                    (Event.current.type == EventType.MouseDown && !rect.Contains(Event.current.mousePosition)))
                {
                    overScriptManager.Data.SubLists[index].groupName = editingHeaderName;
                    isEditingHeader = false;
                    editingIndex = -1;
                    GUIUtility.keyboardControl = 0; // Remove focus from text field
                    Event.current.Use();
                }
            }
            else
            {
                if (index >= overScriptManager.Data.SubLists.Count)
                {
                    //
                }
                else
                {
                    EditorGUI.LabelField(rect, overScriptManager.Data.SubLists[index].groupName);

                    // Detect double-click and right-click for renaming
                    if ((Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition) &&
                        Event.current.clickCount == 2) ||
                        Event.current.type == EventType.ContextClick && rect.Contains(Event.current.mousePosition))
                    {
                        if (Event.current.type == EventType.ContextClick)
                        {
                            // Display context menu for renaming
                            GenericMenu menu = new GenericMenu();
                            menu.AddItem(new GUIContent("Rename"), false, () =>
                            {
                                isEditingHeader = true;
                                editingIndex = index;
                                editingHeaderName = overScriptManager.Data.SubLists[index].groupName;
                                // Set focus on the next Repaint
                            });
                            menu.ShowAsContext();
                            Event.current.Use();
                        }
                        else
                        {
                            // Double-click detected, start editing
                            isEditingHeader = true;
                            editingIndex = index;
                            editingHeaderName = overScriptManager.Data.SubLists[index].groupName;
                            // Focus will be requested on the next Repaint event
                        }
                    }
                }
            }
        }

        private void CreateSubReorderableList(int index, SerializedProperty property)
        {
            var sublist = new ReorderableList(property.serializedObject, property, true, true, true, true)
            {
                drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, "Variables");
                },
                drawElementCallback = (Rect rect, int i, bool isActive, bool isFocused) =>
                {
                    var element = property.GetArrayElementAtIndex(i);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
                }
            };
            subLists.Add(index, sublist);
        }

        private void DrawVariableElement(Rect rect, int rootIndex, int elementIndex)
        {
            if (rootIndex >= overScriptManager.Data.SubLists.Count)
                return;

            var rootElement = overScriptManager.Data.SubLists[rootIndex];
            var childElement = rootElement.variables[elementIndex];

            float x = rect.x;
            float initialPadding = 0f; // Adjust this value to add more or less space
            float y = rect.y + initialPadding;

            float width = rect.width;
            float height = EditorGUIUtility.singleLineHeight;

            float totalWidth = width;
            float spacingPercentage = 0.02f; // 2% of the total width for spacing
            float space = totalWidth * spacingPercentage; // Calculate the space width 

            float availableWidth = totalWidth - (space * 2);

            float textFieldWidth = availableWidth * 0.47f;
            float enumPopupWidth = availableWidth * 0.47f;
            float buttonWidth = availableWidth * 0.06f;

            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    var nameRect = new Rect(x, y, textFieldWidth, height);
                    var typeRect = new Rect(x + textFieldWidth + space, y, enumPopupWidth, height);
                    var buttonRect = new Rect(x + textFieldWidth + space + enumPopupWidth + space, y, buttonWidth, height);

                    // Variable name
                    EditorGUI.BeginChangeCheck();
                    string nameValue = EditorGUI.TextField(nameRect, childElement.name);
                    if (EditorGUI.EndChangeCheck())
                    {
                        // Salva la variabile quando finisci di scrivere nel campo di testo
                        childElement.name = nameValue;
                        overScriptManager.ApplyVariableChangesTo(childElement);
                    }

                    // Variable type
                    EditorGUI.BeginChangeCheck();
                    {
                        childElement.type = (OverVariableType)EditorGUI.EnumPopup(typeRect, childElement.type);
                    }
                    bool hasChanged = EditorGUI.EndChangeCheck();
                    if (hasChanged)
                    {
                        overScriptManager.ApplyVariableChangesTo(childElement);
                    }

                    // Variable type selector
                    DrawTypeSelector(childElement, buttonRect);
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    var variableRect = new Rect(x, y, totalWidth, EditorGUIUtility.singleLineHeight);
                    DrawVariableValue(childElement, variableRect);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawTypeSelector(OverVariableData childElement, Rect buttonPosition)
        {
            if (GUI.Button(buttonPosition, new GUIContent("\u25CE", "Open enum selector window"), EditorStyles.miniButton))
            {
                // Get the names and values of the enum
                var enumList = Enum.GetNames(typeof(OverVariableType)).ToList();
                Array enumValues = Enum.GetValues(typeof(OverVariableType));

                // Convert the enum values to a list of integers
                List<int> enumIntValues = new List<int>();
                foreach (var enumVal in enumValues)
                {
                    enumIntValues.Add((int)enumVal);
                }

                // Find the index of the specific enum value in the list of enum values
                int selectedIndex = enumIntValues.IndexOf((int)childElement.type);

                // Init the window
                OverBetterEnumWindow window = (OverBetterEnumWindow)EditorWindow.GetWindow(typeof(OverBetterEnumWindow), utility: true, title: $"{childElement.type}", focus: true);
                window.UpdateEnumValue(enumList);

                // Update the current selection using the found index
                window.UpdateCurrentSelection(selectedIndex);

                //    // Subscribe to his events
                window.OnItemChosen += BetterEnumWindow_OnItemChosen;
                window.OnWindowCloses += BetterEnumWindow_OnWindowCloses;

                // Show the window
                window.Focus();
                window.Show();

                void BetterEnumWindow_OnWindowCloses(OverBetterEnumWindow windowClosed)
                {
                    windowClosed.OnWindowCloses -= BetterEnumWindow_OnWindowCloses;
                    windowClosed.OnItemChosen -= BetterEnumWindow_OnItemChosen;
                }

                void BetterEnumWindow_OnItemChosen(string enumItem)
                {
                    var currentElementSelected = enumList.FindIndex(x => x == enumItem);

                    if (Enum.TryParse(enumItem, out OverVariableType enumValue))
                    {
                        childElement.type = enumValue;
                    }

                    overScriptManager.ApplyVariableChangesTo(childElement);
                }
            }
        }

        private void DrawVariableValue(OverVariableData val, Rect rect)
        {
            float spacing = 10f;

            switch (val.type)
            {
                //simple
                case OverVariableType.Bool:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.boolValue = EditorGUI.Toggle(rect, "value:", val.boolValue);
                        break;
                    }
                case OverVariableType.String:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.stringValue = EditorGUI.TextField(rect, "value:", val.stringValue);
                        break;
                    }
                case OverVariableType.Float:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.floatValue = EditorGUI.FloatField(rect, "value:", val.floatValue, new GUIStyle(GUI.skin.textField) { alignment = TextAnchor.MiddleRight });
                        break;
                    }
                case OverVariableType.Int:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.integerValue = EditorGUI.IntField(rect, "value:", val.integerValue, new GUIStyle(GUI.skin.textField) { alignment = TextAnchor.MiddleRight });
                        break;
                    }
                case OverVariableType.Vector2:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.vector2Value = EditorGUI.Vector2Field(rect, "value:", val.vector2Value);
                        break;
                    }
                case OverVariableType.Vector3:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.vector3Value = EditorGUI.Vector3Field(rect, "value:", val.vector3Value);
                        break;
                    }
                case OverVariableType.Quaternion:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        Vector4 vector = new Vector4(val.QuaternionValue.x, val.QuaternionValue.y, val.QuaternionValue.z, val.QuaternionValue.w);
                        Vector4 temp_vector = EditorGUI.Vector4Field(rect, "value:", vector);
                        if (vector != temp_vector)
                        {
                            vector = temp_vector;
                        }
                        val.QuaternionValue = new Quaternion(vector.x, vector.y, vector.z, vector.w);
                        break;
                    }
                // Mono
                case OverVariableType.Transform:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.transformValue = (Transform)EditorGUI.ObjectField(rect, "value:", val.transformValue, typeof(Transform), true);
                        break;
                    }
                case OverVariableType.RectTransform:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.rectTransform = (RectTransform)EditorGUI.ObjectField(rect, "value:", val.rectTransform, typeof(RectTransform), true);
                        break;
                    }
                case OverVariableType.Rigidbody:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.rigidbodyValue = (Rigidbody)EditorGUI.ObjectField(rect, "value:", val.rigidbodyValue, typeof(Rigidbody), true);
                        break;
                    }
                case OverVariableType.Collider:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.colliderValue = (Collider)EditorGUI.ObjectField(rect, "value:", val.colliderValue, typeof(Collider), true);
                        break;
                    }
                case OverVariableType.CharacterController:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.characterController = (CharacterController)EditorGUI.ObjectField(rect, "value:", val.characterController, typeof(CharacterController), true);
                        break;
                    }
                case OverVariableType.Object:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.gameObject = (GameObject)EditorGUI.ObjectField(rect, "value:", val.gameObject, typeof(GameObject), true);
                        break;
                    }
                case OverVariableType.Renderer:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.renderer = (Renderer)EditorGUI.ObjectField(rect, "value:", val.renderer, typeof(Renderer), true);
                        break;
                    }
                case OverVariableType.LineRenderer:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.lineRenderer = (LineRenderer)EditorGUI.ObjectField(rect, "value:", val.lineRenderer, typeof(LineRenderer), true);
                        break;
                    }
                case OverVariableType.Material:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.material = (Material)EditorGUI.ObjectField(rect, "value:", val.material, typeof(Material), true);
                        break;
                    }
                case OverVariableType.ParticleSystem:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.particleSystem = (ParticleSystem)EditorGUI.ObjectField(rect, "value:", val.particleSystem, typeof(ParticleSystem), true);
                        break;
                    }
                case OverVariableType.AudioSource:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.audioSource = (AudioSource)EditorGUI.ObjectField(rect, "value:", val.audioSource, typeof(AudioSource), true);
                        break;
                    }
                case OverVariableType.AudioClip:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.audioClip = (AudioClip)EditorGUI.ObjectField(rect, "value:", val.audioClip, typeof(AudioClip), true);
                        break;
                    }
                case OverVariableType.Video:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.videoPlayer = (VideoPlayer)EditorGUI.ObjectField(rect, "value:", val.videoPlayer, typeof(VideoPlayer), true);
                        break;
                    }
                case OverVariableType.ImageStreamer:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.imageStreamer = (ImageStreamer)EditorGUI.ObjectField(rect, "value:", val.imageStreamer, typeof(ImageStreamer), true);
                        break;
                    }
                case OverVariableType.Animator:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.animator = (Animator)EditorGUI.ObjectField(rect, "value:", val.animator, typeof(Animator), true);
                        break;
                    }
                case OverVariableType.Light:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.light = (Light)EditorGUI.ObjectField(rect, "value:", val.light, typeof(Light), true);
                        break;
                    }
                case OverVariableType.NavMeshAgent:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.navMeshAgent = (NavMeshAgent)EditorGUI.ObjectField(rect, "value:", val.navMeshAgent, typeof(NavMeshAgent), true);
                        break;
                    }
                case OverVariableType.Text:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.text = (Text)EditorGUI.ObjectField(rect, "value:", val.text, typeof(Text), true);
                        break;
                    }
                case OverVariableType.TextTMP:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.textTMP = (TextMeshProUGUI)EditorGUI.ObjectField(rect, "value:", val.textTMP, typeof(TextMeshProUGUI), true);
                        break;
                    }
                case OverVariableType.TextTMP_3D:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.textTMP_3D = (TextMeshPro)EditorGUI.ObjectField(rect, "value:", val.textTMP_3D, typeof(TextMeshPro), true);
                        break;
                    }
                case OverVariableType.Image:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.image = (Image)EditorGUI.ObjectField(rect, "value:", val.image, typeof(Image), true);
                        break;
                    }
                case OverVariableType.RawImage:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.rawImage = (RawImage)EditorGUI.ObjectField(rect, "value:", val.rawImage, typeof(RawImage), true);
                        break;
                    }
                case OverVariableType.Color:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.color = EditorGUI.ColorField(rect, "Value: ", val.color);
                        break;
                    }
                case OverVariableType.List:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.list = (OverList)EditorGUI.ObjectField(rect, "container:", val.list, typeof(OverList), true);
                        break;
                    }
                case OverVariableType.JSON:
                    {
                        rect.y += EditorGUIUtility.singleLineHeight + spacing;
                        val.json = EditorGUI.TextArea(rect, val.json);
                        break;
                    }
                default: break;
            }
        }

        /******************** Subscription on variable changed ********************/

        private static void OnHierarchyChanged()
        {
            if (!isPlayModeChanging)
            {
                // Re-evaluate subscriptions whenever the hierarchy changes
                if (overScriptManager != null &&
                !overScriptManager.Equals(null))
                {
                    overScriptManager.UpdateVariableChangedSubscription(subscribe: true);
                }
            }
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            isPlayModeChanging = stateChange == PlayModeStateChange.ExitingEditMode ||
                                 stateChange == PlayModeStateChange.ExitingPlayMode;
        }
    }
}
