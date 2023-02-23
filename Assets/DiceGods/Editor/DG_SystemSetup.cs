using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DG_NewSystemData))]
public class DG_SystemSetup : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Editor"))
        {
            DG_NewSystemWindow.Open((DG_NewSystemData)target);
        }
    }


}
