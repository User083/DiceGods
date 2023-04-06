using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterClass
{
    public string _parentSystemID;
    public string _name;
    public string _description;
    public string _classID;

    public CharacterClass(SystemData parentSystem, string name, string desc)
    {
        _parentSystemID = parentSystem.systemID;
        _name = name;
        _description = desc;
        _classID = generateClassID(name);
        
    }

    //Default fallback
    public CharacterClass(SystemData parentSystem)
    {
        _parentSystemID = parentSystem.systemID;
        _name = "Default";
        _description = "Default placeholder class with no effect on gameplay or stats";
        _classID = parentSystem.systemID + "DefaultCharacterClass";
    }

    private string generateClassID(string className)
    {
        string random = Random.Range(0, 9999).ToString();
        string classID = className + random;

        return classID;
    }
}
