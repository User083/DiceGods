using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class NewSystem_Data : MonoBehaviour, IDataPersistence
{
    [Header("Basic")]
    [SerializeField] private UIDocument newSystemUI;
    private NewSystem_Manager systemManager;
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

    private void Awake()
    {
        systemManager = newSystemUI.GetComponent<NewSystem_Manager>();
    }

    void IDataPersistence.LoadData(SaveData data)
    {
        this.systemName = data.systemName;
        this.useLevels = data.useLevels;
        this.useClasses = data.useClasses;
        this.characterClasses = data.characterClasses;
        this.useRaces = data.useRaces;
        this.races = data.races;
        this.useCoreStats = data.useCoreStats;
        this.coreStats = data.coreStats;
        this.useAttributes = data.useAttributes;
        this.atrributes = data.atrributes;
    }

    void IDataPersistence.SaveData(ref SaveData data)
    {
        data.systemName = this.systemName;
        data.useLevels = this.useLevels;
         data.useClasses = this.useClasses;
         data.characterClasses = this.characterClasses;
         data.useRaces = this.useRaces;
         data.races = this.races;
         data.useCoreStats = this.useCoreStats;
         data.coreStats = this.coreStats;
         data.useAttributes = this.useAttributes;
        data.atrributes = this.atrributes;
    }


}
