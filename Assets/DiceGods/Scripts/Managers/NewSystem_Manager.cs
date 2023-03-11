using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[System.Serializable]

public class NewSystem_Manager : MonoBehaviour, IDataPersistence
{

    [SerializeField] private UIDocument newSystemUI;
    [SerializeField] private UIDocument popupWindow;
    
    private PopupWindow_Manager popupWindowManager;
    private Button createButton;
    private Button resetButton;
    private Button mainMenu;
    private Button confirmButton;

    [Header("Core System Details")]
    public string saveName;
    public string systemName;
    public bool useLevels;
    public bool useClasses;
    public List<CharacterClass> characterClasses;
    public bool useRaces;
    public List<Race> races;
    public bool useCoreStats;
    public List<CoreStat> coreStats;
    public bool useAttributes;
    public List<Attribute> atrributes;

    [Header("Core System Details UI Elements")]
    private TextField saveNameField;
    private TextField systemNameField;
    private Toggle levelsToggle;
    private Toggle classesToggle;
    private Toggle racesToggle;
    private Toggle coreStatsToggle;
    private Toggle attributesToggle;


    private void OnEnable()
    {
        popupWindowManager= popupWindow.GetComponent<PopupWindow_Manager>();
        var root = newSystemUI.rootVisualElement;
        var popupRoot = popupWindow.rootVisualElement;
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
        coreStatsToggle = root.Q<Toggle>("ns-toggle-core-stats");
        attributesToggle = root.Q<Toggle>("ns-toggle-attributes");

        //buttons
        createButton = root.Q<Button>("ns-button-create");
        resetButton = root.Q<Button>("ns-button-reset");
        mainMenu = root.Q<Button>("ns-button-mm");
        systemNameField = root.Q<TextField>("ns-textfield-systemname");
        mainMenu.clickable.clicked += () => ConfirmCancel();
        resetButton.clickable.clicked += () => ConfirmReset();
        createButton.clickable.clicked += () => ConfirmNewSystem();

        saveNameField.RegisterCallback<ChangeEvent<string>>((e) => { saveName = e.newValue; });
        systemNameField.RegisterCallback<ChangeEvent<string>>((e) => { systemName = e.newValue; });

    }

    //User interface handling

    public void OnMMClicked()
    {     
        SceneManager.LoadSceneAsync("DG_MainMenu");
    }

    public void CreateNewSystem()
    {
        DataPersistenceManager.instance.NewSave(saveName);
        SetSystemValues();
        DataPersistenceManager.instance.Save(saveName);
        popupWindowManager.Cancel();
       SceneManager.LoadSceneAsync("DG_Tavern");
    }

    private void resetSystemFields()
    {
        
        popupWindowManager.Cancel();
    }

    private void ConfirmReset()
    {
        
        confirmButton.clickable.clicked += () => resetSystemFields();
        popupWindowManager.SetPromptText("Are you sure you want reset all fields to default?");
        popupWindow.sortingOrder = 3;
    }

    private void ConfirmCancel()
    {
        confirmButton.clickable.clicked += () => OnMMClicked();
        popupWindowManager.SetPromptText("Are you sure you want to return to the main menu? Current changes won't be saved.");
        popupWindow.sortingOrder = 3;
    }

    private void ConfirmNewSystem()
    {
        confirmButton.clickable.clicked += () => CreateNewSystem();
        popupWindowManager.SetPromptText("What would you like to save this system as?");
        popupWindowManager.RenderTextField("Save name", "New System");
        popupWindow.sortingOrder = 3;
       
        
    }

    private void SetSystemValues()
    {
        useLevels = levelsToggle.value;
        useClasses = classesToggle.value;
        useRaces = racesToggle.value;
        useCoreStats = coreStatsToggle.value;
        useAttributes = attributesToggle.value;
    }


    //Data handling
    void IDataPersistence.LoadData(SaveData data)
    {
        this.systemName = data.systemName;
        this.useLevels = data.useLevels;
        this.useClasses = data.useClasses;
        this.characterClasses = data.characterClasses;
        this.useRaces = data.useRaces;
        this.races = data.races;
        this.useCoreStats = data.useCoreStats;
        this.coreStats = data.coreStats;
        this.useAttributes = data.useAttributes;
        this.atrributes = data.atrributes;
    }

    void IDataPersistence.SaveData(ref SaveData data)
    {
        data.systemName = this.systemName;
        data.useLevels = this.useLevels;
        data.useClasses = this.useClasses;
        data.characterClasses = this.characterClasses;
        data.useRaces = this.useRaces;
        data.races = this.races;
        data.useCoreStats = this.useCoreStats;
        data.coreStats = this.coreStats;
        data.useAttributes = this.useAttributes;
        data.atrributes = this.atrributes;
    }

}
