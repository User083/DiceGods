using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Library_Manager : MonoBehaviour
{
    [SerializeField] private UIDocument libraryUI;
    private Button tavernButton;
    private Button tabButtonSystem;
    private Button tabButtonCharacters;
    private Button tabButtonItems;
    private Label currentTabLabel;

    [Header("System UI Properties")]
    private Label systemName;
    private Toggle levelsToggle;
    private Toggle classesToggle;
    private Toggle racesToggle;
    private Toggle coreStatsToggle;
    private Toggle attributesToggle;

    private void OnEnable()
    {
        var root = libraryUI.rootVisualElement;
        if (root == null)
        {
            Debug.Log("No root element found");
        }
        
        
        //Labels
        currentTabLabel = root.Q<Label>("l-label-current-tab");

        //Toggles
        levelsToggle = root.Q<Toggle>("l-toggle-levels");
        classesToggle = root.Q<Toggle>("l-toggle-classes");
        racesToggle = root.Q<Toggle>("l-toggle-races");
        coreStatsToggle = root.Q<Toggle>("l-toggle-core-stats");
        attributesToggle = root.Q<Toggle>("l-toggle-attributes");

        //Buttons
        tavernButton = root.Q<Button>("l-button-tavern");
        tabButtonSystem = root.Q<Button>("l-button-system");
        tabButtonCharacters= root.Q<Button>("l-button-characters");
        tabButtonItems = root.Q<Button>("l-button-items");
        systemName = root.Q<Label>("l-label-system-name");
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
        
    }

    private void charactersTab()
    {
        currentTabLabel.text = "Characters";
    }

    private void itemsTab()
    {
        currentTabLabel.text = "Items";
    }

    private void initLibrary()
    {
        //System Data
        
   
        
    }
}
