using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class SystemDisplay 
{
    public VisualElement root;
    public DataPopulater populater = new DataPopulater();
    private VisualTreeAsset elementSlot;
    private SystemData blankSystem;
    public ListEditor editor;
    public VisualElement editorListRoot;
    public VisualElement editorPopupRoot;
    public VisualElement editorPanel;
    public UIDocument editorPopupUI;
    public VisualElement activeEditorSlot;
    public ElementSlot activeEditorSlotLogic;
    public NewSystem_Manager systemManager;

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
        coreStatsToggle.value = currentSystemData.useCoreStats;
        systemNameField.value = currentSystemData.systemName;
    }

    public void SetDisplayData(SystemData parentSystem)
    {
        
        if (parentSystem == null)
        {
            Debug.LogError("No parent system found - characters can't exist outside a system. Have you loaded a save?");
        }

        attButton.RemoveFromHierarchy();
        raceButton.RemoveFromHierarchy();
        classButton.RemoveFromHierarchy();
        statsButton.RemoveFromHierarchy();
        systemNameField.isReadOnly = true;
        levelsToggle.SetEnabled(false);
        classesToggle.SetEnabled(false);
        racesToggle.SetEnabled(false);
        attributesToggle.SetEnabled(false);
        charsHaveValueToggle.SetEnabled(false);
        weightToggle.SetEnabled(false);
        coreStatsToggle.SetEnabled(false);

        if (parentSystem.useAttributes)
        {
            populater.PopulateAttributes(parentSystem, elementSlot, attFoldout, false);

        }
        else
        {
            attFoldout.RemoveFromHierarchy();
        }

        if(parentSystem.useRaces)
        {
            populater.PopulateRaces(parentSystem, elementSlot, racesFoldout);
        }
        else
        {
            racesFoldout.RemoveFromHierarchy();
        }

        if (parentSystem.useClasses)
        {
            populater.PopulateClasses(parentSystem, elementSlot, classFoldout);
        }
        else
        {
            classFoldout.RemoveFromHierarchy();
        }

        if (parentSystem.useCoreStats)
        {
            populater.PopulateStats(parentSystem, elementSlot, statsFoldout);
        }
        else
        {
            statsFoldout.RemoveFromHierarchy();
        }


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
        attList = populater.PopulateAttributeList(elementSlot, false);

        populater.EnumerateRaces(parentSystem);
        raceList = populater.PopulateRaceList(elementSlot);

        populater.EnumerateClasses(parentSystem);
        classList = populater.PopulateClassList(elementSlot);

        populater.EnumerateStats(parentSystem);
        statList = populater.PopulateStatList(elementSlot);

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
        attList = populater.PopulateAttributeList(elementSlot, false);

        populater.EnumerateRaces(blankSystem);
        raceList =populater.PopulateRaceList(elementSlot);

        populater.EnumerateClasses(blankSystem);
        classList = populater.PopulateClassList(elementSlot);

        populater.EnumerateStats(blankSystem);
        statList = populater.PopulateStatList(elementSlot);

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
        editor.SetButtonStatus(true, true);

    }

    public void OnStatChange(IEnumerable<object> selectedItems)
    {
        var currentStat = statList.selectedItem as Stat;

        if (currentStat == null)
        {
            selectedStat = null;

            return;
        }

        selectedStat = currentStat;
        editor.SetButtonStatus(true, true);

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
        editor.SetButtonStatus(true, true);

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
        editor.SetButtonStatus(true, true);

    }

    //Setup editor tool docs

    public void EditorSetup(VisualTreeAsset editorList, UIDocument editorPopup, NewSystem_Manager systemManager)
    {
        this.systemManager = systemManager;
        editorListRoot = editorList.Instantiate();
        editorListRoot.style.flexGrow = 1;
        editorListRoot.style.width = Length.Percent(59);
        editorPopupUI = editorPopup;
        editorPopupRoot = editorPopup.rootVisualElement;
        editor = new ListEditor(editorListRoot, editorPopupRoot);
        editorPanel = editorPopupRoot.Q<VisualElement>("ep-panel-editor");
        editor.removeButton.clickable.clicked += () => RemoveListItem();
        editor.newButton.clickable.clicked += () => AddNew(editor.activeList);
        editor.cancelButton.clickable.clicked += () => CancelEdit();
        editor.createButton.clickable.clicked += () => AddToList(editor.activeList);


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

        if (panel.Contains(editorListRoot))
        {
            panel.Remove(editorListRoot);
        }
        panel.Add(editorListRoot);
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

        if (panel.Contains(editorListRoot))
        {
            panel.Remove(editorListRoot);
        }
        panel.Add(editorListRoot);
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

        if (panel.Contains(editorListRoot))
        {
            panel.Remove(editorListRoot);
        }
        panel.Add(editorListRoot);
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

        if(panel.Contains(editorListRoot))
        {
            panel.Remove(editorListRoot);
        }
        panel.Add(editorListRoot);
        statList.onSelectionChange += OnStatChange;
    }

    //Remove selected item from list
    public void RemoveListItem()
    {
        if(editor.activeList == attList)
        {
            attList.itemsSource.Remove(selectedAtt);
            //AttributeSlots.Remove(selectedAtt);
            selectedAtt = null;
            attList.RefreshItems();
            return;
        }

        if(editor.activeList == raceList)
        {
            raceList.itemsSource.Remove(selectedRace);
            //RaceSlots.Remove(selectedRace);
            selectedRace = null;
            raceList.RefreshItems();
            return;
        }

        if (editor.activeList == classList)
        {
            classList.itemsSource.Remove(selectedClass);
            //ClassSlots.Remove(selectedClass);
            selectedClass = null;
            classList.RefreshItems();
            return;
        }

        if (editor.activeList == statList)
        {
            statList.itemsSource.Remove(selectedStat);
            //StatSlots.Remove(selectedStat);
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
        statList.ClearSelection();
       

        editor.SetButtonStatus(false, true);
    }

    //Open the individual slot editor and set up slot logic
    private void AddNew(ListView list)
    {
        editorPopupUI.sortingOrder = 3;
        activeEditorSlot = elementSlot.Instantiate();
        activeEditorSlotLogic = new ElementSlot();
        activeEditorSlot.userData = activeEditorSlotLogic;
        activeEditorSlotLogic.SetVisualElement(activeEditorSlot);
        activeEditorSlotLogic.name.isReadOnly= false;
        activeEditorSlotLogic.description.isReadOnly= false;
        activeEditorSlotLogic.maxValue.isReadOnly = false;
        activeEditorSlotLogic.defaultValue.isReadOnly = false;
        if (list == attList)
        {
            activeEditorSlotLogic.SetDisplay("attribute");
        }
        if (list == raceList)
        {
            activeEditorSlotLogic.SetDisplay("race");
        }
        if (list == classList)
        {
            activeEditorSlotLogic.SetDisplay("class");
        }
        if (list == statList)
        {
            activeEditorSlotLogic.SetDisplay("stat");
        }

        editorPanel.Add(activeEditorSlot);

    }

    private void CancelEdit()
    {
        editorPopupUI.sortingOrder = 0;
        editorPanel.Remove(activeEditorSlot);
        activeEditorSlot = null;
    }

    //Add newly created element to the relevant List
    private void AddToList(ListView list)
    {
        if (list == attList)
        {
            Attribute temp = new Attribute(systemManager.newSystem, activeEditorSlotLogic.name.value, activeEditorSlotLogic.description.value, activeEditorSlotLogic.defaultValueSlider.value);
            //AttributeSlots.Add(temp, activeEditorSlotLogic);
            list.itemsSource.Add(temp);
            list.RefreshItems();
        }

        if (list == raceList)
        {
            Race temp = new Race(systemManager.newSystem, activeEditorSlotLogic.name.value, activeEditorSlotLogic.description.value);
            //RaceSlots.Add(temp, activeEditorSlotLogic);
            list.itemsSource.Add(temp);
            list.RefreshItems();
        }

        if (list == classList)
        {
            CharacterClass temp = new CharacterClass(systemManager.newSystem, activeEditorSlotLogic.name.value, activeEditorSlotLogic.description.value);
            //ClassSlots.Add(temp, activeEditorSlotLogic);
            list.itemsSource.Add(temp);
            list.RefreshItems();
        }

        if (list == statList)
        {
            Stat temp = new Stat(systemManager.newSystem, activeEditorSlotLogic.name.value, activeEditorSlotLogic.description.value, activeEditorSlotLogic.maxValue.value);
            //StatSlots.Add(temp, activeEditorSlotLogic);
            list.itemsSource.Add(temp);
            list.RefreshItems();
        }


        CancelEdit();
    }

    //private void EditItem(ListView list)
    //{
    //    if (list == attList)
    //    {
    //        AttributeSlots[selectedAtt].MakeEditble();
    //    }

    //    if (list == raceList)
    //    {
    //        RaceSlots[selectedRace].MakeEditble();
    //    }

    //    if (list == classList)
    //    {
    //        ClassSlots[selectedClass].MakeEditble();
    //    }

    //    if (list == statList)
    //    {
    //        StatSlots[selectedStat].MakeEditble();
    //    }
    //}

    
}
