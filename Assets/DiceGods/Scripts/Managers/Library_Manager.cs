using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Library_Manager : MonoBehaviour
{
    [SerializeField] private UIDocument libraryUI;
    private Button tavernButton;
    private Button tabButtonSystem;
    private Button tabButtonCharacters;
    private Button tabButtonItems;
    private Label currentTabLabel;

    private void OnEnable()
    {
        var root = libraryUI.rootVisualElement;
        if (root == null)
        {
            Debug.Log("No root element found");
        }

        currentTabLabel = root.Q<Label>("l-label-current-tab");
        tavernButton = root.Q<Button>("l-button-tavern");
        tabButtonSystem = root.Q<Button>("l-button-system");
        tabButtonCharacters= root.Q<Button>("l-button-characters");
        tabButtonItems = root.Q<Button>("l-button-items");
        tavernButton.clickable.clicked += () => OnTavernClicked();
        tabButtonSystem.clickable.clicked += () => systemTab();
        tabButtonCharacters.clickable.clicked += () => charactersTab();
        tabButtonItems.clickable.clicked += () => itemsTab();

    }

    public void OnTavernClicked()
    {
        SceneManager.LoadSceneAsync("DG_Tavern");
    }

    private void systemTab()
    {
        currentTabLabel.text = "System";
    }

    private void charactersTab()
    {
        currentTabLabel.text = "Characters";
    }

    private void itemsTab()
    {
        currentTabLabel.text = "Items";
    }
}
