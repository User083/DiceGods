using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initialiseDataIfNull = false;
    [SerializeField] private bool saveOnQuit = true;
    [SerializeField] private bool disableDataPersistence = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private SaveData saveData; 
    public SaveData SaveData
    {
        get
        {
            return saveData;
        }
        private set 
        {
            saveData = value;
        }
    }

    
    public static DataPersistenceManager instance { get; private set; }
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    private string selectedSaveID = string.Empty;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one DPM in scene. New instance destroyed.");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        //InitSelectedSave();
        
    }

    private void OnEnable()
    {
        //subscribe to onScene events
        SceneManager.sceneLoaded+= OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        //unsubscribe onScene events
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    //When new scene loads
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        if (this.saveData != null)
        {
            LoadSave();
        }
        
    }

    //When scene is unloaded
    public void OnSceneUnloaded(Scene scene)
    {
        if(this.saveData != null)
        {
            Save(fileName);
        }
        
    }

    //update selected saveID
    public void ChangeSelectedSaveID(string newSaveID)
    {
        this.selectedSaveID = newSaveID;
        LoadSave();
    }

    //Delete save

    public void DeleteSaveData(string saveID)
    {
        dataHandler.DeleteSave(saveID);
        InitSelectedSave();
        LoadSave();
    }

    private void InitSelectedSave()
    {
        this.selectedSaveID = dataHandler.GetMostRecentSave();
      
    }

    //Create new save data object
    public void NewSave(string newSaveName)
    {
        this.fileName = newSaveName;
        this.dataHandler.UpdateFileName(newSaveName);
        this.SaveData = new SaveData(fileName);
    }

    //Load existing save
    public void LoadSave()
    {
        if(disableDataPersistence)
        {
            return;
        }

         if(this.saveData == null)
        {
            Debug.Log("No data found - new data needs to be initialised");
            return;
        }
        
        //Debugging only
        if(this.saveData == null && initialiseDataIfNull)
        {
           NewSave("Debug Save");
        }
        
        this.saveData = dataHandler.Load(selectedSaveID);

        foreach(IDataPersistence dpo in dataPersistenceObjects)
        {
            dpo.LoadData(saveData);
        }
    }

    //Save runtime data
    public void Save(string fileName)
    {
        this.fileName = fileName;
        if (disableDataPersistence)
        {
            return;
        }

        if (this.saveData == null)
        {
            Debug.LogWarning("No data found - new data needs to be initialised");
            return;
        }
        foreach (IDataPersistence dpo in dataPersistenceObjects)
        {
            dpo.SaveData(ref saveData);
        }

        //timestamp save
        saveData.lastUpdated = System.DateTime.Now.ToBinary();

        dataHandler.Save(saveData, selectedSaveID);
    }



    
    private void OnApplicationQuit()
    {
        //Move this to wherever you want to save the game
        if(saveOnQuit)
        {
            Save(fileName);
        }
        
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasSaveData()
    {
        return this.saveData != null;
    }

    //get all save slots from the data handler
    public Dictionary<string, SaveData> GetAllSaves()
    {
        return dataHandler.LoadAllSaves();
    }
}
