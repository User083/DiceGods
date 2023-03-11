using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField] private UIDocument mainMenu;
    [SerializeField] private UIDocument loadMenu;
    [Header("Main Menu Elements")]
    private Button newSystem;
    private Button loadSystem;
    private Button quit;

    [Header("Load Menu Elements")]
    private Button loadSelectedSystem;
    private Button mmButton;
    private Button deleteButton;
    public string selectedSave = string.Empty;
 


    private void OnEnable()
    {
        
        var mm_root = mainMenu.rootVisualElement;
        var lm_root = loadMenu.rootVisualElement;
        if (mm_root == null || lm_root == null)
        {
            Debug.Log("No root element found");
        }

        //main menu screen
        newSystem = mm_root.Q<Button>("mm-button-new");
        loadSystem = mm_root.Q<Button>("mm-button-load");
        quit = mm_root.Q<Button>("mm-button-quit");
        newSystem.clickable.clicked += () => OnNewSystemClicked();
        loadSystem.clickable.clicked += () => disableMainMenu();
        quit.clickable.clicked += () => OnQuitClicked();

        //Load screen
        loadSelectedSystem = lm_root.Q<Button>("lm-button-load");
        mmButton = lm_root.Q<Button>("lm-button-return");
        deleteButton = lm_root.Q<Button>("lm-button-delete");
        mmButton.clickable.clicked += () => enableMainMenu();
        loadSelectedSystem.clickable.clicked += () => OnLoadSystemClicked();
        deleteButton.clickable.clicked += () => DeleteSave(selectedSave);
        deleteButton.SetEnabled(false);

    }

    private void Start()
    {
        DataSpecificDisable();
   
    }

    private void DataSpecificDisable()
    {
        if (!DataPersistenceManager.instance.HasSaveData())
        {
            loadSystem.SetEnabled(false);
        }
    }

    public void OnNewSystemClicked()
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.NewSave();
        SceneManager.LoadSceneAsync("DG_SystemCreator");
    }

    public void OnLoadSystemClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("DG_Tavern");

    }

    private void DeleteSave(string saveID)
    {
        DataPersistenceManager.instance.DeleteSaveData(saveID);
    }

      public void OnQuitClicked()
    {
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        newSystem.SetEnabled(false);
        loadSystem.SetEnabled(false);
        loadSelectedSystem.SetEnabled(false);
        mmButton.SetEnabled(false);
        quit.SetEnabled(false);
    }

    public void enableMainMenu()
    {
        mainMenu.sortingOrder = 1;
        loadMenu.sortingOrder = 0;
        DataSpecificDisable();
    }

    public void disableMainMenu()
    {
        mainMenu.sortingOrder = 0;
        loadMenu.sortingOrder = 1;
        DataSpecificDisable();
    }


}
