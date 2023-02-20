using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SystemManager : MonoBehaviour
{
    public List<SO_Systems> systems = new List<SO_Systems>();
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
