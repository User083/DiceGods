using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Library_Manager : MonoBehaviour
{
    [SerializeField] private UIDocument libraryUI;
    [SerializeField] private VisualTreeAsset systemDisplayUI;
    [SerializeField] private VisualTreeAsset elementSlotUI;
    [SerializeField] private VisualTreeAsset charDisplayDoc;

    private DataPopulater dataPopulater = new DataPopulater();
    private VisualElement systemDisplay;
    private VisualElement characterDisplay;
    private Button tavernButton;
    private Button tabButtonSystem;
    private Button tabButtonCharacters;
    private Button tabButtonItems;
    private Label currentTabLabel;
    private VisualElement currentTab;
    private SystemData currentSystemData;
    private CharacterDisplay character;

    [Header("System UI Properties")]
    private TextField systemName;
    private Toggle levelsToggle;
    private Toggle classesToggle;
    private Toggle racesToggle;
    private Toggle coreStatsToggle;
    private Toggle attributesToggle;


    private void Awake()
    {
        currentSystemData = new SystemData("Default");
        systemDisplay = systemDisplayUI.Instantiate();
        characterDisplay = charDisplayDoc.Instantiate();
        systemDisplay.style.flexGrow = 1;
        characterDisplay.style.flexGrow = 1;
    }
    private void OnEnable()
    {

        character = new CharacterDisplay(characterDisplay, elementSlotUI);
        var root = libraryUI.rootVisualElement;
        
        if (root == null)
        {
            Debug.Log("No root element found");
        }

        //Tab
        currentTab = root.Q<VisualElement>("l-panel-main");
        //Labels
        currentTabLabel = root.Q<Label>("l-label-current-tab");

        //Toggles
        levelsToggle = systemDisplay.Q<Toggle>("ns-toggle-levels");
        classesToggle = systemDisplay.Q<Toggle>("ns-toggle-classes");
        racesToggle = systemDisplay.Q<Toggle>("ns-toggle-races");
        coreStatsToggle = root.Q<Toggle>("l-toggle-core-stats");
        attributesToggle = systemDisplay.Q<Toggle>("ns-toggle-attributes");

        //Buttons
        tavernButton = root.Q<Button>("l-button-tavern");
        tabButtonSystem = root.Q<Button>("l-button-system");
        tabButtonCharacters= root.Q<Button>("l-button-characters");
        tabButtonItems = root.Q<Button>("l-button-items");
        systemName = systemDisplay.Q<TextField>("ns-textfield-systemname");
        tavernButton.clickable.clicked += () => OnTavernClicked();
        tabButtonSystem.clickable.clicked += () => systemTab();
        tabButtonCharacters.clickable.clicked += () => charactersTab();
        tabButtonItems.clickable.clicked += () => itemsTab();

        

        
    }

    private void Start()
    {
        initLibrary();
    }

    public void OnTavernClicked()
    {
        SceneManager.LoadSceneAsync("DG_Tavern");
    }

    private void systemTab()
    {
        currentTabLabel.text = "System";
        clearTab();
        currentTab.Add(systemDisplay);
        
    }

    private void charactersTab()
    {
        currentTabLabel.text = "Characters";
        clearTab();
        currentTab.Add(characterDisplay);
    }

    private void itemsTab()
    {
        currentTabLabel.text = "Items";
        clearTab();
       
    }

    private void clearTab()
    {
        if (currentTab.Contains(systemDisplay))
        {
            currentTab.Remove(systemDisplay);
        }

        if (currentTab.Contains(characterDisplay))
        {
            currentTab.Remove(characterDisplay);
        }

    }

    private void initLibrary()
    {
        currentSystemData = DataPersistenceManager.instance.activeSave.parentSystem;
        character.SetDisplayData(currentSystemData, true);

        levelsToggle.value = currentSystemData.useLevels;
        racesToggle.value = currentSystemData.useRaces;
        classesToggle.value = currentSystemData.useClasses;
        attributesToggle.value = currentSystemData.useAttributes;
        systemName.value = currentSystemData.systemName;
        systemName.SetEnabled(false);
        levelsToggle.SetEnabled(false);
        racesToggle.SetEnabled(false);
        classesToggle.SetEnabled(false);
        attributesToggle.SetEnabled(false);
        dataPopulater.PopulateAttributes(currentSystemData, elementSlotUI, systemDisplay.Q<Foldout>("ns-foldout-att"));
        dataPopulater.PopulateRaces(currentSystemData, elementSlotUI, systemDisplay.Q<Foldout>("ns-foldout-races"));
        dataPopulater.PopulateClasses(currentSystemData, elementSlotUI, systemDisplay.Q<Foldout>("ns-foldout-classes"));
        if(!currentSystemData.useAttributes)
        {
            systemDisplay.Q<Foldout>("ns-foldout-att").SetEnabled(false);
        }

        if (!currentSystemData.useRaces)
        {
            systemDisplay.Q<Foldout>("ns-foldout-races").SetEnabled(false);
        }

        if (!currentSystemData.useClasses)
        {
            systemDisplay.Q<Foldout>("ns-foldout-classes").SetEnabled(false);
        }

    }
}
