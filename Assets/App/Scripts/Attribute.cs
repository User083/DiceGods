using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute 
{

    public string _name;
    public string _description;
    public string _ID;
    public string _parentSystemID;
    public int base_value;
    public int max_value;
    public int final_value;


    public Attribute(SystemData parentSystem, string name, string desc, int defVal)
    {
        
        _parentSystemID = parentSystem.systemID; 
        _name = name;
        _description = desc;
        _ID = DataPersistenceManager.instance.generateUniqueID(name);
        max_value = 20;
        base_value= defVal;
    }

    public Attribute(SystemData parentSystem)
    {
        _parentSystemID = parentSystem.systemID;
        _name = "Attribute";
        _description = "Default attribute";
        _ID = "DefaultAttribute" + parentSystem.systemID;
        max_value = 20;
        base_value = 10;

    }
}
