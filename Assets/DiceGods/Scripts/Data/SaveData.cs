using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

[System.Serializable]
public class SaveData 
{
    public List<DG_NewSystemData> savedSystems;
    public List<string> systemNames;

    public SaveData()
    {
        this.savedSystems = new List<DG_NewSystemData>();
        this.systemNames = new List<string>();

        if(savedSystems != null)
        {
             foreach (var system in savedSystems)
            {
                systemNames.Add(system.SystemName);
            }
        }
      

    }

  
}
