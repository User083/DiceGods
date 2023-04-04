using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Character : CharacterData
{
    
   
    //Default blank character values
    public Character(SystemData parentSystem,string name, string desc)
    {
        //Values that can never be null
        _name = name;
        _SystemID = parentSystem.systemID;
        _description = desc;
        _ID = generateCharacterID(name);
        //Following values default to -1 if system doesn't use them
        _value = -1;
        _weight = -1;
        _level = -1;
        //Values constructs to default at creation, can then be changed to custom system classes if the system uses them
        _race = new Race(parentSystem);
        _class = new CharacterClass(parentSystem);
        //Character defaults to system's attributes
        _attributes = parentSystem.attributes;
        //Default health setup
        _minHealth = 0;
        _maxHealth = 100;
        _curHealth = _maxHealth;
    }

    private string generateCharacterID(string characterName)
    {
        string random = Random.Range(0, 9999).ToString();
        string characterID = characterName + random;

        return characterID;
    }

    private void SetupHealth()
    {
        
    }
}
