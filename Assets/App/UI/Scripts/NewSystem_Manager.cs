using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[System.Serializable]

public class NewSystem_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager UIManager;
    [SerializeField] private UIDocument newSystemUI;
    [SerializeField] private VisualTreeAsset elementSlot;
   

    private SaveData newSystemSaveData;   
    private Button createButton;
    private Button resetButton;
    private Button mainMenu;
    private Button confirmButton;

    [Header("Core System Details")]
    public SystemData newSystem = new SystemData("New System");
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
    //Foldouts
    private Foldout attFoldout;
    private Foldout classFoldout;
    private Foldout racesFoldout;



    private void OnEnable()
    {
        var root = newSystemUI.rootVisualElement;
        var popupRoot = UIManager.popupWindow.rootVisualElement;

        if (root == null)
        {
            Debug.Log("No root element found");
        }

        //Popup
        confirmButton = popupRoot.Q<Button>("popup-button-confirm");
        saveNameField = popupRoot.Q<TextField>("popup-textfield-optional");
        
        //Toggles
        levelsToggle = root.Q<Toggle>("ns-toggle-levels");
        classesToggle = root.Q<Toggle>("ns-toggle-classes");
        racesToggle = root.Q<Toggle>("ns-toggle-races");
        //coreStatsToggle = root.Q<Toggle>("ns-toggle-core-stats");
        attributesToggle = root.Q<Toggle>("ns-toggle-attributes");

        //Foldouts
        attFoldout = root.Q<Foldout>("ns-foldout-att");
        racesFoldout = root.Q<Foldout>("ns-foldout-races");
        classFoldout = root.Q<Foldout>("ns-foldout-classes");

        //buttons
        createButton = root.Q<Button>("ns-button-create");
        resetButton = root.Q<Button>("ns-button-reset");
        mainMenu = root.Q<Button>("ns-button-mm");
        systemNameField = root.Q<TextField>("ns-textfield-systemname");

        //Button bindings
        mainMenu.clickable.clicked += () => ConfirmCancel();
        resetButton.clickable.clicked += () => ConfirmReset();
        createButton.clickable.clicked += () => ConfirmNewSystem();

        saveNameField.RegisterCallback<ChangeEvent<string>>((e) => { saveName = e.newValue; });
        attributesToggle.RegisterCallback<ChangeEvent<bool>>((e) => { attFoldout.SetEnabled(e.newValue); });
        classesToggle.RegisterCallback<ChangeEvent<bool>>((e) => { classFoldout.SetEnabled(e.newValue); });
        racesToggle.RegisterCallback<ChangeEvent<bool>>((e) => { racesFoldout.SetEnabled(e.newValue); });


    }

    private void Start()
    {
        systemNameField.RegisterCallback<ChangeEvent<string>>((e) => { newSystem.systemName = e.newValue; });

        PopulateAttributes();
        PopulateClasses();
        PopulateRaces(); 
        attFoldout.SetEnabled(false);
        racesFoldout.SetEnabled(false);
        classFoldout.SetEnabled(false);
    }



    //User interface handling

    public void OnMMClicked()
    {
        UIManager.mainMenuManager.enableMainMenu();
        
    }

    public void CreateNewSystem()
    {
        newSystemSaveData = DataPersistenceManager.instance.initialiseNewSave(saveName);
        newSystemSaveData.parentSystem = newSystem;
        SetSystemValues();
        DataPersistenceManager.instance.Save(newSystemSaveData, newSystemSaveData.saveID);
        
        UIManager.popupWindowManager.Cancel();
        SceneManager.LoadSceneAsync("DG_Tavern");

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
        newSystem.useLevels = levelsToggle.value;
        newSystem.useClasses = classesToggle.value;
        newSystem.useRaces = racesToggle.value;
        //newSystem.useCoreStats = coreStatsToggle.value;
        newSystem.useAttributes = attributesToggle.value;
        
    }

    void PopulateAttributes()
    {
        int attI = 0;
        List<Attribute> attributes = new List<Attribute>();
        attributes = newSystem.attributes;

        foreach (var attribute in attributes)
        {
            var newSlotEntry = elementSlot.Instantiate();
            var newEntryLogic = new ElementSlot();
            newSlotEntry.userData = newEntryLogic;
            newEntryLogic.SetVisualElement(newSlotEntry);
            newEntryLogic.SetAttributeData(attribute);

            attFoldout.Insert(attI, newSlotEntry);
            attI++;
        };

    }

    void PopulateRaces()
    {
        int raceI = 0;
        List<Race> races = new List<Race>();
        races = newSystem.races;

        foreach (var race in races)
        {
            var newSlotEntry = elementSlot.Instantiate();
            var newEntryLogic = new ElementSlot();
            newSlotEntry.userData = newEntryLogic;
            newEntryLogic.SetVisualElement(newSlotEntry);
            newEntryLogic.SetRaceData(race);

            racesFoldout.Insert(raceI, newSlotEntry);
            raceI++;
        };
    }

    void PopulateClasses()
    {
        int classI = 0;
        List<CharacterClass> classes = new List<CharacterClass>();
        classes = newSystem.characterClasses;

        foreach (var charClass in classes)
        {
            var newSlotEntry = elementSlot.Instantiate();
            var newEntryLogic = new ElementSlot();
            newSlotEntry.userData = newEntryLogic;
            newEntryLogic.SetVisualElement(newSlotEntry);
            newEntryLogic.SetCharClassData(charClass);

            classFoldout.Insert(classI, newSlotEntry);
            classI++;
        };
    }



    //Data handling
    //void IDataPersistence.LoadData(SaveData data)
    //{
    //    this.newSystem.systemName = data.parentSystem.systemName;
    //    this.useLevels = data.parentSystem.useLevels;
    //    this.useClasses = data.parentSystem.useClasses;
    //    this.characterClasses = data.parentSystem.characterClasses;
    //    this.useRaces = data.parentSystem.useRaces;
    //    this.races = data.parentSystem.races;
    //    this.useCoreStats = data.parentSystem.useCoreStats;
    //    this.coreStats = data.parentSystem.coreStats;
    //    this.useAttributes = data.parentSystem.useAttributes;
    //    this.atrributes = data.parentSystem.atrributes;
    //}

    //void IDataPersistence.SaveData(SaveData data)
    //{
    //    data.parentSystem.systemName = this.systemName;
    //    data.parentSystem.useLevels = this.useLevels;
    //    data.parentSystem.useClasses = this.useClasses;
    //    data.parentSystem.characterClasses = this.characterClasses;
    //    data.parentSystem.useRaces = this.useRaces;
    //    data.parentSystem.races = this.races;
    //    data.parentSystem.useCoreStats = this.useCoreStats;
    //    data.parentSystem.coreStats = this.coreStats;
    //    data.parentSystem.useAttributes = this.useAttributes;
    //    data.parentSystem.atrributes = this.atrributes;
    //}

}
