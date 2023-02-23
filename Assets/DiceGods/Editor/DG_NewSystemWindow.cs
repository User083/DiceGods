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
        Apply();
    }

}
