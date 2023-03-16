using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu_Manager : UI_Manager
{
    [SerializeField] private UIDocument mainMenu;
    [SerializeField] private UIDocument loadMenu;
    [SerializeField] private UIDocument popupWindow;
    [SerializeField] private VisualTreeAsset saveSlot;
    private PopupWindow_Manager popupWindowManager;
   
    [Header("Main Menu Elements")]
    private Button newSystem;
    private Button loadSystem;
    private Button quit;

    [Header("Load Menu Elements")]
    private Button loadSelectedSystem;
    private Button mmButton;
    private Button deleteButton;
    public string selectedSave = string.Empty;
    

    [Header("Popup")]
    private Button confirmButton;

    private void Awake()
    {
        popupWindowManager = popupWindow.GetComponent<PopupWindow_Manager>();
    }

    private void OnEnable()
    {
        
        var mm_root = mainMenu.rootVisualElement;
        var lm_root = loadMenu.rootVisualElement;
        var popup_root = popupWindow.rootVisualElement;
        if (mm_root == null || lm_root == null)
        {
            Debug.Log("No root element found");
        }

        var saveManager = new SaveSlot_Manager();

        saveManager.InitialiseSaveList(lm_root, saveSlot);

        confirmButton = popup_root.Q<Button>("popup-button-confirm");
        
        //main menu screen
        newSystem = mm_root.Q<Button>("mm-button-new");
        loadSystem = mm_root.Q<Button>("mm-button-load");
        quit = mm_root.Q<Button>("mm-button-quit");
        newSystem.clickable.clicked += () => OnNewSystemClicked();
        loadSystem.clickable.clicked += () => disableMainMenu();
        quit.clickable.clicked += () => ConfirmQuit();

        //Load screen
        loadSelectedSystem = lm_root.Q<Button>("lm-button-load");
        mmButton = lm_root.Q<Button>("lm-button-return");
        deleteButton = lm_root.Q<Button>("lm-button-delete");
        
        mmButton.clickable.clicked += () => enableMainMenu();
        loadSelectedSystem.clickable.clicked += () => OnLoadSystemClicked();
        deleteButton.clickable.clicked += () => ConfirmDelete();
        
        deleteButton.SetEnabled(false);
 

    }

    private void Start()
    {
        enableMainMenu();
   
    }

    private void DataSpecificDisable()
    {
        if (!DataPersistenceManager.instance.HasSaveData())
        {
            loadSystem.SetEnabled(false);
        }

    }

    public void OnNewSystemClicked()
    {
        DisableMenuButtons();
        //DataPersistenceManager.instance.NewSave();
        SceneManager.LoadSceneAsync("DG_SystemCreator");
    }

    public void OnLoadSystemClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("DG_Tavern");

    }

    public void DeleteSave(string saveID)
    {
        DataPersistenceManager.instance.DeleteSaveData(saveID);
        EnableMenuButtons();
        popupWindowManager.Cancel();
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
        DataSpecificDisable();
    }

    public void enableMainMenu()
    {
        mainMenu.sortingOrder = 2;
        loadMenu.sortingOrder = 1;
        popupWindow.sortingOrder = 0;
        DataSpecificDisable();
    }

    public void disableMainMenu()
    {
        mainMenu.sortingOrder = 1;
        loadMenu.sortingOrder = 2;
        popupWindow.sortingOrder = 0;
        DataSpecificDisable();
    }

    public void ConfirmDelete()
    {
        DisableMenuButtons();
        confirmButton.clickable.clicked += () => DeleteSave(selectedSave);
        popupWindowManager.SetPromptText("Are you sure you want to delete this save?");
        popupWindow.sortingOrder = 3;
    }

    private void ConfirmQuit()
    {
        DisableMenuButtons();
        confirmButton.clickable.clicked += () => OnQuitClicked();
        popupWindowManager.SetPromptText("Are you sure you want to quit to desktop?");
        popupWindow.sortingOrder = 3;
    }


}
