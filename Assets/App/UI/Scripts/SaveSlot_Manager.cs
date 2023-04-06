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

    
    List<string> saveList;

    void EnumerateAllSaves()
    {
        saveList= new List<string>();
        saveList = DataPersistenceManager.instance.LoadAllSaves().Keys.ToList();
       

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

        saveListView.fixedItemHeight = 30;
        saveListView.itemsSource = saveList;
        
    }

    public void removeSaveFromList(string saveID)
    {
        if(saveID != null || saveID != string.Empty)
        {
            saveList.Remove(saveID);
            saveListView.Rebuild();
        }
        
    }


}
