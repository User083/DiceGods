using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[System.Serializable]


public class DG_NewSystemData : ScriptableObject
{

    public string SystemName;
    public List<Attribute> attributes = new List<Attribute>();
    private List<Race> races;
    private List<Class> classes;
    private List<CoreStat> coreStats;

    
    public DG_NewSystemData()
    {
        
    }

}
