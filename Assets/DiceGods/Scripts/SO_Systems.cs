using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New System", menuName = "Systems/NewSystem", order = 1)]
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

    [Header("Core Character Stats")]
    public bool health;
    public bool mana;
    public bool stamina;
    public bool armour;
    public bool defence;


    private void Awake()
    {
        self = this;
    }

}
