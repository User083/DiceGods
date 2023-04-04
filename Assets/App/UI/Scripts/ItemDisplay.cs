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

}
