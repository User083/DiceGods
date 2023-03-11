using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{

    [Header("Save Info")]

    public string fileName;
    public long lastUpdated;

    [Header("System Info")]
    public string systemName;

    [Header("Core Components")]
    public bool useLevels;
    public bool useClasses;
    public List<CharacterClass> characterClasses;
    public bool useRaces;
    public List<Race> races;
    public bool useCoreStats;
    public List<CoreStat> coreStats;
    public bool useAttributes;
    public List<Attribute> atrributes;



    //Init default values for new save
    public SaveData(string fileName) 
    {
        this.fileName= fileName;
   
    }

    //Overload for full system
   /* public SaveData(string fileName, string systemName, bool useLevels, bool useClasses, 
        List<CharacterClass> characterClasses, bool useRaces, List<Race> races, bool useCoreStats, 
        List<CoreStat> coreStats, bool useAttributes, List<Attribute> atrributes)
    {
        this.fileName = fileName;
        this.lastUpdated = DateTime.Now.ToBinary();
        this.systemName = systemName;
        this.useLevels = useLevels;
        this.useClasses = useClasses;
        this.characterClasses = characterClasses;
        this.useRaces = useRaces;
        this.races = races;
        this.useCoreStats = useCoreStats;
        this.coreStats = coreStats;
        this.useAttributes = useAttributes;
        this.atrributes= atrributes;
    }*/
}
