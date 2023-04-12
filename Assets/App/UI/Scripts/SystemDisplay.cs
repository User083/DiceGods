using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;

public class SystemDisplay 
{
    public VisualElement root;
    public DataPopulater populater = new DataPopulater();
    private VisualTreeAsset elementSlot;
    private SystemData blankSystem;
    public ListEditor editor;
    public VisualElement editorRoot;
    

    [Header("Core System Details UI Elements")]
    public TextField saveNameField;
    public TextField systemNameField;
    //Toggles
    public Toggle levelsToggle;
    public Toggle classesToggle;
    public Toggle racesToggle;
    public Toggle coreStatsToggle;
    public Toggle attributesToggle;
    public Toggle charsHaveValueToggle;
    public Toggle weightToggle;
    //SelectedItems
    public Attribute selectedAtt;
    public Race selectedRace;
    public CharacterClass selectedClass;
    public Stat selectedStat;
    //Listviews
    public ListView attList;
    public ListView raceList;
    public ListView classList;
    public ListView statList;
    //Buttons
    public Button attButton;
    public Button raceButton;
    public Button classButton;
    public Button statsButton;
    //Foldouts
    public Foldout attFoldout;
    public Foldout classFoldout;
    public Foldout racesFoldout;
    public Foldout statsFoldout;

    public SystemDisplay(VisualElement root, VisualTreeAsset attributeElementSlot)
    {
        this.root = root;
        elementSlot = attributeElementSlot;

        systemNameField = root.Q<TextField>("ns-textfield-systemname");
        //Toggles
        levelsToggle = root.Q<Toggle>("ns-toggle-levels");
        classesToggle = root.Q<Toggle>("ns-toggle-classes");
        racesToggle = root.Q<Toggle>("ns-toggle-races");
        coreStatsToggle = root.Q<Toggle>("ns-toggle-stats");
        attributesToggle = root.Q<Toggle>("ns-toggle-attributes");
        charsHaveValueToggle = root.Q<Toggle>("ns-toggle-value");
        weightToggle = root.Q<Toggle>("ns-toggle-weight");
        //Foldouts
        attFoldout = root.Q<Foldout>("ns-foldout-att");
        racesFoldout = root.Q<Foldout>("ns-foldout-races");
        classFoldout = root.Q<Foldout>("ns-foldout-classes");
        statsFoldout = root.Q<Foldout>("ns-foldout-stats");
        //Buttons
        attButton = root.Q<Button>("ns-button-attributes");
        raceButton = root.Q<Button>("ns-button-races");
        classButton = root.Q<Button>("ns-button-classes");
        statsButton = root.Q<Button>("ns-button-stats");
        
    }

    public void PopulateSystemData(SystemData currentSystemData)
    {
        levelsToggle.value = currentSystemData.useLevels;
        racesToggle.value = currentSystemData.useRaces;
        classesToggle.value = currentSystemData.useClasses;
        attributesToggle.value = currentSystemData.useAttributes;
        systemNameField.value = currentSystemData.systemName;
    }

    public void SetDisplayData(SystemData parentSystem, bool editable)
    {
        
        if (parentSystem == null)
        {
            Debug.LogError("No parent system found - characters can't exist outside a system. Have you loaded a save?");
        }

            systemNameField.isReadOnly= true;
            levelsToggle.SetEnabled(editable);
            classesToggle.SetEnabled(editable);
            racesToggle.SetEnabled(editable);
            coreStatsToggle.SetEnabled(editable);
            attributesToggle.SetEnabled(editable);
            charsHaveValueToggle.SetEnabled(editable);
            weightToggle.SetEnabled(editable);
        attButton.RemoveFromHierarchy();
        raceButton.RemoveFromHierarchy();
        classButton.RemoveFromHierarchy();
        statsButton.RemoveFromHierarchy();

        populater.PopulateAttributes(parentSystem, elementSlot, attFoldout);
        populater.PopulateRaces(parentSystem, elementSlot, racesFoldout);
        populater.PopulateClasses(parentSystem, elementSlot, classFoldout);

    }

    public void SetEditableSystem(SystemData parentSystem)
    {
        blankSystem = parentSystem;

        //Setup default fields
        attFoldout.RemoveFromHierarchy();
        racesFoldout.RemoveFromHierarchy();
        classFoldout.RemoveFromHierarchy();
        statsFoldout.RemoveFromHierarchy();
        attButton.SetEnabled(false);
        raceButton.SetEnabled(false);
        classButton.SetEnabled(false);
        statsButton.SetEnabled(false);

        //Populate system defaults for editing
        populater.EnumerateAttributes(parentSystem);
        attList = populater.PopulateAttributeList(elementSlot);
        populater.EnumerateRaces(parentSystem);
        raceList = populater.PopulateRaceList(elementSlot);
        populater.EnumerateClasses(parentSystem);
        classList = populater.PopulateClassList(elementSlot);
    }

    public void ResetFields()
    {
        levelsToggle.value = false;
        classesToggle.value = false;
        racesToggle.value = false;
        coreStatsToggle.value = false;
        attributesToggle.value = false;
        charsHaveValueToggle.value = false;
        weightToggle.value = false;

        populater.EnumerateAttributes(blankSystem);
        attList = populater.PopulateAttributeList(elementSlot);
        populater.EnumerateRaces(blankSystem);
        raceList =populater.PopulateRaceList(elementSlot);
        populater.EnumerateClasses(blankSystem);
        classList = populater.PopulateClassList(elementSlot);
    }

    //Handle list selection changes
    public void OnAttChange(IEnumerable<object> selectedItems)
    {
        var currentAtt = attList.selectedItem as Attribute;

        if (currentAtt == null)
        {
            selectedAtt = null;

            return;
        }

        selectedAtt = currentAtt;
        editor.SetButtonStatus(true, true, true);

    }

    public void OnRaceChange(IEnumerable<object> selectedItems)
    {
        var currentRace = raceList.selectedItem as Race;

        if (currentRace == null)
        {
            selectedRace = null;

            return;
        }

        selectedRace = currentRace;
        editor.SetButtonStatus(true, true, true);

    }

    public void OnClassChange(IEnumerable<object> selectedItems)
    {
        var currentClass = classList.selectedItem as CharacterClass;

        if (currentClass == null)
        {
            selectedClass = null;

            return;
        }

        selectedClass = currentClass;
        editor.SetButtonStatus(true, true, true);

    }

    //Setup list editor

    public void ListEditorSetup(VisualTreeAsset editorDoc)
    {
        editorRoot = editorDoc.Instantiate();
        editorRoot.style.flexGrow = 1;
        editor = new ListEditor(editorRoot);
        editor.removeButton.clickable.clicked += () => RemoveListItem();
        
    }

    //display relevant list
    public void DisplayAttributes(VisualElement panel)
    {
        ClearSelection();
        if (editor.activeList != null)
        {
            editor.RemoveList();
        }
        editor.AddList(attList);

        if (panel.Contains(editorRoot))
        {
            panel.Remove(editorRoot);
        }
        panel.Add(editorRoot);
        attList.onSelectionChange += OnAttChange;
    }

    public void DisplayClasses(VisualElement panel)
    {
        ClearSelection();
        if (editor.activeList != null)
        {
            editor.RemoveList();
        }
        editor.AddList(classList);

        if (panel.Contains(editorRoot))
        {
            panel.Remove(editorRoot);
        }
        panel.Add(editorRoot);
        classList.onSelectionChange += OnClassChange;
    }

    public void DisplayRaces(VisualElement panel)
    {
        ClearSelection();
        if (editor.activeList != null)
        {
            editor.RemoveList();
            
        }
        editor.AddList(raceList);

        if (panel.Contains(editorRoot))
        {
            panel.Remove(editorRoot);
        }
        panel.Add(editorRoot);
        raceList.onSelectionChange += OnRaceChange;
    }

    public void DisplayStats(VisualElement panel)
    {
        ClearSelection();
        if (editor.activeList != null)
        {
            editor.RemoveList();
        }
        editor.AddList(statList);

        if(panel.Contains(editorRoot))
        {
            panel.Remove(editorRoot);
        }
        panel.Add(editorRoot);
    }

    //Remove selected item from list
    public void RemoveListItem()
    {
        if(editor.activeList == attList)
        {
            attList.itemsSource.Remove(selectedAtt);
            selectedAtt= null;
            attList.RefreshItems();
            return;
        }

        if(editor.activeList == raceList)
        {
            raceList.itemsSource.Remove(selectedRace);
            selectedRace = null;
            raceList.RefreshItems();
            return;
        }

        if (editor.activeList == classList)
        {
            classList.itemsSource.Remove(selectedClass);
            selectedClass = null;
            classList.RefreshItems();
            return;
        }

        if (editor.activeList == statList)
        {
            statList.itemsSource.Remove(selectedStat);
            selectedStat = null;
            statList.RefreshItems();
            return;
        }
    }

    private void ClearSelection()
    {

        attList.ClearSelection();
        raceList.ClearSelection();
        classList.ClearSelection();
       

        editor.SetButtonStatus(false, true, false);
    }

    private bool isSomethingSelected()
    {
        if(selectedAtt != null|| selectedClass != null || selectedRace != null || selectedStat != null)
        {
            return true;
        }
        return false;
    }
}
