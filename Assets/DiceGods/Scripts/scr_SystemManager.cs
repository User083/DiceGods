using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SystemManager : MonoBehaviour
{
    public List<DG_NewSystemData> systems = new List<DG_NewSystemData>();
    public List<string> systemNames;

    private void Awake()
    {
        systemNames = new List<string>();
       
       
        foreach(var system in systems)
        {
            systemNames.Add(system.systemName);
        }
    }



}
