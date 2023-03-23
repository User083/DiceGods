using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData 
{

    public string _ID;
    public string _name;
    public string _description;
    public float _value;
    public float _weight;
    public int _level;
    public Race _race;
    public CharacterClass _class;
    public List<Attribute> _attributes;

    public float _curHealth;
    public float _maxHealth;
    public float _minHealth;

    public CharacterData(string name, string desc) 
    {
        _name = name;
        _description = desc;
        _value = -1;
        _weight = -1;
        _level = -1;
        _race = null;
        _class = null;
        _attributes = new List<Attribute>();
        _minHealth = 0;
        _maxHealth = 100;
        _curHealth = _maxHealth;
    }




}
