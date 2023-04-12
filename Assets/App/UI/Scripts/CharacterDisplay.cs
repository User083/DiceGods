using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
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
    public IntegerField LevelDisplay;
    public GroupBox levelBox;
    public Label levelLabel;

    [Header("Data")]
    private Dictionary<string, string> RaceDictionary = new Dictionary<string, string>();
    private Dictionary<string, string> ClassDictionary = new Dictionary<string, string>();


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
        LevelDisplay = root.Q<IntegerField>("cd-intfield-level");
        levelBox = root.Q<GroupBox>("cd-levelbox");
        levelLabel = root.Q<Label>("cd-label-level");
        Level.RegisterCallback<ChangeEvent<int>>((e) => { levelLabel.text=e.newValue.ToString(); });
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
        if (LevelDisplay != null)
        {
            LevelDisplay.value = currentChar._level;
        }

    }
    public void SetDisplayData(SystemData parentSystem, bool editable)
    {
        if(parentSystem == null)
        {
            Debug.LogError("No parent system found - characters can't exist outside a system. Have you loaded a save?");
        }
        Name.isReadOnly = !editable;
        Description.isReadOnly = !editable;
        Value.isReadOnly = !editable;
        Weight.isReadOnly = !editable;
        
        if(!parentSystem.useAttributes)
        {
            Attributes.RemoveFromHierarchy();
        }
        else
        {

            populater.PopulateAttributes(parentSystem, elementSlot, Attributes);
            Attributes.SetEnabled(true);
        }

        if(!parentSystem.useWeight)
        {
            Weight.RemoveFromHierarchy();
        }
        else
        {
            Weight.isReadOnly = !editable;
        }

        if(!parentSystem.charsHaveValue)
        {
            Value.RemoveFromHierarchy();
        }
        else
        {
            Value.SetEnabled(editable);
        }

        if(!parentSystem.useLevels)
        {
            levelBox.RemoveFromHierarchy();
            LevelDisplay.RemoveFromHierarchy();
        }
        else
        {
           if(editable)
            {
                Level.highValue = parentSystem.attMaxLevel;
                Level.SetEnabled(editable);
                LevelDisplay.RemoveFromHierarchy();
            }
            else
            {
                levelBox.RemoveFromHierarchy();
                
            }

        }

        if(!parentSystem.useClasses)
        {
            Class.RemoveFromHierarchy();
        }
        else
        {
            Class.SetEnabled(editable);
            foreach (var charClass in parentSystem.characterClasses)
            {
                ClassDictionary.Add(charClass._ID, charClass._name);
            }

            if (ClassDictionary.Count > 0)
            {
                Class.choices.Clear();
                foreach (KeyValuePair<string, string> item in ClassDictionary)
                {
                    Class.choices.Add(item.Value);
                }
                Class.value = Class.choices[0];
            }
        }

        if(!parentSystem.useRaces)
        {
            Race.RemoveFromHierarchy();
        }
        else
        {
            Race.SetEnabled(editable);
            foreach(var race in parentSystem.races)
            {
                RaceDictionary.Add(race._ID, race._name);
            }

            if(RaceDictionary.Count > 0)
            {
                Race.choices.Clear();
                foreach(KeyValuePair<string, string> item in RaceDictionary)
                {
                    Race.choices.Add(item.Value);

                }
                Race.value = Race.choices[0];
            }
        }

    }

    public void ResetFields()
    {
        Name.value = string.Empty; 
        Description.value = string.Empty;
        Race.value = Race.choices[0];
        Class.value = Class.choices[0];
        Weight.value = 0;
        Value.value = 0;
        Level.value = 0;
    }
}
