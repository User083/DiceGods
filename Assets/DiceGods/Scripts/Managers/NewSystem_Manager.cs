using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewSystem_Manager : MonoBehaviour
{

    [SerializeField] private UIDocument newSystemUI;
    private Button initSystem;
    private Button resetSystem;
    private Button mainMenu;

    private void OnEnable()
    {
        var root = newSystemUI.rootVisualElement;
        if (root == null)
        {
            Debug.Log("No root element found");
        }

        initSystem = root.Q<Button>("ns-button-init");
        resetSystem = root.Q<Button>("ns-button-reset");
        mainMenu = root.Q<Button>("ns-button-return");


    }

    private void Start()
    {
     
    }

    
 
}
