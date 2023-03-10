using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Tavern_Manager : MonoBehaviour
{
    [SerializeField] private UIDocument tavernUI;
    private Button libraryButton; 
    private Button mainMenu;
    private Button creatorButton;
    private Button encounterButton;

    private void OnEnable()
    {
        var root = tavernUI.rootVisualElement;
        if (root == null)
        {
            Debug.Log("No root element found");
        }

        libraryButton = root.Q<Button>("t-button-library");  
        mainMenu = root.Q<Button>("t-button-return");
        creatorButton = root.Q<Button>("t-button-creator");
        encounterButton = root.Q<Button>("t-button-encounter");
        libraryButton.clickable.clicked += () => OnLibraryClicked();
        mainMenu.clickable.clicked += () => OnMMClicked();
        creatorButton.SetEnabled(false);
        encounterButton.SetEnabled(false);
    }

    public void OnMMClicked()
    {   
        SceneManager.LoadSceneAsync("DG_MainMenu");
    }
    public void OnLibraryClicked()
    {
        SceneManager.LoadSceneAsync("DG_Library");
    }

    
}
