using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ElementSlot 
{
    public Label title;
    public TextField name;
    public TextField description;
    public IntegerField defaultValue;
    public IntegerField maxValue;
    public void SetVisualElement(VisualElement visualElement)
    {
        title = visualElement.Q<Label>("se-label-title");
        name = visualElement.Q<TextField>("se-textfield-name");
        description = visualElement.Q<TextField>("se-textfield-description");
        defaultValue = visualElement.Q<IntegerField>("se-intfield-default");
        maxValue = visualElement.Q<IntegerField>("se-intfield-max");
    }

    public void SetAttributeData(Attribute att)
    {
        title.text = att._name;
        name.label = "Name:";
        description.label = "Description:";
        defaultValue.label = "Default Value:";
        maxValue.label = "Max Value:";
        name.value = att._name;
        description.value = att._description;
        defaultValue.value = att.base_value;
        maxValue.value = att.max_value;
    }

    public void SetCharClassData(CharacterClass charClass)
    {
        title.text = charClass._name;
        name.label = "Name:";
        description.label = "Description:";
        defaultValue.RemoveFromHierarchy();
        maxValue.RemoveFromHierarchy();
        name.value = charClass._name;
        description.value = charClass._description;
        
    }

    public void SetRaceData(Race race)
    {
        title.text = race._name;
        name.label = "Name:";
        description.label = "Description:";
        defaultValue.RemoveFromHierarchy();
        maxValue.RemoveFromHierarchy();
        name.value = race._name;
        description.value = race._description;

    }

    
}
