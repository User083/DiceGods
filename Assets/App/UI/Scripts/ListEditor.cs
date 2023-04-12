using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;

public class ListEditor
{
    public ListView activeList;
    public Button removeButton;
    public Button newButton;
    public Button saveButton;
    public VisualElement root;
    public VisualElement listPanel;

    public ListEditor(VisualElement root)
    {
        this.root = root;
        removeButton = root.Q<Button>("ed-button-remove");
        newButton = root.Q<Button>("ed-button-new");
        saveButton = root.Q<Button>("ed-button-save");
        listPanel = root.Q<VisualElement>("ed-panel-list");
        removeButton.SetEnabled(false);
        saveButton.SetEnabled(false);
    }

    public void AddList(ListView list)
    {
        activeList = list;
        listPanel.Add(list);
    }

    public void RemoveList()
    {
        listPanel.Remove(activeList);
    }

    public void SetButtonStatus(bool remove, bool add, bool save)
    {
        removeButton.SetEnabled(remove);
        newButton.SetEnabled(add);
        saveButton.SetEnabled(save);
    }
}
