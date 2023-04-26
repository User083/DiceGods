using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData 
{
    [Header ("Data")]
    
    public string _ID;
    public string _SystemID;
    public string _name;
    public string _description;
    public float _value;
    public float _weight;
    public int _level;
    public Race _race;
    public CharacterClass _class;
    public List<Attribute> _attributes;
    public List<Stat> _coreStats;
    protected float _curHealth;
    public float _maxHealth;
    protected float _minHealth;

}
