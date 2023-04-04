using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterDisplay 
{

    public VisualElement root;
    public DataPopulater populater = new DataPopulater();
    private VisualTreeAsset elementSlot;
    [Header ("Data Fields")]
    public TextField Name;
    public TextField Description;
    public FloatField Value;
    public FloatField Weight;
    public SliderInt Level;
    public DropdownField Race;
    public DropdownField Class;
    public Foldout Attributes;

    public CharacterDisplay(VisualElement root, VisualTreeAsset attributeElementSlot)
    {
        this.root = root;
        elementSlot = attributeElementSlot;
        Name = root.Q<TextField>("cd-textfield-charactername");
        Description = root.Q<TextField>("cd-textfield-characterdesc");
        Value = root.Q<FloatField>("cd-floatfield-value");
        Weight = root.Q<FloatField>("cd-floatfield-weight");
        Level = root.Q<SliderInt>("cd-slider-level");
        Attributes = root.Q<Foldout>("cd-foldout-att");
        Class = root.Q<DropdownField>("cd-dropdown-class");
        Race = root.Q<DropdownField>("cd-dropdown-race");
    }

    
    public void DisplayCharacter(Character currentChar)
    {
        Name.value = currentChar._name;
        Description.value = currentChar._description;
        if (Value!= null)
        {
            Value.value = currentChar._value;
        }
        if(Weight != null)
        {
            Weight.value = currentChar._weight;
        }
        if (Level != null)
        {
            Level.value = currentChar._level;
        }

    }
    public void SetDisplayData(SystemData parentSystem, bool editable)
    {
        if(parentSystem == null)
        {
            Debug.LogError("No parent system found - characters can't exist outside a system. Have you loaded a save?");
        }
        Name.SetEnabled(editable);
        Description.SetEnabled(editable);
        Value.SetEnabled(editable);
        Weight.SetEnabled(editable);
        
        if(!parentSystem.useAttributes)
        {
            Attributes.RemoveFromHierarchy();
        }
        else
        {
            populater.PopulateAttributes(parentSystem, elementSlot, Attributes);
            Attributes.SetEnabled(editable);
        }

        if(!parentSystem.useLevels)
        {
            Level.RemoveFromHierarchy();
        }
        else
        {
            Level.highValue = parentSystem.attMaxLevel;
            Level.SetEnabled(editable);
        }

        if(!parentSystem.useClasses)
        {
            Class.RemoveFromHierarchy();
        }
        else
        {
            Class.SetEnabled(editable);
        }

        if(!parentSystem.useRaces)
        {
            Race.RemoveFromHierarchy();
        }
        else
        {
            Race.SetEnabled(editable);
        }

    }
}
