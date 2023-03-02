using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DG_NewSystemData))]
public class DG_SystemPropDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        SerializedProperty healthProp = property.FindPropertyRelative("health");
        SerializedProperty manaProp = property.FindPropertyRelative("mana");
        SerializedProperty attributeProp = property.FindPropertyRelative("Attributes");
        SerializedProperty nameProp = property.FindPropertyRelative("systemName");

        DrawProperties(attributeProp, true);



        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }

    protected void DrawProperties(SerializedProperty property, bool drawChildren)
    {
        string prevPath = string.Empty;

        //Loop through all properties
        foreach (SerializedProperty p in property)
        {

            //is property an array? 
            if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                prevPath = p.propertyPath;
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                //recursive drawing, i.e. drawing all children
                if (p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndHorizontal();



            }
            else //check to prevent child properties being drawn over iteration
            {

                if (!string.IsNullOrEmpty(prevPath) && p.propertyPath.Contains(prevPath))
                {
                    continue;
                }

                prevPath = string.Empty;
                EditorGUILayout.PropertyField(p, drawChildren);
            }

            

        }

    }
}
