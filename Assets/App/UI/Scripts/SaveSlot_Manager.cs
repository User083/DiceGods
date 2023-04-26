using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class SaveSlot_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager UIManager;
    public VisualTreeAsset saveSlotElement;
    ListView saveListView;
   
    public void InitialiseSaveList(VisualElement root)
    {
        EnumerateAllSaves();
        saveListView = root.Q<ListView>("lm-listview");
        
        FillSaveList();
    }

    
    List<SaveData> saveList;

    void EnumerateAllSaves()
    {
        saveList= new List<SaveData>();
        saveList = DataPersistenceManager.instance.LoadAllSaves().Values.ToList();
       

    }

    void FillSaveList()
    {
        saveListView.makeItem = () =>
        {
            var newSlotEntry = saveSlotElement.Instantiate();

            var newEntryLogic = new SingleSlot();

            newSlotEntry.userData = newEntryLogic;

            newEntryLogic.SetVisualElement(newSlotEntry);

            return newSlotEntry;
        };

        saveListView.bindItem = (item, index) =>
        {
            (item.userData as SingleSlot).SetSaveData(saveList[index]);
        };

        saveListView.fixedItemHeight = 45;
        saveListView.itemsSource = saveList;
        
    }

    public void removeSaveFromList(SaveData save)
    {
        if(save != null)
        {
                    saveList.Remove(save);
                    saveListView.Rebuild();

        }
        
    }


}
