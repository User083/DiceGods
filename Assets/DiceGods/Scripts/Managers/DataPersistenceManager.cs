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

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    
    private SaveData saveData;
    public static DataPersistenceManager instance { get; private set; }
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

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
        //Call this wherever you want to load
        LoadSave();
    }

    //When scene is unloaded
    public void OnSceneUnloaded(Scene scene)
    {
        Save();
    }

    //Create new save data object
    public void NewSave()
    {
        this.saveData= new SaveData();
    }

    //Load existing save
    public void LoadSave()
    {
        this.saveData = dataHandler.Load();

       //Debugging only
        if(this.saveData == null && initialiseDataIfNull)
        {
            NewSave();
        }
        
        if(this.saveData == null)
        {
            Debug.Log("No data found - new data needs to be initialised");
            return;
        }

        foreach(IDataPersistence dpo in dataPersistenceObjects)
        {
            dpo.LoadData(saveData);
        }
    }

    //Save runtime data
    public void Save()
    {

        if(this.saveData == null)
        {
            Debug.LogWarning("No data found - new data needs to be initialised");
            return;
        }
        foreach (IDataPersistence dpo in dataPersistenceObjects)
        {
            dpo.SaveData(ref saveData);
        }

        dataHandler.Save(saveData);
    }



    
    private void OnApplicationQuit()
    {
        //Move this to wherever you want to save the game
        if(saveOnQuit)
        {
            Save();
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
}
