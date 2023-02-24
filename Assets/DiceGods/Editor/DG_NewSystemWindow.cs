using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DG_NewSystemWindow : DG_CustomEditorWindow
{
     public static void Open(DG_NewSystemData newSystem)
    {
        DG_NewSystemWindow window = GetWindow<DG_NewSystemWindow>("New System Editor");
        window.serializedObject = new SerializedObject(newSystem);

    }

    private void OnGUI()
    {
        
        
        currentProperty = serializedObject.GetIterator();
        DrawProperties(currentProperty, true);
        //draw sidebar
       /* EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));   
     
            DrawSidebar(currentProperty);
        
        
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if (selectedProperty != null)
        {
            //Drawing the actual properties (if returned)
            
            
        }
        else
        {
            EditorGUILayout.LabelField("Select item from the list");
            
        }
        
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();*/
        
        
        //Save info to object
        Apply();
    }

}
