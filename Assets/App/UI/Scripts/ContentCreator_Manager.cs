using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ContentCreator_Manager : MonoBehaviour
{
    [SerializeField] private UIDocument self;
    [SerializeField] private UIDocument popupWindow;
    [SerializeField] private VisualTreeAsset elementSlot;
    [SerializeField] private VisualTreeAsset charDisplayDoc;
    private VisualElement ccDisplay;
    private VisualElement characterDisplay;
    private CharacterDisplay character;

    [Header("Self")]
    private Button returnButton;


    [Header("Popup")]
    private PopupWindow_Manager popupWindowManager;
    private Button confirmButton;
    private TextField optionalTextField;

    private void Awake()
    {
        popupWindowManager = popupWindow.GetComponent<PopupWindow_Manager>();
        characterDisplay = charDisplayDoc.Instantiate();
        characterDisplay.style.flexGrow = 1;
        
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
        ccDisplay = root.Q<VisualElement>("cc-panel-display");

        returnButton.clickable.clicked += () => ConfirmReturn();
    }

    private void OnEnable()
    {
        character = new CharacterDisplay(characterDisplay, elementSlot);
    }

    private void Start()
    {
        character.SetDisplayData(DataPersistenceManager.instance.activeSave.parentSystem, true);
        ccDisplay.Add(characterDisplay);
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
}
