using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DataPopulater 
{
    
    public void PopulateAttributes(SystemData newSystem, VisualTreeAsset elementSlot, Foldout foldout)
    {
        int attI = 0;
        List<Attribute> attributes = new List<Attribute>();
        attributes = newSystem.attributes;

        foreach (var attribute in attributes)
        {
            var newSlotEntry = elementSlot.Instantiate();
            var newEntryLogic = new ElementSlot();
            newSlotEntry.userData = newEntryLogic;
            newEntryLogic.SetVisualElement(newSlotEntry);
            newEntryLogic.SetAttributeData(attribute);

            foldout.Insert(attI, newSlotEntry);
            attI++;
        };

    }

    public void PopulateRaces(SystemData newSystem, VisualTreeAsset elementSlot, Foldout foldout)
    {
        int raceI = 0;
        List<Race> races = new List<Race>();
        races = newSystem.races;

        foreach (var race in races)
        {
            var newSlotEntry = elementSlot.Instantiate();
            var newEntryLogic = new ElementSlot();
            newSlotEntry.userData = newEntryLogic;
            newEntryLogic.SetVisualElement(newSlotEntry);
            newEntryLogic.SetRaceData(race);

            foldout.Insert(raceI, newSlotEntry);
            raceI++;
        };
    }

    public void PopulateClasses(SystemData newSystem, VisualTreeAsset elementSlot, Foldout foldout)
    {
        int classI = 0;
        List<CharacterClass> classes = new List<CharacterClass>();
        classes = newSystem.characterClasses;

        foreach (var charClass in classes)
        {
            var newSlotEntry = elementSlot.Instantiate();
            var newEntryLogic = new ElementSlot();
            newSlotEntry.userData = newEntryLogic;
            newEntryLogic.SetVisualElement(newSlotEntry);
            newEntryLogic.SetCharClassData(charClass);

            foldout.Insert(classI, newSlotEntry);
            classI++;
        };
    }
}
