using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemData : DefaultSystemData
{
    [Header("System Info")]
    public string systemName;
    public string systemID;

    [Header("Core Components")]
    public bool useLevels;
    public bool useClasses;
    public List<CharacterClass> characterClasses;
    public bool useRaces;
    public List<Race> races;
  

    [Header("Attributes & Skills")]
    public bool useCoreStats;
    public List<Stat> coreStats;
    public bool useAttributes;
    public List<Attribute> attributes;
    public int attMaxLevel = 20;
    public SystemData(string systemName) 
    {
        
        this.systemName = systemName;
        systemID = string.Empty;
        setDefaultValues();
    }

    public void setDefaultValues()
    {
        
        attributes = InitAttributes(this);
        characterClasses = InitClasses(this);
        races = InitRaces(this);
    }

    public void SetID()
    {
        if(systemID!= null || systemID != string.Empty)
        {
            systemID = generateSystemID(systemName);
        }
        else
        {
            Debug.LogWarning("System ID already set to: " + systemID);
        }       
    }
    private string generateSystemID(string systemName)
    {
        string random = Random.Range(0, 9999).ToString();
        string systemID = systemName + random;

        return systemID;
    }


}
