using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New System", menuName = "DiceGods/System", order = 1)]
[System.Serializable]
public class SO_Systems : ScriptableObject
{
    private ScriptableObject self;

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
    public List<Attribute> Attributes = new List<Attribute>();

    [Header("Races")]
    public List<DamageType> DamageTypes = new List<DamageType>();

  

    private void Awake()
    {
        self = this;


    }

}
