using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

[System.Serializable]
public class Race 
{
    public string _parentSystemID;
    public string _name;
    public string _description;
    public string _ID;

    //Constructor for custom race creation 
    public Race(SystemData parentSystem, string name, string desc)
    {
        _parentSystemID = parentSystem.systemID;
        _name = name;
        _description = desc;
        _ID = generateRaceID(name);
    }

    //Default fallback
    public Race(SystemData parentSystem)
    {
        _parentSystemID = parentSystem.systemID;
        _name = "Default";
        _description = "Default placeholder race with no effect on gameplay or stats";
        _ID = parentSystem.systemID + "DefaultRace";
    }

    //Generate unique race ID when creating custom race
    private string generateRaceID(string raceName)
    {
        string random = Random.Range(0, 9999).ToString();
        string raceID = raceName + random;

        return raceID;
    }
}
