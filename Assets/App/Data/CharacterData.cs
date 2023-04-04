using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
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
    protected float _curHealth;
    public float _maxHealth;
    protected float _minHealth;

    //For the purposes of saving, it may be wise to implement functionality that saves only IDs of
    //classes set up in the system, rather than saving instances of each class




}
