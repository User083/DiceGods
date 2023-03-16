using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class SaveSlot_Manager 
{
    VisualTreeAsset saveSlotElement;
    ListView saveListView;
    
    public void InitialiseSaveList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllSaves();

        saveSlotElement = listElementTemplate;

        saveListView = root.Q<ListView>("lm-listview");

        FillSaveList();
    }

    Dictionary<string, SaveData> savesDictionary;
    List<string> saveList;

    void EnumerateAllSaves()
    {
        savesDictionary = DataPersistenceManager.instance.GetAllSaves();
        saveList = savesDictionary.Keys.ToList();
      
    }

    void FillSaveList()
    {
        saveListView.makeItem = () =>
        {
            var newSlotEntry = saveSlotElement.Instantiate();

            var newEntryLogic = new SaveSlot();

            newSlotEntry.userData = newEntryLogic;

            newEntryLogic.SetVisualElement(newSlotEntry);

            return newSlotEntry;
        };

        saveListView.bindItem = (item, index) =>
        {
            (item.userData as SaveSlot).SetSlotData(saveList[index]);
        };

        saveListView.fixedItemHeight = 45;
        saveListView.itemsSource = saveList;
        
    }

    


}
