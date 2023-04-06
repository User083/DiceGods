using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class SingleSlot
{
    public Label slotName;
    public string slotID;

    public void SetVisualElement(VisualElement visualElement)
    {
        slotName = visualElement.Q<Label>("ss-label-name");
    }

    public void SetCharData(Character character)
    {
        slotName.text = character._name;
        slotID = character._ID;
    }

    public void SetItemData(Item item)
    {
        slotName.text = item._name;
        slotID = item._ID;
    }

    public void SetSaveData(SaveData save)
    {
        slotName.text = save.fileName;
        slotID = save.saveID;
    }

}
