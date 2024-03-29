using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Library_Manager : MonoBehaviour
{
    [SerializeField] private UIDocument libraryUI;
    [SerializeField] private VisualTreeAsset systemDisplayUI;
    [SerializeField] private VisualTreeAsset elementSlotUI;
    [SerializeField] private VisualTreeAsset charDisplayDoc;
    [SerializeField] private VisualTreeAsset itemDisplayDoc;
    [SerializeField] private VisualTreeAsset singleSlotUI;

    [Header("Main")]
    private DataPopulater dataPopulater = new DataPopulater();
    private VisualElement systemDisplay;
    private VisualElement characterDisplay;
    private Button tavernButton;
    private Button tabButtonSystem;
    private Button tabButtonCharacters;
    private Button tabButtonItems;
    private Label currentTabLabel;
    private ListView characterListView;
    private ListView itemListView;
    private VisualElement currentTab;
    private SystemData currentSystemData;
    private CharacterDisplay character;
    private VisualElement itemDisplay;
    private ItemDisplay item;
    private SystemDisplay system;
    private SaveData activeSave;

    [Header("System UI Properties")]
    private TextField systemName;
    private Toggle levelsToggle;
    private Toggle classesToggle;
    private Toggle racesToggle;
    private Toggle coreStatsToggle;
    private Toggle attributesToggle;
    private Toggle valueToggle;
    private Toggle weightToggle;


    private void Awake()
    {
        activeSave = DataPersistenceManager.instance.activeSave;
        currentSystemData = new SystemData("Default");
        systemDisplay = systemDisplayUI.Instantiate();
        characterDisplay = charDisplayDoc.Instantiate();
        characterDisplay.style.maxWidth = Length.Percent(60);
        characterDisplay.style.minWidth = Length.Percent(40);
        itemDisplay = itemDisplayDoc.Instantiate();
        itemDisplay.style.maxWidth = Length.Percent(60);
        itemDisplay.style.minWidth = Length.Percent(40);
        dataPopulater.EnumerateCharacters(activeSave);
        dataPopulater.EnumerateItems(activeSave);
        characterListView = dataPopulater.PopulateCharacters(singleSlotUI);
        characterListView.style.flexGrow = 1;
        itemListView = dataPopulater.PopulateItems(singleSlotUI);
        itemListView.style.flexGrow = 1;
        systemDisplay.style.flexGrow = 1;
       
        
    }
    private void OnEnable()
    {

        character = new CharacterDisplay(characterDisplay, elementSlotUI);
        item = new ItemDisplay(itemDisplay, elementSlotUI);
        system = new SystemDisplay(systemDisplay, elementSlotUI);
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
        characterListView.onSelectionChange += OnCharSelected;
        itemListView.onSelectionChange += OnItemSelected;
    }

    private void Start()
    {
        initLibrary();
        systemTab();
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
        currentTab.Add(characterListView);
        currentTab.Add(characterDisplay);
    }

    private void itemsTab()
    {
        currentTabLabel.text = "Items";
        clearTab();
        currentTab.Add(itemListView);
        currentTab.Add(itemDisplay);
    }

    private void clearTab()
    {
        if (currentTab.Contains(systemDisplay))
        {
            currentTab.Remove(systemDisplay);
        }

        if (currentTab.Contains(characterDisplay))
        {
            currentTab.Remove(characterListView);
            currentTab.Remove(characterDisplay);
        }

        if (currentTab.Contains(itemDisplay))
        {
            currentTab.Remove(itemListView);
            currentTab.Remove(itemDisplay);
        }
    }

    private void OnCharSelected(IEnumerable<object> selectedItems)
    {
        var selectedChar = characterListView.selectedItem as Character;
        if(selectedChar == null)
        {
            character.ResetFields();
            return;
        }

       character.DisplayCharacter(selectedChar);

    }

    private void OnItemSelected(IEnumerable<object> selectedItems)
    {
        var selectedItem = itemListView.selectedItem as Item;
        if (selectedItem == null)
        {
            item.ResetFields();
            return;
        }

        item.DisplayItem(selectedItem);
    }

    private void initLibrary()
    {
        currentSystemData = DataPersistenceManager.instance.activeSave.parentSystem;
        character.SetDisplayData(currentSystemData, false);
        item.SetDisplayData(currentSystemData, false);
        system.SetDisplayData(currentSystemData);
        system.PopulateSystemData(currentSystemData);
    }
}
