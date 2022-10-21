using UnityEditor;
using UnityEngine;


namespace Over
{
    /// <summary>
    /// This will allow you to mark -any- field as readonly with a single property attribute / property drawer.
    /// </summary>
    /// <seealso cref="https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html"/>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //  If you exit the method with GUI.enabled set to false the entire element will be ignored,
            //  while if GUI.enabled is false while populating children they will appear disabled.
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}