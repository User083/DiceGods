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
    private DataPopulater dataPopulater = new DataPopulater();
    private VisualElement systemDisplay;
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
    private TextField systemNameField;
    //Toggles
    private Toggle levelsToggle;
    private Toggle classesToggle;
    private Toggle racesToggle;
    private Toggle coreStatsToggle;
    private Toggle attributesToggle;
    private Toggle charsHaveValueToggle;
    private Toggle weightToggle;
    //Foldouts
    private Foldout attFoldout;
    private Foldout classFoldout;
    private Foldout racesFoldout;


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
        if (root == null)
        {
            Debug.Log("No root element found");
        }

        //Popup
        confirmButton = popupRoot.Q<Button>("popup-button-confirm");
        saveNameField = popupRoot.Q<TextField>("popup-textfield-optional");
        
        //Toggles
        levelsToggle = systemDisplay.Q<Toggle>("ns-toggle-levels");
        classesToggle = systemDisplay.Q<Toggle>("ns-toggle-classes");
        racesToggle = systemDisplay.Q<Toggle>("ns-toggle-races");
        //coreStatsToggle = root.Q<Toggle>("ns-toggle-core-stats");
        attributesToggle = systemDisplay.Q<Toggle>("ns-toggle-attributes");

        //Foldouts
        attFoldout = systemDisplay.Q<Foldout>("ns-foldout-att");
        racesFoldout = systemDisplay.Q<Foldout>("ns-foldout-races");
        classFoldout = systemDisplay.Q<Foldout>("ns-foldout-classes");

        //buttons
        createButton = root.Q<Button>("ns-button-create");
        resetButton = root.Q<Button>("ns-button-reset");
        mainMenu = root.Q<Button>("ns-button-mm");
        systemNameField = systemDisplay.Q<TextField>("ns-textfield-systemname");

        //Button bindings
        mainMenu.clickable.clicked += () => ConfirmCancel();
        resetButton.clickable.clicked += () => ConfirmReset();
        createButton.clickable.clicked += () => ConfirmNewSystem();

        attFoldout.SetEnabled(false);
        racesFoldout.SetEnabled(false);
        classFoldout.SetEnabled(false); 

        saveNameField.RegisterCallback<ChangeEvent<string>>((e) => { saveName = e.newValue; });
        attributesToggle.RegisterCallback<ChangeEvent<bool>>((e) => { attFoldout.SetEnabled(e.newValue); });
        classesToggle.RegisterCallback<ChangeEvent<bool>>((e) => { classFoldout.SetEnabled(e.newValue); });
        racesToggle.RegisterCallback<ChangeEvent<bool>>((e) => { racesFoldout.SetEnabled(e.newValue); });


    }

    private void Start()
    {
        
             
        PopulateData();
        mainPanel.hierarchy.Add(systemDisplay);
       
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
        newSystem.systemName = systemNameField.value;
        newSystem.useLevels = levelsToggle.value;
        newSystem.useClasses = classesToggle.value;
        newSystem.useRaces = racesToggle.value;
        //newSystem.useCoreStats = coreStatsToggle.value;
        newSystem.useAttributes = attributesToggle.value;

        
    }

    public void PopulateData()
    {
        dataPopulater.PopulateClasses(newSystem, elementSlotUI, classFoldout);
        dataPopulater.PopulateRaces(newSystem, elementSlotUI, racesFoldout);
        dataPopulater.PopulateAttributes(newSystem, elementSlotUI, attFoldout);
    }

}
