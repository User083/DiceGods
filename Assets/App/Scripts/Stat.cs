using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    public string _parentSystemID;
    public string _name;
    public string _description;
    public string _ID;
    public float _maxVal;
    public float _baseVal;
    public float _finalVal;

    //Constructor for custom stat creation 
    public Stat(SystemData parentSystem, string name, string desc, float maxVal)
    {
        _parentSystemID = parentSystem.systemID;
        _name = name;
        _description = desc;
        _ID = DataPersistenceManager.instance.generateUniqueID(name);
        _maxVal = maxVal;
        _baseVal = _maxVal;
        _finalVal = _maxVal;
    }
    //Default fallback
    public Stat(SystemData parentSystem)
    {
        _parentSystemID = parentSystem.systemID;
        _name = "Default";
        _description = "Default placeholder stat with no effect on gameplay";
        _ID = parentSystem.systemID + "DefaultStat";
        _maxVal = 100;
        _baseVal = _maxVal;
        _finalVal = _maxVal;
    }
}
