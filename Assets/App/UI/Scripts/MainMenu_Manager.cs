using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager UIManager;
   
   
    [Header("Main Menu Elements")]
    private Button newSystem;
    private Button loadSystem;
    private Button quit;

    [Header("Load Menu Elements")]
    private Button loadSelectedSystem;
    private Button mmButton;
    private Button deleteButton;
    private ListView loadList;
    private SaveData selectedSave;

    [Header("Popup")]
    private Button confirmButton;


    private void OnEnable()
    {
        
        var mm_root = UIManager.mainMenu.rootVisualElement;
        var lm_root = UIManager.loadMenu.rootVisualElement;
        var popup_root = UIManager.popupWindow.rootVisualElement;

        
        if (mm_root == null || lm_root == null)
        {
            Debug.Log("No root element found");
        }

        confirmButton = popup_root.Q<Button>("popup-button-confirm");
        
        //main menu screen
        newSystem = mm_root.Q<Button>("mm-button-new");
        loadSystem = mm_root.Q<Button>("mm-button-load");
        quit = mm_root.Q<Button>("mm-button-quit");
        newSystem.clickable.clicked += () => enableSystemMenu();
        loadSystem.clickable.clicked += () => enableLoadMenu();
        quit.clickable.clicked += () => ConfirmQuit();

        //Load screen
        loadSelectedSystem = lm_root.Q<Button>("lm-button-load");
        mmButton = lm_root.Q<Button>("lm-button-return");
        deleteButton = lm_root.Q<Button>("lm-button-delete");
        loadList = lm_root.Q<ListView>("lm-listview");
        
        mmButton.clickable.clicked += () => enableMainMenu();
        loadSelectedSystem.clickable.clicked += () => OnLoadSystemClicked();
        deleteButton.clickable.clicked += () => ConfirmDelete();
        
        deleteButton.SetEnabled(false);
        loadSelectedSystem.SetEnabled(false);
        loadList.onSelectionChange += OnSaveChange;

    }

    private void Start()
    {

        UIManager.saveManager.InitialiseSaveList(UIManager.loadMenu.rootVisualElement);
    }


    public void OnLoadSystemClicked()
    {
        if(selectedSave == null)
        {
            Debug.LogError("No save selected - unable to load.");
            return;
        }
        else
        {
            DataPersistenceManager.instance.updateActiveSave(selectedSave.saveID);
            
            DisableMenuButtons();
            SceneManager.LoadSceneAsync("DG_Tavern");
        }
    }

    public void DeleteSave(string saveID)
    {
        DataPersistenceManager.instance.DeleteSaveData(saveID);
        UIManager.saveManager.removeSaveFromList(selectedSave);
        EnableMenuButtons();
        UIManager.popupWindowManager.Cancel();
    }

      public void OnQuitClicked()
    {
        Application.Quit();
    }


    private void DisableMenuButtons()
    {
        newSystem.SetEnabled(false);
        loadSystem.SetEnabled(false);
        loadSelectedSystem.SetEnabled(false);
        mmButton.SetEnabled(false);
        quit.SetEnabled(false);
    }

    private void EnableMenuButtons()
    {
        newSystem.SetEnabled(true);
        loadSystem.SetEnabled(true);
        loadSelectedSystem.SetEnabled(true);
        mmButton.SetEnabled(true);
        quit.SetEnabled(true);
      
    }

    public void enableMainMenu()
    {
        UIManager.mainMenu.sortingOrder = 2;
        UIManager.loadMenu.sortingOrder = 1;
        UIManager.popupWindow.sortingOrder = 0;
        UIManager.systemCreator.sortingOrder = 0;

    }

    public void enableLoadMenu()
    {
        UIManager.mainMenu.sortingOrder = 1;
        UIManager.loadMenu.sortingOrder = 2;
        UIManager.popupWindow.sortingOrder = 0;
        UIManager.systemCreator.sortingOrder = 0;
    }

    public void OnSaveChange(IEnumerable<object> selectedItems)
    {
        var currentSave = loadList.selectedItem as SaveData;

        if (currentSave == null)
        {
            selectedSave = null;
            deleteButton.SetEnabled(false);
            loadSelectedSystem.SetEnabled(false);
            return;
        }

        selectedSave = currentSave;
        deleteButton.SetEnabled(true);
        loadSelectedSystem.SetEnabled(true);
        
    }
    public void enableSystemMenu()
    {
        UIManager.mainMenu.sortingOrder = 1;
        UIManager.loadMenu.sortingOrder = 1;
        UIManager.popupWindow.sortingOrder = 0;
        UIManager.systemCreator.sortingOrder= 2;
        UIManager.systemManager.LaunchSystemCreator();
    }

    public void ConfirmDelete()
    {
        DisableMenuButtons();
        confirmButton.clickable.clicked += () => DeleteSave(selectedSave.saveID);
        UIManager.popupWindowManager.SetPromptText("Are you sure you want to delete this save?");
        UIManager.popupWindow.sortingOrder = 3;

    }

    private void ConfirmQuit()
    {
        confirmButton.clickable.clicked += () => OnQuitClicked();
        UIManager.popupWindowManager.SetPromptText("Are you sure you want to quit to desktop?");
        UIManager.popupWindow.sortingOrder = 3;
    }


}
