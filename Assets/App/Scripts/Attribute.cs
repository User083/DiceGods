using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute : Abstract_Parent
{

   
    public string _parentSystemID;
    public int base_value;
    public int max_value;
    public int final_value;


    public Attribute(SystemData parentSystem, string name, string desc, int defVal)
    {
        
        _parentSystemID = parentSystem.systemID; 
        _name = name;
        _description = desc;
        max_value = 20;
        base_value= defVal;
    }
}
