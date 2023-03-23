using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Item : MonoBehaviour
{
    public string _ID;
    public string _name;
    public string _description;
    public float _value;
    public float _weight;
    public int _level;
    public float _quality;

    public Item(string name, string desc)
    {
        _name = name;
        _description = desc;
        _value = -1;
        _weight = -1;
        _level = -1;
        _quality = -1;
      
    }
}
