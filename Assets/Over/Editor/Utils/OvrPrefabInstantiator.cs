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

using Over;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

[InitializeOnLoad]
public class OvrPrefabInstantiator : Editor
{
    /// <summary>
    /// ScriptableObject for scripts references.
    /// </summary>
    private static OvrPrefabInstantiatorScriptableObject scriptsReferencesSO = null;

    private const string DEFAULT_SETTINGS_PATH = "Assets/Over/Editor/Settings";
    private const string DEFAULT_SETTINGS_NAME = "PrefabInstantiatorSettings.asset";

    [MenuItem("GameObject/OVER/OvrAsset", isValidateFunction: false, priority: 1)]
    private static void InstantiateOvrAsset()
    {
        if (CheckScriptsReferencesSO() is false)
            return;

        if (CheckReferencesInSO(scriptsReferencesSO.ovrAsset) is false)
            return;

        PrefabUtility.InstantiatePrefab(scriptsReferencesSO.ovrAsset);
    }

    [MenuItem("GameObject/OVER/OvrCanvas", isValidateFunction: false, priority: 1)]
    private static void InstantiateOvrCanvas()
    {
        if (CheckScriptsReferencesSO() is false)
            return;

        if (CheckReferencesInSO(scriptsReferencesSO.ovrCanvas) is false)
            return;

        PrefabUtility.InstantiatePrefab(scriptsReferencesSO.ovrCanvas);
    }

    [MenuItem("GameObject/OVER/OvrPlayerSimulator", isValidateFunction: false, priority: 1)]
    private static void InstantiateOvrPlayerSimulator()
    {
        if (CheckScriptsReferencesSO() is false)
            return;

        if (CheckReferencesInSO(scriptsReferencesSO.ovrPlayerSimulator) is false)
            return;

        PrefabUtility.InstantiatePrefab(scriptsReferencesSO.ovrPlayerSimulator);
    }

    [MenuItem("GameObject/OVER/OvrArWorldCanvas", isValidateFunction: false, priority: 1)]
    private static void InstantiateOvrArWorldCanvas()
    {
        if (CheckScriptsReferencesSO() is false)
            return;

        if (CheckReferencesInSO(scriptsReferencesSO.ovrArWorldCanvas) is false)
            return;

        PrefabUtility.InstantiatePrefab(scriptsReferencesSO.ovrArWorldCanvas);
    }

    [MenuItem("GameObject/OVER/Triggers/OvrClickableObject", isValidateFunction: false, priority: 2)]
    private static void InstantiateOvrClickableObject()
    {
        if (CheckScriptsReferencesSO() is false)
            return;

        if (CheckReferencesInSO(scriptsReferencesSO.ovrClickableObject) is false)
            return;

        PrefabUtility.InstantiatePrefab(scriptsReferencesSO.ovrClickableObject);
    }

    [MenuItem("GameObject/OVER/Triggers/OvrColliderTrigger", isValidateFunction: false, priority: 2)]
    private static void InstantiateOvrColliderTrigger()
    {
        if (CheckScriptsReferencesSO() is false)
            return;

        if (CheckReferencesInSO(scriptsReferencesSO.ovrColliderTrigger) is false)
            return;

        PrefabUtility.InstantiatePrefab(scriptsReferencesSO.ovrColliderTrigger);
    }

    [MenuItem("GameObject/OVER/Triggers/OvrUIButton", isValidateFunction: false, priority: 2)]
    private static void InstantiateOvrUIButton()
    {
        if (CheckScriptsReferencesSO() is false)
            return;

        if (CheckReferencesInSO(scriptsReferencesSO.ovrUIButton) is false)
            return;

        OvrUIButton ovrUIButton = (OvrUIButton)PrefabUtility.InstantiatePrefab(scriptsReferencesSO.ovrUIButton);

        if (Selection.activeTransform != null)
        {
            if (Selection.activeTransform.TryGetComponent(out Canvas canvasToParent))
            {
                ovrUIButton.transform.parent = canvasToParent.transform;
            }
        }
        else
        {
            OvrCanvas ovrCanvasToParent = FindObjectOfType<OvrCanvas>();
            if (ovrCanvasToParent != null)
            {
                ovrUIButton.transform.parent = ovrCanvasToParent.transform;
            }
            else
            {
                Canvas canvasToParent = FindObjectOfType<Canvas>();
                if (canvasToParent != null)
                {
                    ovrUIButton.transform.parent = canvasToParent.transform;
                }
            }
        }
    }

    [MenuItem("GameObject/OVER/Preset/ChromaKeyVideoPlayer", isValidateFunction: false, priority: 3)]
    private static void InstantiateChromaKeyVideoPlayer()
    {
        if (CheckScriptsReferencesSO() is false)
            return;

        if (CheckReferencesInSO(scriptsReferencesSO.chromaKeyVideoPlayer) is false)
            return;

        PrefabUtility.InstantiatePrefab(scriptsReferencesSO.chromaKeyVideoPlayer);
    }

    /// <summary>
    /// Check if we have a scriptable object settings.
    /// </summary>
    /// <returns>True we have it, otherwise false.</returns>
    private static bool CheckScriptsReferencesSO()
    {
        if (scriptsReferencesSO == null || scriptsReferencesSO.Equals(null))
        {
            // Load the scriptable object
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(OvrPrefabInstantiatorScriptableObject)}");
            string assetPath = AssetDatabase.GUIDToAssetPath(guids.FirstOrDefault());
            scriptsReferencesSO = AssetDatabase.LoadAssetAtPath<OvrPrefabInstantiatorScriptableObject>(assetPath);

            if (scriptsReferencesSO == null)
            {
                string pathToCreate = System.IO.Path.Combine(DEFAULT_SETTINGS_PATH, DEFAULT_SETTINGS_NAME);

                Debug.LogError($"Cannot find the instantiator settings, created new one at {pathToCreate}");

                AssetDatabase.CreateAsset(new OvrPrefabInstantiatorScriptableObject(), pathToCreate);

                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Check if the MonoBehaviour is null or nullReference
    /// </summary>
    /// <returns>True is valid, otherwise false.</returns>
    private static bool CheckReferencesInSO<T>(T referenceToCheck)
    {
        if (referenceToCheck == null || referenceToCheck.Equals(null))
        {
            Debug.LogError("The reference asset is NULL, please assign it in the settings.");

            return false;
        }

        return true;
    }
}
