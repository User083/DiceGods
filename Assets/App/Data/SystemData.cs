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
        setDefaultValues();
    }

    public void setDefaultValues()
    {
        DefaultSystemData temp = new DefaultSystemData();
        attributes = temp.InitAttributes(this);
        characterClasses = temp.InitClasses(this);
        races = temp.InitRaces(this);
    }

    public void SetID()
    {

        systemID = generateSystemID(systemName);
     
    }
    private string generateSystemID(string systemName)
    {
        string random = Random.Range(0, 9999).ToString();
        string systemID = systemName + random;

        return systemID;
    }


}
