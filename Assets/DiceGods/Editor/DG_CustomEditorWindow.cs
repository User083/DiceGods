using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DG_CustomEditorWindow : EditorWindow
{

    //Allows custom editors to agnostically auto-draw any serialised properties

    protected SerializedObject serializedObject;
    protected SerializedProperty currentProperty;

    //Draw call
    protected void DrawProperties(SerializedProperty property, bool drawChildren)
    {
        string prevPath = string.Empty; 

        //Loop through all properties
        foreach(SerializedProperty p in property)
        {
           
            //is property an array? 
            if(p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                EditorGUILayout.EndHorizontal();

                //recursive drawing, i.e. drawing all children
                if(p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel--;
                }
               prevPath = p.propertyPath;
            }
            else //check to prevent child properties being drawn over iteration
            {
                if(!string.IsNullOrEmpty(prevPath) && p.propertyPath.Contains(prevPath)) 
                { 
                    continue; 
                }
                prevPath = p.propertyPath;
            }
                
                EditorGUILayout.PropertyField(p, drawChildren);
        }
    }
    
   
    //Saves entries to the scriptable object
    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
