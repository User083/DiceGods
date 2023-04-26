using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DefaultSystemData 
{

    [Header("Default Attributes")]
    public Attribute strength;
    public Attribute intelligence;
    public Attribute wisdom;
    public Attribute dexterity;
    public Attribute persuasion;

    [Header("Default Classes")]
    public CharacterClass warrior;
    public CharacterClass mage;
    public CharacterClass rogue;

    [Header("Default Classes")]
    public Race human;
    public Race elf;
    public Race dwarf;

    [Header("Default Stats")]
    public Stat health;
    public Stat stamina;
    public Stat mana;
    public Stat defence;
    public List<Attribute> InitAttributes(SystemData parentSystem)
    {
        List<Attribute> attributes = new List<Attribute>();
        strength = new Attribute(parentSystem, "Strength", "Measure of a character's raw physical ability", 10);
        attributes.Add(strength);
        intelligence = new Attribute(parentSystem, "Intelligence", "Measure of a character's analytical and deductive abilities", 10);
        attributes.Add(intelligence);
        wisdom = new Attribute(parentSystem, "Wisdom", "Measure of a character's willpower and knowledge of the world", 10);
        attributes.Add(wisdom);
        dexterity = new Attribute(parentSystem, "Dexterity", "Measure of a character's agility and aim", 10);
        attributes.Add(dexterity);
        persuasion = new Attribute(parentSystem, "Persuasion", "Measure of a character's ability to persuade others", 10);
        attributes.Add(persuasion);

        return attributes;
    }

    public List<CharacterClass> InitClasses(SystemData parentSystem)
    {
        List<CharacterClass> characterClasses= new List<CharacterClass>();

        warrior = new CharacterClass(parentSystem, "Warrior", "Strong class with focus on melee");
        characterClasses.Add(warrior);
        mage = new CharacterClass(parentSystem, "Mage", "Powerful magic users");
        characterClasses.Add(mage);
        rogue = new CharacterClass(parentSystem, "Rogue", "Dexterous and stealthy");
        characterClasses.Add(rogue);

        return characterClasses;
    }

    public List<Race> InitRaces(SystemData parentSystem)
    {
        List<Race> races = new List<Race>();

        human = new Race(parentSystem, "Human", "A balanced race");
        races.Add(human);
        elf = new Race(parentSystem, "Elf", "A lithe and magical race");
        races.Add(elf);
        dwarf = new Race(parentSystem, "Dwarf", "A sturdy, warrior-like race");
        races.Add(dwarf);

        return races;
    }

    public List<Stat> InitStats(SystemData parentSystem)
    {
        List<Stat> stats = new List<Stat>();

        health = new Stat(parentSystem, "Health", "A measure of vitality", 100);
        stats.Add(health);
        stamina = new Stat(parentSystem, "Stamina", "A measure of physical endurance", 200);
        stats.Add(stamina);
        mana = new Stat(parentSystem, "Mana", "A measure of magical endurance", 150);
        stats.Add(mana);
        defence = new Stat(parentSystem, "Defence", "A measure of damage resistance", 50);
        stats.Add(defence);

        return stats;
    }

}
