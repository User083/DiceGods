using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class scr_UIManager : MonoBehaviour, IDataPersistance
{
    public UIDocument MainMenuScreen;
    public UIDocument TavernScreen;
    public UIDocument LibraryScreen;
    public GameObject Managers;
    private scr_SystemManager SystemManager;

    private Array allSystems;
    public List<SO_Systems> systems;
    public List<string> systemNames;

    private void Awake()
    {
      SystemManager = Managers.GetComponent<scr_SystemManager>();
      systems = Resources.FindObjectsOfTypeAll<SO_Systems>().ToList<SO_Systems>();
       
    }
    private void OnEnable()
    {
        // DataPersistenceManager.instance.LoadSystem();
        systemNames = new List<string>();


        foreach (var system in systems)
        {
            systemNames.Add(system.systemName);
        }


    }

    private void Start()
    {
        
        MainMenu();
        TavernScreen.sortingOrder = 0;
        LibraryScreen.sortingOrder = 0;

        Debug.Log(systems.Count);
    }

    //activate MainMenu UI
    private void MainMenu()
    {
        MainMenuScreen.sortingOrder = 1;
        var root = MainMenuScreen.rootVisualElement;
        var enterButton = root.Q<Button>("menu-button-enter");
        var quitButton = root.Q<Button>("menu-button-quit");
        var systemSelector = root.Q<DropdownField>("menu-system-select");

        systemSelector.choices = systemNames;

        if (enterButton !=null)
        {
            enterButton.clickable.clicked += () =>
            {
                MainMenuScreen.sortingOrder = 0;
                Tavern();
            };
        }

        if (quitButton != null)
        {
            quitButton.clickable.clicked += () =>
            {
                Application.Quit();
            };
        }
    }

    //activate Tavern UI
    private void Tavern()
    {
        TavernScreen.sortingOrder = 1;
        var root = TavernScreen.rootVisualElement;
        var libraryButton = root.Q<Button>("tavern-button-library");
        var menuButton = root.Q<Button>("tavern-button-return");

        if (libraryButton != null)
        {
            libraryButton.clickable.clicked += () =>
            {
                TavernScreen.sortingOrder = 0;
                Library();
            };
        }

        if (menuButton != null)
        {
            menuButton.clickable.clicked += () =>
            {
                TavernScreen.sortingOrder = 0;
                MainMenu();
            };
        }
    }

    //activate Library UI
    private void Library()
    {
        LibraryScreen.sortingOrder = 1;
        var root = LibraryScreen.rootVisualElement;
        //library buttons
        var returnButton = root.Q<Button>("library-button-return");
        var systemButton = root.Q<Button>("library-button-system");
        var characterButton = root.Q<Button>("library-button-characters");
        var itemButton = root.Q<Button>("library-button-items");

        //test label
        var testLabel = root.Q<Label>("library-tab-text-test");

        //return to tavern
        if (returnButton != null)
        {
            returnButton.clickable.clicked += () =>
            {
                LibraryScreen.sortingOrder = 0;
                Tavern();
            };
        }

        //switch to system

        if (systemButton != null)
        {
            systemButton.clickable.clicked += () =>
            {
                testLabel.text = "System";
            };
        }

        //switch to characters

        if (characterButton != null)
        {
            characterButton.clickable.clicked += () =>
            {
                testLabel.text = "Characters";
            };
        }

        //switch to items

        if (itemButton != null)
        {
            itemButton.clickable.clicked += () =>
            {
                testLabel.text = "Items";
            };
        }


    }

    public void LoadData(SaveData data)
    {
        systems = data.savedSystems;
        systemNames = data.systemNames;
    }

    public void SaveData(ref SaveData data)
    {
        data.savedSystems = systems;
    }

    private void OnApplicationQuit()
    {
       // DataPersistenceManager.instance.SaveSystem();
    }
}
