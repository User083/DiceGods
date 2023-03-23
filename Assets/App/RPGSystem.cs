using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RPGSystem 
{


    public SaveData activeSave;
    

    public RPGSystem(SaveData saveData)
    {
        activeSave = saveData;
        
        
    }



    //Handle data changes

    public void LoadData(SaveData data)
    {
        activeSave= data;
        
    }

    public void SaveData(SaveData data)
    {
        
        data = activeSave;
    }
}
