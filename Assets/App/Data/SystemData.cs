using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemData 
{
    [Header("System Info")]
    public string systemName;
    public string systemID;

    [Header("Core Components")]
    public bool useLevels;
    public bool useWeight;
    public bool charsHaveValue;
    public bool useClasses;
    public List<CharacterClass> characterClasses;
    public Dictionary<string, CharacterClass> classesByID;
    public bool useRaces;
    public List<Race> races;
    public Dictionary <string, Race> racesByID;
  

    [Header("Attributes & Skills")]
    public bool useCoreStats;
    public List<Stat> coreStats;
    public bool useAttributes;
    public List<Attribute> attributes;
    public int attMaxLevel = 20;
    public SystemData(string systemName) 
    {
        
        this.systemName = systemName;
        classesByID = new Dictionary<string, CharacterClass>();
        racesByID= new Dictionary<string, Race>();
        setDefaultValues();
        SetupDictionaries();
    }

    public void setDefaultValues()
    {
        DefaultSystemData temp = new DefaultSystemData();
        attributes = temp.InitAttributes(this);
        characterClasses = temp.InitClasses(this);
        races = temp.InitRaces(this);
        coreStats = temp.InitStats(this);
    }

    public void SetID()
    {

        systemID = DataPersistenceManager.instance.generateUniqueID(systemName);

    }
    public void SetupDictionaries()
    {
        foreach(var charClass in characterClasses)
        {
            classesByID.Add(charClass._ID, charClass);
        }
        foreach(var race in races)
        {
            racesByID.Add(race._ID, race);
        }
    }

    public void UpdateIDs()
    {
        foreach(var att in attributes)
        {
            att._parentSystemID = systemID;
        }

        foreach (var charClass in characterClasses)
        {
            charClass._parentSystemID = systemID;
        }

        foreach (var race in races)
        {
            race._parentSystemID = systemID;
        }

    }
}
