using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemData 
{
    [Header("System Info")]
    public string systemName;
    public string systemID;
    public DefaultSystemData defaultData = new DefaultSystemData();

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
        systemID = systemName + "4";
        setDefaultValues();
    }

    public void setDefaultValues()
    {
       
        attributes = defaultData.InitAttributes(this);
        characterClasses = defaultData.InitClasses(this);
        races = defaultData.InitRaces(this);
    }



}
