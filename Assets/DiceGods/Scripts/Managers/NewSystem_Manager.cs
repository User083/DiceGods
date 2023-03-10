using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NewSystem_Manager : MonoBehaviour
{

    [SerializeField] private UIDocument newSystemUI;
    private Button createButton;
    private Button resetButton;
    private Button mainMenu;
    private TextField systemNameField;

    private void OnEnable()
    {
        var root = newSystemUI.rootVisualElement;
        if (root == null)
        {
            Debug.Log("No root element found");
        }

        createButton = root.Q<Button>("ns-button-create");
        resetButton = root.Q<Button>("ns-button-reset");
        mainMenu = root.Q<Button>("ns-button-mm");
        systemNameField = root.Q<TextField>("ns-textfield-systemname");
        mainMenu.clickable.clicked += () => OnMMClicked();
        resetButton.clickable.clicked += () => resetSystemFields();
        createButton.clickable.clicked += () => CreateNewSystem();

    }

    public void OnMMClicked()
    {     
        SceneManager.LoadSceneAsync("DG_MainMenu");
    }

    public void CreateNewSystem()
    {

    }

    private void resetSystemFields()
    {

    }


}
