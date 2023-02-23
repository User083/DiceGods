using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DGInitWindow : EditorWindow
{

    DG_NewSystemData newSystem = null;
    private string pathName;
    private string newSystemName = string.Empty;
  
    

    [MenuItem("DiceGods/New System")]
    public static void Popup ()
    {
        GetWindow<DGInitWindow>("Dice Gods");
        
    }
    
    private void OnGUI()
    {
       
       newSystemName = EditorGUILayout.TextField("System Name", newSystemName);
        
        
        if (GUILayout.Button("Create System") && newSystemName != string.Empty)
        {
            newSystem = CreateInstance<DG_NewSystemData>();

            if (AssetDatabase.AssetPathToGUID(pathName).Length < 1) //if asset doesn't already exist
            {
            pathName = AssetDatabase.GenerateUniqueAssetPath($"Assets/DiceGods/Resources/{newSystemName}.asset"); //Generate a unique pathname
           
                // create a new scriptable object, save it, referesh and wipe the string

            AssetDatabase.CreateAsset(newSystem, pathName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            newSystemName= string.Empty;
                //Focus on the newly created object in editor and open the custom object editor
            EditorUtility.FocusProjectWindow();
            DG_NewSystemWindow.Open(newSystem);
                Close();
            }
            else
            {
                Debug.Log("File already exists");
            }            
            
        }
        else
        {
            
        }
    }

}
