using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class DataPersistenceManager : MonoBehaviour
{

    private SaveData saveData;
    private List<IDataPersistance> dataPersistanceObjects;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one Data Persistance Manager Present");
        }
        instance = this;
    this.dataPersistanceObjects = FindAllDataPersistanceObjects();
    }

    private void Start()
    {
        
    }

    public void NewSystem()
    {
        this.saveData= new SaveData();
    }

    public void LoadSystem()
    {
        if (this.saveData == null)
        {
            NewSystem();
        }

        foreach(IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(saveData);
        }

        Debug.Log("Loaded systems " + saveData.savedSystems.Count);
    }

    public void SaveSystem() 
    {
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref saveData);
        }

        Debug.Log("Saved systems " + saveData.savedSystems.Count);
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }

  
}
