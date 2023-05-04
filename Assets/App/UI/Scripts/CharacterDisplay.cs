using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    public ListView AttList = new ListView();
    public IntegerField LevelDisplay;
    public GroupBox levelBox;
    public Label levelLabel;
    public VisualElement contentContainer;
    public Label attLabel;
    public Foldout statFoldout;
    public TextField raceDisplay;
    public TextField classDisplay;

    [Header("Data")]
    private Dictionary<string, string> RaceDictionary = new Dictionary<string, string>();
    private Dictionary<string, string> ClassDictionary = new Dictionary<string, string>();
    public List<Attribute> attCreationList = new List<Attribute>(); 
    public Attribute selectedAtt;

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
        raceDisplay = root.Q<TextField>("cd-textfield-race");
        classDisplay = root.Q<TextField>("cd-textfield-class");
        LevelDisplay = root.Q<IntegerField>("cd-intfield-level");
        levelBox = root.Q<GroupBox>("cd-levelbox");
        levelLabel = root.Q<Label>("cd-label-level");
        attLabel = root.Q<Label>("cd-label-attributes");
        statFoldout = root.Q<Foldout>("cd-foldout-stats");
        contentContainer = root.Q<VisualElement>("cd-content-container");
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

        if(Attributes != null)
        {
            Attributes.Clear();
            populater.PopulateCharacterAttributes(elementSlot, Attributes, currentChar);
        }

        if(statFoldout != null)
        {
            statFoldout.Clear();
            populater.PopulateCharacterStats(elementSlot, statFoldout, currentChar);
        }

        if(raceDisplay != null)
        {
            raceDisplay.value = currentChar._race._name;
        }

        if(classDisplay != null)
        {
            classDisplay.value = currentChar._class._name;
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
            AttList.RemoveFromHierarchy();
            attLabel.RemoveFromHierarchy();
        }
        else
        {
   
            if(editable)
            {
                populater.EnumerateAttributes(parentSystem);
                AttList = populater.PopulateAttributeList(elementSlot, true);
                if (contentContainer.Contains(AttList))
                {
                    contentContainer.Remove(AttList);
                }
                contentContainer.Add(AttList);
                AttList.onSelectionChange += AttChange;
                Attributes.RemoveFromHierarchy();
            }
            else
            {
                if (contentContainer.Contains(AttList))
                {
                    contentContainer.Remove(AttList);
                }
                AttList.RemoveFromHierarchy();
                attLabel.RemoveFromHierarchy();
                Attributes.SetEnabled(true);
            }
            
        }

        if(!parentSystem.useCoreStats)
        {
            statFoldout.RemoveFromHierarchy();
        }
        else
        {
            populater.PopulateStats(parentSystem, elementSlot, statFoldout);
            statFoldout.SetEnabled(true);
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
            classDisplay.RemoveFromHierarchy();
        }
        else
        {
            if(editable)
            {
                Class.SetEnabled(editable);
                classDisplay.RemoveFromHierarchy();
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
            else
            {
                Class.RemoveFromHierarchy();
                classDisplay.isReadOnly = true;
            }

        }

        if(!parentSystem.useRaces)
        {
            Race.RemoveFromHierarchy();
            raceDisplay.RemoveFromHierarchy();
        }
        else
        {
            if(editable)
            {
                Race.SetEnabled(editable);
                raceDisplay.RemoveFromHierarchy();
                foreach (var race in parentSystem.races)
                {
                    RaceDictionary.Add(race._ID, race._name);
                }

                if (RaceDictionary.Count > 0)
                {
                    Race.choices.Clear();
                    foreach (KeyValuePair<string, string> item in RaceDictionary)
                    {
                        Race.choices.Add(item.Value);

                    }
                    Race.value = Race.choices[0];
                }
            }
            else
            {
                Race.RemoveFromHierarchy();
                raceDisplay.isReadOnly = true;
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

        if(DataPersistenceManager.instance.activeSave.parentSystem.useAttributes)
        {
            foreach (var item in populater.AttributeSlots)
            {

                item.Value.defaultValueSlider.value = 10;


            }
        }

    }

    public int GetAttValue(VisualElement slot)
    {
        int attVal = 0;
        var SlotLogic = new ElementSlot();
        slot.userData = SlotLogic;
       SlotLogic.SetVisualElement(slot);
        SlotLogic.TestAtt();
        attVal = SlotLogic.attValue;
        return attVal;

    }

    public List<Attribute> UpdateAttValues()
    {
        Dictionary<Attribute, ElementSlot> Slots = new Dictionary<Attribute, ElementSlot>();
        Slots = populater.AttributeSlots;
        foreach(var item in Slots )
        {
            if(item.Key.base_value != item.Value.attValue)
            {
                item.Key.base_value = item.Value.attValue;
            }

        }

        return Slots.Keys.ToList();
    }

   

    public void AttChange(IEnumerable<object> selectedItems)
    {
        var currentAtt = AttList.selectedItem as Attribute;

        if (currentAtt == null)
        {
            selectedAtt = null;
            return;
        }

        selectedAtt = currentAtt;
    


    }

}
