using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(StateMachine))]
    public class StateMachineDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty state = property.FindPropertyRelative("currentState");

            string name = "";
            
            if (state != null)
            {
                name = state.managedReferenceFullTypename.Split('.').Last().Replace("State", "");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Null";
            }

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.LabelField(position, "Current State", name);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}