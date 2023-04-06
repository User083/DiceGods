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

    List<Character> allCharacters;
    List<Item> allItems;
    public void EnumerateCharacters(SaveData activeSave)
    {
         allCharacters = new List<Character>();
        allCharacters.AddRange(activeSave.characterList);

    }

    public void EnumerateItems(SaveData activeSave)
    {
        allItems = new List<Item>();
        allItems.AddRange(activeSave.itemList);

    }

    public ListView PopulateCharacters(VisualTreeAsset singleSlot)
    {
        ListView list = new ListView();

        list.makeItem = () =>
        {
            var newSlotEntry = singleSlot.Instantiate();

            var newEntryLogic = new SingleSlot();

            newSlotEntry.userData = newEntryLogic;

            newEntryLogic.SetVisualElement(newSlotEntry);

            return newSlotEntry;
        };

        
        list.bindItem = (item, index) =>
        {
            (item.userData as SingleSlot).SetCharData(allCharacters[index]);
        };

        list.fixedItemHeight = 45;
        list.itemsSource = allCharacters;

        return list;
    }

    public ListView PopulateItems(VisualTreeAsset singleSlot)
    {
        ListView list = new ListView();

        list.makeItem = () =>
        {
            var newSlotEntry = singleSlot.Instantiate();

            var newEntryLogic = new SingleSlot();

            newSlotEntry.userData = newEntryLogic;

            newEntryLogic.SetVisualElement(newSlotEntry);

            return newSlotEntry;
        };


        list.bindItem = (item, index) =>
        {
            (item.userData as SingleSlot).SetItemData(allItems[index]);
        };

        list.fixedItemHeight = 45;
        list.itemsSource = allItems;

        return list;
    }
}
