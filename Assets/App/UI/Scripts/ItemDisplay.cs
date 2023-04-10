using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemDisplay 
{
    public VisualElement root;
    public DataPopulater populater = new DataPopulater();
    private VisualTreeAsset elementSlot;
    
    
   [Header("Data Fields")]
    public TextField Name;
    public TextField Description;
    public FloatField Value;
    public FloatField Weight;
    public SliderInt Level;

    public ItemDisplay(VisualElement root, VisualTreeAsset attributeElementSlot)
    {
        this.root = root;
        elementSlot = attributeElementSlot;
        Name = root.Q<TextField>("id-textfield-name");
        Description = root.Q<TextField>("id-textfield-description");
        Value = root.Q<FloatField>("id-floatfield-value");
        Weight = root.Q<FloatField>("id-floatfield-weight");
        Level = root.Q<SliderInt>("id-slider-level");

    }

    public void DisplayItem(Item item)
    {
        Name.value = item._name;
        Description.value = item._description;
        if (Value != null)
        {
            Value.value = item._value;
        }
        if (Weight != null)
        {
            Weight.value = item._weight;
        }
        if (Level != null)
        {
            Level.value = item._level;
        }
    }

    public void SetDisplayData(SystemData parentSystem, bool editable)
    {
        if (parentSystem == null)
        {
            Debug.LogError("No parent system found - characters can't exist outside a system. Have you loaded a save?");
        }
        Name.isReadOnly = !editable;
        Description.isReadOnly = !editable;

        if (!parentSystem.useWeight)
        {
            Weight.RemoveFromHierarchy();
        }
        else
        {
            Weight.isReadOnly = !editable;
        }

        if (!parentSystem.charsHaveValue)
        {
            Value.RemoveFromHierarchy();
        }
        else
        {
            Value.isReadOnly = !editable;
        }

        if (!parentSystem.useLevels)
        {
            Level.RemoveFromHierarchy();
        }
        else
        {
            Level.highValue = parentSystem.attMaxLevel;
            Level.SetEnabled(editable);
        }

    }

    public void ResetFields()
    {
        Name.value = string.Empty;
        Description.value = string.Empty;
        Weight.value = 0;
        Value.value = 0;
        Level.value = 0;
    }

}
