using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Race 
{
    public string parentSystem;
    public string _name;
    public string _description;

    public Race(SystemData parentSystem, string name, string desc)
    {
        this.parentSystem = parentSystem.systemID;
        _name = name;
        _description = desc;

    }
}
