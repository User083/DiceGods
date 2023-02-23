using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DG_NewSystemData : ScriptableObject
{

    [Header("Basic")]
    public string systemName;

    [Header("System Settings")]
    public bool usesAttributes;
    public bool usesClasses;
    public bool usesLevels;
    public bool usesRaces;
    public bool usesDamageTypes;

    [Header("Core Character Stats")]
    public bool health;
    public bool mana;
    public bool stamina;
    public bool armour;
    public bool defence;


    [Header("Attributes")]
    public List<Attribute> Attributes;

    [Header("Races")]
    public List<DamageType> DamageTypes;

  public DG_NewSystemData() 
    {
        Attributes = new List<Attribute>();
        DamageTypes= new List<DamageType>();
    }

}
