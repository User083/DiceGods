using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class scr_UIManager : MonoBehaviour
{
    public UIDocument MainMenuScreen;
    public UIDocument TavernScreen;
    public UIDocument LibraryScreen;

    private void OnEnable()
    {
        MainMenu();
        TavernScreen.sortingOrder = 0;
        LibraryScreen.sortingOrder = 0;
    
    }
    //activate MainMenu UI
    private void MainMenu()
    {
        MainMenuScreen.sortingOrder = 1;
        var root = MainMenuScreen.rootVisualElement;
        var enterButton = root.Q<Button>("menu-button-enter");
        var quitButton = root.Q<Button>("menu-button-quit");

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
        var returnButton = root.Q<Button>("library-button-return");

        if (returnButton != null)
        {
            returnButton.clickable.clicked += () =>
            {
                LibraryScreen.sortingOrder = 0;
                Tavern();
            };
        }
    }

}
