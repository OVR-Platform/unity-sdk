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

using BlueGraph;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace OverSDK.VisualScripting
{
    [Tags("Common")]
    public abstract class OverListOperation : OverExecutionFlowNode { }
    [Tags("Common")]
    public abstract class OverListCommon : OverNode { }

    // COMMON
    [Node(Path = "Operations/List/Handlers/Base", Name = "Length", Icon = "OPERATIONS/LIST")]
    public class OverListLength : OverListCommon
    {
        [Input("List")] public IList list;

        [Output("Length", Multiple = true)] public int length;

        public override object OnRequestNodeValue(Port port)
        {
            IList _list = GetInputValue("List", list);
            switch (port.Name)
            {
                case "Length":
                    return _list.Count;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/List/Handlers/Base", Name = "First", Icon = "OPERATIONS/LIST")]
    public class OverListFirst : OverListCommon
    {
        [Input("List")] public IList list;
        [Output("Element", Multiple = true)] public object element;

        public override object OnRequestNodeValue(Port port)
        {
            IList _list = GetInputValue("List", list);
            switch (port.Name)
            {
                case "Element":
                    return _list.Count > 0 ? _list[0] : null;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/List/Handlers/Base", Name = "Last", Icon = "OPERATIONS/LIST")]
    public class OverListLast : OverListCommon
    {
        [Input("List")] public IList list;
        [Output("Element", Multiple = true)] public object element;

        public override object OnRequestNodeValue(Port port)
        {
            IList _list = GetInputValue("List", list);
            switch (port.Name)
            {
                case "Element":
                    return _list.Count > 0 ? _list[_list.Count - 1] : null; ;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    //OPERATIONS

    [Node(Path = "Operations/List/Handlers/Common", Name = "Add", Icon = "OPERATIONS/LIST")]
    [Output("Updated List", Multiple = true, Type = typeof(IList))]
    public class OverListAdd : OverListOperation
    {
        [Input("List")] public IList list;
        [Input("Element")] public object element;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            var _element = GetInputValue("Element", element);
            IList _list = GetInputValue("List", list);

            try
            {
                _list.Add(_element);
                list = _list;
            }
            catch (ArgumentException e)
            {
                UnityEngine.Debug.LogError($"There's a problem adding element {_element.GetType()} into {_list.GetType()} list.\n{e}");
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Updated List":
                    return list;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/List/Handlers/Common", Name = "Remove", Icon = "OPERATIONS/LIST")]
    [Output("Updated List", Multiple = true, Type = typeof(IList))]
    public class OverListRemove : OverListOperation
    {
        [Input("List")] public IList list;
        [Input("Element")] public object element;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            var _element = GetInputValue("Element", element);
            IList _list = GetInputValue("List", list);

            try
            {
                if (_list.Contains(_element))
                {
                    _list.Remove(_element);
                    list = _list;
                }
            }
            catch (ArgumentException e)
            {
                UnityEngine.Debug.LogError($"There's a problem removing element {_element.GetType()} into {_list.GetType()} list.\n{e}");
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Updated List":
                    return list;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/List/Handlers/Common", Name = "Clear", Icon = "OPERATIONS/LIST")]
    [Output("Updated List", Multiple = true, Type = typeof(IList))]
    public class OverListClear : OverListOperation
    {
        [Input("List")] public IList list;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            IList _list = GetInputValue("List", list);

            _list.Clear();
            list = _list;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Updated List":
                    return list;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/List/Handlers/Common", Name = "Get At", Icon = "OPERATIONS/LIST")]
    public class OverListGetElementAt : OverListOperation
    {
        [Input("List")] public IList list;
        [Input("Index")] public int index;
        [Output("Element", Multiple = true)] public object element;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            int _index = GetInputValue("Index", index);
            IList _list = GetInputValue("List", list);

            element = (_index >= 0 && _index < _list.Count) ? _list[_index] : null;

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Element":
                    return element;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/List/Handlers/Advanced", Name = "Insert At", Icon = "OPERATIONS/LIST")]
    [Output("Updated List", Multiple = true, Type = typeof(IList))]
    public class OverListInsertAt : OverListOperation
    {
        [Input("List")] public IList list;
        [Input("Element")] public object element;
        [Input("Index")] public int index;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            var _element = GetInputValue("Element", element);
            int _index = GetInputValue("Index", index);
            IList _list = GetInputValue("List", list);

            try
            {
                _list.Insert(_index, _element);
                list = _list;
            }
            catch (ArgumentException e)
            {
                UnityEngine.Debug.LogError($"There's a problem inserting element {_element.GetType()} into {_list.GetType()} list.\n{e}");
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Updated List":
                    return list;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/List/Handlers/Advanced", Name = "Remove At", Icon = "OPERATIONS/LIST")]
    [Output("Updated List", Multiple = true, Type = typeof(IList))]
    public class OverListRemoveAt : OverListOperation
    {
        [Input("List")] public IList list;
        [Input("Index")] public int index;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            int _index = GetInputValue("Index", index);
            IList _list = GetInputValue("List", list);

            try
            {
                _list.RemoveAt(_index);
                list = _list;
            }
            catch (ArgumentException e)
            {
                UnityEngine.Debug.LogError($"There's a problem removing element in {_list.GetType()} list at {_index} index.\n{e}");
            }

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Updated List":
                    return list;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/List/Handlers/Advanced", Name = "Reverse", Icon = "OPERATIONS/LIST")]
    [Output("Updated List", Multiple = true, Type = typeof(IList))]
    public class OverListReverse : OverListOperation
    {
        [Input("List")] public IList list;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            IList _list = GetInputValue("List", list);

            object[] objects = new object[_list.Count];

            int length = _list.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                objects[Mathf.Abs(i - length)] = _list[i];
            }

            list = objects.ToList();

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Updated List":
                    return list;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/List/Handlers/Boolean", Name = "Contains", Icon = "OPERATIONS/LIST")]
    public class OverListContainsElement : OverListOperation
    {
        [Input("List")] public IList list;
        [Input("Element")] public object element;

        [Output("Is Inside", Multiple = true)] public bool isInside;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            var _element = GetInputValue("Element", element);
            IList _list = GetInputValue("List", list);

            // Check if the list is empty
            if (_list.Count == 0)
            {
                Debug.LogWarning("The list is empty. No type to compare to.");
            }
            else
            {
                // Get the type of the first item in the list
                Type firstItemType = _list[0].GetType();
                Type secondItemType = _element.GetType();

                // Compare it to the type of the element
                if (IsTheSameType(firstItemType, secondItemType) is false)
                {
                    Debug.LogWarning($"The type of the element ({_element.GetType().Name}) is different from the type of items in the list ({firstItemType.Name}).");
                }
            }

            isInside = _list.Contains(_element);

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Is Inside":
                    return isInside;
            }

            return base.OnRequestNodeValue(port);
        }

        /// <summary>
        /// This method checks if are the same type taking in consideration also the derived types (Update this check if more type are supported in over list)
        /// </summary>
        /// <returns>True are the same type, otherwise not.</returns>
        private bool IsTheSameType(Type firstItemType, Type secondItemType)
        {
            // Direct type comparison
            if (firstItemType == secondItemType)
                return true;

            string firstTypeName = firstItemType.Name;
            string secondTypeName = secondItemType.Name;

            // String comparison instead reflection is cheaper vvv

            // Check for Transform and RectTransform relationship
            if (firstTypeName.Contains("Transform") && secondTypeName.Contains("Transform"))
                return true;

            // Check for Collider relationship
            if (firstTypeName.Contains("Collider") && secondTypeName.Contains("Collider"))
                return true;

            return false;
        }
    }

    [Node(Path = "Operations/List/Handlers/Common", Name = "Index Of", Icon = "OPERATIONS/LIST")]
    public class OverListIndexOf : OverListOperation
    {
        [Input("List")] public IList list;
        [Input("Element")] public object element;

        [Output("Index", Multiple = true)] public int index;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            var _element = GetInputValue("Element", element);
            IList _list = GetInputValue("List", list);

            index = _list.IndexOf(_element);

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Index":
                    return index;
            }

            return base.OnRequestNodeValue(port);
        }
    }

    [Node(Path = "Operations/List/Handlers/Advanced", Name = "Range", Icon = "OPERATIONS/LIST")]
    [Output("Updated List", Multiple = true, Type = typeof(IList))]
    public class OverListRange : OverListOperation
    {
        [Input("List")] public IList list;

        [Input("Index")] public int index;
        [Input("Count")] public int count;

        public override IExecutableOverNode Execute(OverExecutionFlowData data)
        {
            //read inputs
            IList _list = GetInputValue("List", list);
            int _index = GetInputValue("Index", index);
            int _count = GetInputValue("Count", count);

            object[] objects = new object[_count];

            for (int i = 0; i < _count; i++)
            {
                if (_index + i < _list.Count)
                {
                    objects[i] = _list[_index + i];
                }
            }

            list = objects.ToList();

            return base.Execute(data);
        }

        public override object OnRequestNodeValue(Port port)
        {
            switch (port.Name)
            {
                case "Updated List":
                    return list;
            }

            return base.OnRequestNodeValue(port);
        }
    }
}