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



using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace OverSDK
{
#if UNITY_EDITOR
    public class OverBetterEnumWindow : EditorWindow
    {
        public event Action<OverBetterEnumWindow> OnWindowCloses;

        public event Action<string> OnItemChosen;

        private List<string> enumList = new List<string>();
        private List<string> tempList = new List<string>();

        private ListView listView = null;
        private TextField searchBar = null;

        Func<VisualElement> makeItem;
        Action<VisualElement, int> bindItem;

        private string lastSearchValue;
        private string tempItemSelected = string.Empty;

        private int currSelection = -1;
        private int tempSelectionIndex = -1;

        /// <summary>
        /// Provide the list view with an explict height for every row so it can calculate how many items to actually display.
        /// </summary>
        private const int itemHeight = 16;

        /******************** Mono ********************/

        public void OnEnable()
        {
            searchBar = new TextField();

            rootVisualElement.Add(searchBar);
            lastSearchValue = searchBar.text;

            InitListView();
        }

        private void OnGUI()
        {
            // Update the list based on what is typed in the search bar
            if (searchBar.text != lastSearchValue)
            {
                lastSearchValue = searchBar.text.ToLower();

                tempList = enumList.Where(x => x.ToLower().Contains(lastSearchValue)).ToList();

                if (tempList.Count <= 0)
                    tempList.AddRange(enumList);

                tempSelectionIndex = tempList.FindIndex(x => x == tempItemSelected);

                DeinitListView();
                InitListView();
            }
        }

        private void OnLostFocus()
        {
            Close();
        }

        private void OnDestroy()
        {
            OnWindowCloses?.Invoke(this);
        }

        /******************** Init/Deinit ********************/

        private void InitListView()
        {
            // The "makeItem" function is called when the
            // ListView needs more items to render.
            makeItem += OnMakeItem;

            // As the user scrolls through the list, the ListView object
            // recycles elements created by the "makeItem" function,
            // and invoke the "bindItem" callback to associate
            // the element with the matching data item (specified as an index in the list).
            bindItem += OnBindItem;
            listView = new ListView(tempList, itemHeight, makeItem, bindItem);

            listView.selectionType = SelectionType.Single;

            // Auto selection the item in the list
            if (string.IsNullOrEmpty(lastSearchValue))
            {
                listView.SetSelection(currSelection);
            }
            else if (tempSelectionIndex >= 0)
            {
                listView.SetSelection(tempSelectionIndex);
            }

            // Callbacks when the user interact with the items in the list
            listView.onItemsChosen += ListView_onItemsChosen;
            listView.onSelectionChange += ListView_onSelectionChange;

            listView.style.flexGrow = 1.0f;

            rootVisualElement.Add(listView);
        }

        private void DeinitListView()
        {
            makeItem -= OnMakeItem;
            bindItem -= OnBindItem;

            listView.onItemsChosen -= ListView_onItemsChosen;
            listView.onSelectionChange -= ListView_onSelectionChange;

            rootVisualElement.Remove(listView);
        }

        /******************** Events ********************/

        private void ListView_onSelectionChange(IEnumerable<object> items)
        {
            // Broadcast the element selected
            object firstItem = items.First();

            OnItemChosen?.Invoke(firstItem.ToString());

            // Keep track of what item is selected
            if (listView.selectedIndex >= 0)
                tempItemSelected = listView.selectedItem.ToString();
        }

        private void ListView_onItemsChosen(IEnumerable<object> items)
        {
            Close();
        }

        /******************** ListView events ********************/

        private VisualElement OnMakeItem() => new Label();

        private void OnBindItem(VisualElement element, int index)
        {
            (element as Label).text = tempList[index];
        }

        /******************** Public ********************/

        public void UpdateEnumValue(List<string> enumValues)
        {
            enumList.Clear();
            tempList.Clear();

            enumList.AddRange(enumValues);
            tempList.AddRange(enumValues);

            lastSearchValue = string.Empty;

            listView.Rebuild();
        }

        public void UpdateCurrentSelection(int selectionIndex)
        {
            currSelection = selectionIndex;

            listView.SetSelection(currSelection);
        }
    }
#endif
}