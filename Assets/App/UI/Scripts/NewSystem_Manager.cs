using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[System.Serializable]

public class NewSystem_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager UIManager;
    [SerializeField] private UIDocument newSystemUI;
    [SerializeField] private VisualTreeAsset systemDisplayUI;
    [SerializeField] private VisualTreeAsset elementSlotUI;
    [SerializeField] private VisualTreeAsset editorDocUI;
    [SerializeField] private UIDocument editorPopupDocUI;
    private DataPopulater dataPopulater = new DataPopulater();
    private SystemDisplay system;
    private VisualElement systemDisplay;
    private VisualElement editorDisplay;
    private SaveData newSystemSaveData;   
    private Button createButton;
    private Button resetButton;
    private Button mainMenu;
    private Button confirmButton;
    private VisualElement mainPanel;

    [Header("Core System Details")]
    public SystemData newSystem;
    private string saveName;


    [Header("Core System Details UI Elements")]
    private TextField saveNameField;

    private void Awake()
    {
        newSystem = new SystemData("New System");
    }
    private void OnEnable()
    {
        var root = newSystemUI.rootVisualElement;
        var popupRoot = UIManager.popupWindow.rootVisualElement;
        
        mainPanel = root.Q<VisualElement>("ns-panel-main");
        systemDisplay = systemDisplayUI.Instantiate();
        systemDisplay.style.flexGrow = 1;
        
        system = new SystemDisplay(systemDisplay, elementSlotUI);
        
        if (root == null)
        {
            Debug.Log("No root element found");
        }

        //Popup
        confirmButton = popupRoot.Q<Button>("popup-button-confirm");
        saveNameField = popupRoot.Q<TextField>("popup-textfield-optional");
        
        //buttons
        createButton = root.Q<Button>("ns-button-create");
        resetButton = root.Q<Button>("ns-button-reset");
        mainMenu = root.Q<Button>("ns-button-mm");


        //Button bindings
        mainMenu.clickable.clicked += () => ConfirmCancel();
        resetButton.clickable.clicked += () => ConfirmReset();
        createButton.clickable.clicked += () => ConfirmNewSystem();

        saveNameField.RegisterCallback<ChangeEvent<string>>((e) => { saveName = e.newValue; });
        

    }

    private void Start()
    {
        system.EditorSetup(editorDocUI, editorPopupDocUI, this);
        system.attributesToggle.RegisterCallback<ChangeEvent<bool>>((e) => { system.attButton.SetEnabled(e.newValue); });
        system.classesToggle.RegisterCallback<ChangeEvent<bool>>((e) => { system.classButton.SetEnabled(e.newValue); });
        system.racesToggle.RegisterCallback<ChangeEvent<bool>>((e) => { system.raceButton.SetEnabled(e.newValue); });
        system.coreStatsToggle.RegisterCallback<ChangeEvent<bool>>((e) => { system.statsButton.SetEnabled(e.newValue); });
        system.attButton.clickable.clicked += () => system.DisplayAttributes(mainPanel);
        system.raceButton.clickable.clicked += () => system.DisplayRaces(mainPanel);
        system.classButton.clickable.clicked += () => system.DisplayClasses(mainPanel);
        system.statsButton.clickable.clicked += () => system.DisplayStats(mainPanel);
    }

    //User interface handling

    public void OnMMClicked()
    {
        UIManager.mainMenuManager.enableMainMenu();
        
    }

    public void CreateNewSystem()
    {
        
        if(newSystem.systemName != string.Empty)
        {
            SetSystemValues();
            newSystem.SetID();
            
            newSystem.UpdateIDs();
            if (newSystem.systemID != string.Empty)
            {
                newSystemSaveData = DataPersistenceManager.instance.initialiseNewSave(saveName); ;
                newSystemSaveData.parentSystem = newSystem;
                DataPersistenceManager.instance.Save(newSystemSaveData, newSystemSaveData.saveID);
                UIManager.popupWindowManager.Cancel();
                SceneManager.LoadSceneAsync("DG_Tavern");
            }
            else
            {
                Debug.Log("System ID not generated");
            }

        }
        else
        {
            UIManager.popupWindowManager.SingleButtonPrompt("Please fill in the required fields.");
            UIManager.popupWindow.sortingOrder = 3;
            confirmButton.clickable.clicked += () => UIManager.popupWindowManager.Cancel();
        }

    }

    private void resetSystemFields()
    {
        system.ResetFields();
        UIManager.popupWindowManager.Cancel();
    }

    private void ConfirmReset()
    {
        
        confirmButton.clickable.clicked += () => resetSystemFields();
        UIManager.popupWindowManager.SetPromptText("Are you sure you want reset all fields to default?");
        UIManager.popupWindow.sortingOrder = 3;

    }

    private void ConfirmCancel()
    {
        confirmButton.clickable.clicked += () => OnMMClicked();
        UIManager.popupWindowManager.SetPromptText("Are you sure you want to return to the main menu? Current changes won't be saved.");
        UIManager.popupWindow.sortingOrder = 3;
    }

    private void ConfirmNewSystem()
    {
        confirmButton.clickable.clicked += () => CreateNewSystem();
        UIManager.popupWindowManager.SetPromptText("What would you like to save this system as?");
        UIManager.popupWindowManager.RenderTextField("Save name", "New System");
        UIManager.popupWindow.sortingOrder = 3;
       
        
    }


    private void SetSystemValues()
    {
        newSystem.systemName = system.systemNameField.value;
        newSystem.useLevels = system.levelsToggle.value;
        newSystem.useClasses = system.classesToggle.value;
        newSystem.useRaces = system.racesToggle.value;
        newSystem.useCoreStats = system.coreStatsToggle.value;
        newSystem.useAttributes = system.attributesToggle.value;
        newSystem.attributes = (List<Attribute>)system.attList.itemsSource;
        newSystem.races = (List<Race>)system.raceList.itemsSource;
        newSystem.characterClasses = (List<CharacterClass>)system.classList.itemsSource;

    }

    public void PopulateData()
    {
        system.SetEditableSystem(newSystem);

    }
 
    public void LaunchSystemCreator()
    {
        PopulateData();
        mainPanel.hierarchy.Add(systemDisplay);

    }

  

}
