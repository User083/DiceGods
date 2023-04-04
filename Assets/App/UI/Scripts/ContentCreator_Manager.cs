using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ContentCreator_Manager : MonoBehaviour
{
    public enum Tab { CHARACTER, ITEM}
    public Tab currentTab;
    [SerializeField] private UIDocument self;
    [SerializeField] private UIDocument popupWindow;
    [SerializeField] private VisualTreeAsset elementSlot;
    [SerializeField] private VisualTreeAsset charDisplayDoc;
    [SerializeField] private VisualTreeAsset itemDisplayDoc;
    private VisualElement ccDisplayPanel;
    private VisualElement characterDisplay;
    private CharacterDisplay character;
    private VisualElement itemDisplay;
    private ItemDisplay item;


    [Header("Self")]
    private Button returnButton;
    private Button resetButton;
    private Button createButton;
    private Button characterButton;
    private Button itemButton;
    private SaveData activeSave;


    [Header("Popup")]
    private PopupWindow_Manager popupWindowManager;
    private Button confirmButton;
    private TextField optionalTextField;

    [Header("Creatables")]
    private Character currentCharacter;
    private string currentName;
    private string currentDescription;
    private Item currentItem;
    private void Awake()
    {
        activeSave = new SaveData("Temporary");
        popupWindowManager = popupWindow.GetComponent<PopupWindow_Manager>();
        characterDisplay = charDisplayDoc.Instantiate();
        characterDisplay.style.flexGrow = 1;
        itemDisplay = itemDisplayDoc.Instantiate();
        itemDisplay.style.flexGrow = 1;
        

        var root = self.rootVisualElement;
        var popupRoot = popupWindow.rootVisualElement;

        if (root == null)
        {
            Debug.Log("No root element found");
        }

        //Popup
        confirmButton = popupRoot.Q<Button>("popup-button-confirm");
        optionalTextField = popupRoot.Q<TextField>("popup-textfield-optional");


        //Self
        returnButton = root.Q<Button>("cc-button-return");
        resetButton = root.Q<Button>("cc-button-reset");
        createButton = root.Q<Button>("cc-button-create");
        characterButton = root.Q<Button>("cc-button-character");
        itemButton = root.Q<Button>("cc-button-item");
        ccDisplayPanel = root.Q<VisualElement>("cc-panel-display");
        

        resetButton.clickable.clicked += () => ConfirmReset();
        createButton.clickable.clicked += () => ConfirmCreate();
        returnButton.clickable.clicked += () => ConfirmReturn();
        characterButton.clickable.clicked += () => CharacterCreator();
        itemButton.clickable.clicked += () => ItemCreator();

    }

    private void OnEnable()
    {
        activeSave = DataPersistenceManager.instance.activeSave;
        character = new CharacterDisplay(characterDisplay, elementSlot);
        item = new ItemDisplay(itemDisplay, elementSlot);
    }

    private void Start()
    {
        character.SetDisplayData(activeSave.parentSystem, true);
        CharacterCreator();
    }

    public void BackToTavern()
    {
        SceneManager.LoadSceneAsync("DG_Tavern");
    }

    public void ConfirmReturn()
    {
        confirmButton.clickable.clicked += () => BackToTavern();
        popupWindowManager.SetPromptText("Are you sure you want to return to the Tavern? Current changes won't be saved.");
        popupWindow.sortingOrder = 3;
    }

    public void ConfirmReset()
    {
        popupWindowManager.SetPromptText("Are you sure you want to reset all fields?");
        popupWindow.sortingOrder = 3;
        confirmButton.clickable.clicked += () => ResetFields();
    }

    public void ResetFields()
    {
        character.ResetFields();
        popupWindowManager.Cancel();
    }

    public void ConfirmCreate()
    {
        if(currentTab == Tab.CHARACTER)
        {
            SetCharacterValues();

            if (currentCharacter != null && currentCharacter._ID != string.Empty)
            {
                activeSave.characterList.Add(currentCharacter);
                DataPersistenceManager.instance.Save(activeSave, activeSave.saveID);
                currentCharacter = null;
                character.ResetFields();
                currentName = string.Empty;
                currentDescription = string.Empty;
            }
            else
            {
                popupWindowManager.SingleButtonPrompt("Please fill in the required fields.");
                popupWindow.sortingOrder = 3;
                confirmButton.clickable.clicked += () => popupWindowManager.Cancel();
            }
        }

        if (currentTab == Tab.ITEM)
        {
            if (currentItem != null && currentItem._ID != string.Empty)
            {
                activeSave.itemList.Add(currentItem);
                DataPersistenceManager.instance.Save(activeSave, activeSave.saveID);
                currentItem = null;
                currentName = string.Empty;
                currentDescription = string.Empty;
            }
            else
            {
                popupWindowManager.SingleButtonPrompt("Please fill in the required fields.");
                popupWindow.sortingOrder = 3;
                confirmButton.clickable.clicked += () => popupWindowManager.Cancel();
            }
        }

    }

    private void CharacterCreator()
    {
        currentName= string.Empty;
        currentDescription= string.Empty;
        currentTab = Tab.CHARACTER;
        RemoveExistingPanel();
        ccDisplayPanel.Add(characterDisplay);
    }

    private void ItemCreator()
    {
        currentName = string.Empty;
        currentDescription = string.Empty;
        RemoveExistingPanel();
        currentTab = Tab.ITEM;
        ccDisplayPanel.Add(itemDisplay);
    }

    private void RemoveExistingPanel()
    {
        if(ccDisplayPanel.Contains(characterDisplay))
        {
            ccDisplayPanel.Remove(characterDisplay);
        }

        if(ccDisplayPanel.Contains(itemDisplay))
        {
            ccDisplayPanel.Remove(itemDisplay);
        }
    }

    private void SetCharacterValues()
    {
        currentName = character.Name.value;
        currentDescription = character.Description.value;
        if(currentName != string.Empty)
        {
            currentCharacter = new Character(activeSave.parentSystem, currentName, currentDescription);
        }
       

    }
}
