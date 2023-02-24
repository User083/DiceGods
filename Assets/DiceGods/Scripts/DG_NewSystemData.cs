using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]


public class DG_NewSystemData : ScriptableObject
{
        
    [Header("Basic")]
    public string SystemName;

    [Header("System Settings")]
    [SerializeField] public bool usesAttributes;
    [SerializeField] public bool usesClasses;
    [SerializeField] public bool usesLevels;
    [SerializeField] public bool usesRaces;
    [SerializeField] public bool usesDamageTypes;

    [Header("Core Character Stats")]
    [SerializeField] public bool health;
    [SerializeField] public bool mana;
    [SerializeField] public bool stamina;
    [SerializeField] public bool armour;
    [SerializeField] public bool defence;


    //[Header("Attributes")]
    public List<Attribute> Attributes;

    //[Header("Damage Types")]
    [SerializeField] private List<DamageType> DamageTypes;

  public DG_NewSystemData() 
    {
        //SystemName = "New System";
        //Attributes = new List<Attribute>();
        //DamageTypes= new List<DamageType>();
    }

   private void enableSection(bool option)
    {

    }
}
