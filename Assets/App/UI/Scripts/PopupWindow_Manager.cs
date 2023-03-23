using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PopupWindow_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager UIManager;
    private UIDocument popupWindow;
    private Button cancelButton;
    public Button confirmButton;
    private Label promptText;
    private TextField optionalText;


    private void Awake()
    {
        popupWindow = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = popupWindow.rootVisualElement;
        if (root == null)
        {
            Debug.Log("No root element found");
        }

        cancelButton = root.Q<Button>("popup-button-cancel");
        confirmButton = root.Q<Button>("popup-button-confirm");
        promptText = root.Q<Label>("popup-label-text");
        optionalText = root.Q<TextField>("popup-textfield-optional");

        cancelButton.clickable.clicked += () => Cancel();
        optionalText.visible = false;       

    }

    public void Cancel()
    {
        popupWindow.sortingOrder = 0;
        promptText.text = string.Empty;
       
    }

    public void SetPromptText(string newText)
    {
        promptText.text = newText;
    }

    public void RenderTextField(string label, string value)
    {
        optionalText.visible = true;
        optionalText.label = label;
        optionalText.value = value;
    }
       
}
