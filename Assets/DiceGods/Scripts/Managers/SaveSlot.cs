using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveSlot 
{
    public Label saveName;

    public void SetVisualElement(VisualElement visualElement)
    {
        saveName = visualElement.Q<Label>("ss-label-name");
    }

    public void SetSlotData(string file)
    {
        saveName.text = file;
    }
}
