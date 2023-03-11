using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewSystem_Data : MonoBehaviour
{
    [Header("Basic")]
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


}
