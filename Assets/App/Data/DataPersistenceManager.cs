using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager instance { get; private set; }

    private Dictionary<string, SaveData> existingSaves = new Dictionary<string, SaveData>();
    public SaveData activeSave;
    
    public RPGSystem SystemManager;

    private string saveDirectoryPath = "";
    private string saveFileName = "";
    private string activeSaveID;
    private List<IDataPersistence> dataPersistenceObjects;



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        saveDirectoryPath = "K:\\Projects\\SaveTest";
        LoadAllSaves();
        dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    private void OnEnable()
    {
     
    }


    private void Start()
    {
      
    }

   

    private string generateSaveID(string fileName)
    {
        string random = UnityEngine.Random.Range(0, 500).ToString();
        string saveID = fileName + random;

        return saveID;
    }

    public SaveData initialiseNewSave(string fileName)
    {       
        SaveData newSave = new SaveData(fileName);     
        newSave.saveID = generateSaveID(fileName);
        saveFileName = fileName;
        Save(newSave, newSave.saveID);
        activeSave = newSave;
        activeSaveID = newSave.saveID;
        
        return newSave;
    }

    public void Save(SaveData data, string saveID)
    {
        
        if (saveID == null)
        {
            return;
        }

        saveFileName = data.fileName;
        //updateDPO(null, data);

            //timestamp save
            data.timeStamp = System.DateTime.Now.ToBinary();
        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(saveDirectoryPath, saveID, saveID);
       
        try
        {
            // create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object into Json
            string dataToStore = JsonUtility.ToJson(data, true);

        
            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
        
    }


    public SaveData Load(string saveID)
    {
        
        if (saveID == null)
        {
            return null;
        }

        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(saveDirectoryPath, saveID, saveID);
        SaveData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // deserialize the data from Json back into the C# object
                loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to LOAD data to file: " + fullPath + "\n" + e);
            }
        }

        //updateDPO(loadedData, null);

        return loadedData;
    }

    public void updateActiveSave(string saveID)
    {
        SaveData saveToLoad = Load(saveID);
        if (saveToLoad != null)
        {
            activeSave= saveToLoad;
            activeSaveID = saveToLoad.saveID;
        }
    }

    private void updateDPO(SaveData loadData, SaveData saveData)
    {
        if(dataPersistenceObjects.Count > 0 && loadData !=null)
        {
            foreach (IDataPersistence dpo in dataPersistenceObjects)
            {
                dpo.LoadData(loadData);
            }
        }
        else if(dataPersistenceObjects.Count > 0 && saveData != null)
        {
            foreach (IDataPersistence dpo in dataPersistenceObjects)
            {
                dpo.SaveData(saveData);
            }
        }
    }

  

    //Load all saved data and return as dictionary

    public Dictionary<string, SaveData> LoadAllSaves()
    {
       
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(saveDirectoryPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string saveID = dirInfo.Name;
            //Debug.Log(dirInfo.Name + " directoryName");

            string fullPath = Path.Combine(saveDirectoryPath, saveID, saveID);
            //Debug.Log(fullPath + " fullpath");
            if (!File.Exists(fullPath))
            {
                continue;
            }

            // load the game data for this profile and put it in the dictionary
            SaveData saveData = Load(saveID);
            
            if (saveData != null)
            {
                if(!existingSaves.ContainsKey(saveID))
                {
                    existingSaves.Add(saveID, saveData);
                }
                
            }
            else
            {
                Debug.LogError("Tried to load save but something went wrong. SaveID: " + saveID);
            }
        }

        return existingSaves;
    }

    //Delete selected save by saveID

    public void DeleteSaveData(string saveID)
    {
       
            if (saveID == null)
            {
                return;
            }

            string fullPath = Path.Combine(saveDirectoryPath, saveID, saveID);

            try
            {
                if (File.Exists(fullPath))
                {
                    //Delete entire save directory
                    Directory.Delete(Path.GetDirectoryName(fullPath), true);
                    //Delete only saved file
                    //File.Delete(Path.GetFileName(fullPath), true);
                }
                else
                {
                    Debug.LogWarning("Can't delete save data: " + fullPath + " path not found");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to delete save data for: " + saveID + " at path " + fullPath + "\n" + e);
            }
        
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
