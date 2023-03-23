using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ContentCreator_Manager : MonoBehaviour
{
    [SerializeField] private UIDocument self;
    [SerializeField] private UIDocument popupWindow;

    [Header("Self")]
    private Button returnButton;


    [Header("Popup")]
    private PopupWindow_Manager popupWindowManager;
    private Button confirmButton;
    private TextField optionalTextField;

    private void Awake()
    {
        popupWindowManager = popupWindow.GetComponent<PopupWindow_Manager>();
        
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

        returnButton.clickable.clicked += () => ConfirmReturn();
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
