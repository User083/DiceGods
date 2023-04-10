using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SystemDisplay 
{
    public VisualElement root;
    public DataPopulater populater = new DataPopulater();
    private VisualTreeAsset elementSlot;
    private SystemData blankSystem;

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
    //Foldouts
    public Foldout attFoldout;
    public Foldout classFoldout;
    public Foldout racesFoldout;

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

        blankSystem = parentSystem;
    
        if(editable)
        {

            attFoldout.SetEnabled(false);
            classFoldout.SetEnabled(false);
            racesFoldout.SetEnabled(false);


        }
        else
        {
            systemNameField.isReadOnly= true;
            levelsToggle.SetEnabled(editable);
            classesToggle.SetEnabled(editable);
            racesToggle.SetEnabled(editable);
            coreStatsToggle.SetEnabled(editable);
            attributesToggle.SetEnabled(editable);
            charsHaveValueToggle.SetEnabled(editable);
            weightToggle.SetEnabled(editable);
        }

        populater.PopulateAttributes(parentSystem, elementSlot, attFoldout);
        populater.PopulateRaces(parentSystem, elementSlot, racesFoldout);
        populater.PopulateClasses(parentSystem, elementSlot, classFoldout);

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

        populater.PopulateAttributes(blankSystem, elementSlot, attFoldout);
        populater.PopulateRaces(blankSystem, elementSlot, racesFoldout);
        populater.PopulateClasses(blankSystem, elementSlot, classFoldout);
    }
}
