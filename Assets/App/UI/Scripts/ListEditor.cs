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
    public Button cancelButton;
    public Button createButton;
    public VisualElement root;
    public VisualElement listPanel;
    public VisualElement editorRoot;

    public ListEditor(VisualElement root, VisualElement editorRoot)
    {
        this.root = root;
        removeButton = root.Q<Button>("ed-button-remove");
        newButton = root.Q<Button>("ed-button-new");
        listPanel = root.Q<VisualElement>("ed-panel-list");
        removeButton.SetEnabled(false);
        this.editorRoot = editorRoot;
        cancelButton = editorRoot.Q<Button>("ep-button-cancel");
        createButton = editorRoot.Q<Button>("ep-button-create");
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

    public void SetButtonStatus(bool remove, bool add)
    {
        removeButton.SetEnabled(remove);
        newButton.SetEnabled(add);

    }
}
