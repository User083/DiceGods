using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DataPopulater 
{

    public void PopulateAttributes(SystemData newSystem, VisualTreeAsset elementSlot, Foldout foldout, bool editable)
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

            if(editable)
            {
                newEntryLogic.SetAttributeDataChar(attribute);
            }
            else
            {
                newEntryLogic.SetAttributeData(attribute);
            }

            newSlotEntry.style.minHeight = 200;
            foldout.Insert(attI, newSlotEntry);

            attI++;
        };

    }

    public void PopulateCharacterAttributes(VisualTreeAsset elementSlot, Foldout foldout, Character character)
    {
        int attI = 0;
        List<Attribute> attributes = new List<Attribute>();
        attributes = character._attributes;

        foreach (var attribute in attributes)
        {
            var newSlotEntry = elementSlot.Instantiate();
            var newEntryLogic = new ElementSlot();
            newSlotEntry.userData = newEntryLogic;
            newEntryLogic.SetVisualElement(newSlotEntry);
            newEntryLogic.SetAttributeData(attribute);
            newSlotEntry.style.minHeight = 200;
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
            newSlotEntry.style.minHeight = 150;
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
            newSlotEntry.style.minHeight = 150;
            foldout.Insert(classI, newSlotEntry);
            classI++;
        };
    }

    public List<Attribute> allAttributes;
    public void EnumerateAttributes(SystemData activeSystem)
    {
        allAttributes = new List<Attribute>();
        allAttributes.AddRange(activeSystem.attributes);

    }

    public List<ElementSlot> elementSlots = new List<ElementSlot>();
    public Dictionary<Attribute, ElementSlot> AttributeSlots = new Dictionary<Attribute, ElementSlot>();
    public ListView PopulateAttributeList(VisualTreeAsset elementSlot, bool charDisplay)
    {
        ListView list = new ListView();
        

        list.makeItem = () =>
        {
            var newSlotEntry = elementSlot.Instantiate();

            var newEntryLogic = new ElementSlot();

            newSlotEntry.userData = newEntryLogic;

            newEntryLogic.SetVisualElement(newSlotEntry);

            

             return newSlotEntry;
        };


        list.bindItem = (item, index) =>
        {
            if(charDisplay)
            {
                (item.userData as ElementSlot).SetAttributeDataChar(allAttributes[index]);
                AttributeSlots.Add(allAttributes[index], item.userData as ElementSlot);
            }
            else
            {
                (item.userData as ElementSlot).SetAttributeData(allAttributes[index]);
            }
            
        };

        if(charDisplay)
        {
            list.fixedItemHeight = 200;
        }
        else
        {
            list.fixedItemHeight = 250;
        }
        list.itemsSource = allAttributes;

        return list;
    }

    List<Race> allRaces;
    public void EnumerateRaces(SystemData activeSystem)
    {
        allRaces = new List<Race>();
        allRaces.AddRange(activeSystem.races);

    }
    public ListView PopulateRaceList(VisualTreeAsset elementSlot)
    {
        ListView list = new ListView();

        list.makeItem = () =>
        {
            var newSlotEntry = elementSlot.Instantiate();

            var newEntryLogic = new ElementSlot();

            newSlotEntry.userData = newEntryLogic;

            newEntryLogic.SetVisualElement(newSlotEntry);

            return newSlotEntry;
        };


        list.bindItem = (item, index) =>
        {
            (item.userData as ElementSlot).SetRaceData(allRaces[index]);
        };

        list.fixedItemHeight = 200;
        list.itemsSource = allRaces;

        return list;
    }

    List<CharacterClass> allClasses;
    public void EnumerateClasses(SystemData activeSystem)
    {
        allClasses = new List<CharacterClass>();
        allClasses.AddRange(activeSystem.characterClasses);

    }
    public ListView PopulateClassList(VisualTreeAsset elementSlot)
    {
        ListView list = new ListView();

        list.makeItem = () =>
        {
            var newSlotEntry = elementSlot.Instantiate();

            var newEntryLogic = new ElementSlot();

            newSlotEntry.userData = newEntryLogic;

            newEntryLogic.SetVisualElement(newSlotEntry);

            return newSlotEntry;
        };


        list.bindItem = (item, index) =>
        {
            (item.userData as ElementSlot).SetCharClassData(allClasses[index]);
        };

        list.fixedItemHeight = 200;
        list.itemsSource = allClasses;

        return list;
    }

    List<Character> allCharacters;

    public void EnumerateCharacters(SaveData activeSave)
    {
         allCharacters = new List<Character>();
        allCharacters.AddRange(activeSave.characterList);

    }

    List<Item> allItems;
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

    public void RemoveByID(SystemData system, string ID, string type, Foldout foldout, VisualElement slot)
    {
        switch(type)
        {
            case "race":

                foreach (var race in system.races)
                {
                    if (race._ID == ID)
                    {
                        system.races.Remove(race);
                    }
                }
                foldout.Remove(slot);

                break;

            case "class":

                foreach (var charClass in system.characterClasses)
                {
                    if (charClass._ID == ID)
                    {
                        system.characterClasses.Remove(charClass);
                    }
                }
                foldout.Remove(slot);

                break;

            case "attribute":

                foreach (var att in system.attributes)
                {
                    if (att._ID == ID)
                    {
                        system.attributes.Remove(att);
                    }
                }
                foldout.Remove(slot);

                break;

        }
    }

}
