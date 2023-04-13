using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ElementSlot 
{
    public VisualElement root;
    public Label title;
    public TextField name;
    public TextField description;
    public IntegerField defaultValue;
    public IntegerField maxValue;
    public Label defaultValueLabel;
    public SliderInt defaultValueSlider;
    public GroupBox boxValue;
    public string slotID;
    public string type;
    public int index;
    public void SetVisualElement(VisualElement visualElement)
    {
        root = visualElement;
        title = root.Q<Label>("se-label-title");
        name = root.Q<TextField>("se-textfield-name");
        description = root.Q<TextField>("se-textfield-description");
        defaultValue = root.Q<IntegerField>("se-intfield-default");
        maxValue = root.Q<IntegerField>("se-intfield-max");
        defaultValueSlider = root.Q<SliderInt>("se-slider-value");
        defaultValueLabel = root.Q<Label>("se-label-value");
        boxValue = root.Q<GroupBox>("se-box-value");
    }

    public void SetDisplay(string type)
    {
        switch(type)
        {
            case "attribute":
                title.text = "New Attribute";
                name.label = "Name:";
                description.label = "Description:";
                defaultValue.label = "Default Value:";
                maxValue.label = "Max Value:";
                name.value = "Attribute Name";
                description.value = "Describe your attribute";
                defaultValue.RemoveFromHierarchy();
                defaultValueSlider.highValue = maxValue.value;
                maxValue.value = 20;
                defaultValueSlider.RegisterCallback<ChangeEvent<int>>((e) => { defaultValueLabel.text = e.newValue.ToString(); });
                maxValue.RegisterCallback<ChangeEvent<int>>((e) => { defaultValueSlider.highValue = e.newValue; });
                break;
            case "class":
                title.text = "New Class";
                name.label = "Name:";
                description.label = "Description:";
                name.value = "Class Name";
                description.value = "Describe your character class";
                defaultValue.RemoveFromHierarchy();
                maxValue.RemoveFromHierarchy();
                boxValue.RemoveFromHierarchy();
                break;
            case "race":
                title.text = "New Race";
                name.label = "Name:";
                description.label = "Description:";
                name.value = "Race Name";
                description.value = "Describe your race";
                defaultValue.RemoveFromHierarchy();
                maxValue.RemoveFromHierarchy();
                boxValue.RemoveFromHierarchy();
                break;
        }
    }

    public void SetAttributeData(Attribute att)
    {
        title.text = att._name;
        name.label = "Name:";
        description.label = "Description:";
        defaultValue.label = "Default Value:";
        maxValue.label = "Max Value:";
        boxValue.RemoveFromHierarchy();
        name.value = att._name;
        description.value = att._description;
        defaultValue.value = att.base_value;
        maxValue.value = att.max_value;
        type = "attribute";
        
    }

    public void SetCharClassData(CharacterClass charClass)
    {
        title.text = charClass._name;
        name.label = "Name:";
        description.label = "Description:";
        boxValue.RemoveFromHierarchy();
        defaultValue.RemoveFromHierarchy();
        maxValue.RemoveFromHierarchy();
        name.value = charClass._name;
        description.value = charClass._description;
        slotID = charClass._ID;
        type = "class";
       
    }

    public void SetRaceData(Race race)
    {
        title.text = race._name;
        name.label = "Name:";
        description.label = "Description:";
        boxValue.RemoveFromHierarchy();
        defaultValue.RemoveFromHierarchy();
        maxValue.RemoveFromHierarchy();
        name.value = race._name;
        description.value = race._description;
        slotID = race._ID;
        type = "race";
  
    }

    public string GetID()
    {
        return slotID;
    }

    public string GetSlotType()
    {
        return type;
    }
    
}
