using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;


public class DG_CustomEditorWindow : EditorWindow
{

    //Allows custom editors to agnostically auto-draw any serialised properties

    protected SerializedObject serializedObject;
    protected SerializedProperty currentProperty;
    private string propPath;
    protected SerializedProperty selectedProperty;

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
                prevPath = p.propertyPath;
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                 //recursive drawing, i.e. drawing all children
                if(p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndHorizontal();
                
               
               
            }
            else //check to prevent child properties being drawn over iteration
            {
                
                if(!string.IsNullOrEmpty(prevPath) && p.propertyPath.Contains(prevPath)) 
                { 
                    continue; 
                }
                
                prevPath = string.Empty;
                
            }

            EditorGUILayout.PropertyField(p, drawChildren);

        }

    }


    protected void DrawSidebar(SerializedProperty property)
    {
        
        
            foreach(SerializedProperty p in property)
            {
                if(GUILayout.Button(p.displayName))
                {
                    propPath = p.propertyPath;
                }
            }

            if(!string.IsNullOrEmpty(propPath))
            {
                selectedProperty = serializedObject.FindProperty(propPath);
            }
  
    }
   
    //Saves entries to the scriptable object
    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
