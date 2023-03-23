using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Manager : MonoBehaviour
{
    public UIDocument mainMenu;
    public UIDocument loadMenu;
    public UIDocument popupWindow;
    public UIDocument systemCreator;

    public SaveSlot_Manager saveManager;
    public PopupWindow_Manager popupWindowManager;
    public MainMenu_Manager mainMenuManager;

    private void Awake()
    {

        
    }
}
