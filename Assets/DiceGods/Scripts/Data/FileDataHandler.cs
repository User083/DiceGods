using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "fokololos";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
        
    }

    public SaveData Load(string saveID)
    {
       if(saveID == null)
        {
            return null;
        }
        
        string fullPath = Path.Combine(dataDirPath, saveID, dataFileName);
        SaveData loadedData = null;
        
        if(File.Exists(fullPath))
        {
            try
            {
                //Load data from file
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }
                //Deserialize data
                loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);
            }
            catch(Exception e)
            {
           
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        else
        {
            //Debug.LogError("No such file exists");
        }
        return loadedData;
    }

    public void Save(SaveData data, string saveID)
    {

        if (saveID == null)
        {
            return;
        }
        string fullPath = Path.Combine(dataDirPath, saveID, dataFileName);
        try
        {
            //Create directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize data into JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            if(useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }
            //Write serialized data to file
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            if(dataFileName.Length == 0)
            {
                Debug.LogError("Invalid file name. Error occured when trying to save data to file: " + fullPath + "\n" + e);
            }
            else
            {
                Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
            }
            
        }
    }

    public void DeleteSave(string saveID)
    {
        if(saveID == null)
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, saveID, dataFileName);

        try
        {
            if(File.Exists(fullPath))
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
        catch(Exception e)
        {
            Debug.LogError("Failed to delete save data for: " + saveID + " at path " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, SaveData> LoadAllSaves()
    {
        Dictionary<string, SaveData> savesDictionary = new Dictionary<string, SaveData>();

        //loop over all saves in path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach(DirectoryInfo dirInfo in dirInfos)
        {
            string saveID = dirInfo.Name;

            //safety check
            string fullPath = Path.Combine(dataDirPath, saveID, dataFileName);
            if(!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory" + saveID + ": does not contain save data");
                continue;
            }

            SaveData saveData = Load(saveID);

            if(saveData!= null)
            {
                savesDictionary.Add(saveID, saveData);
            }
            else
            {
                Debug.LogError("Error occurred loading: " + saveID);
            }
        }

        return savesDictionary;
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for(int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }

    //get the most recent save
    public string GetMostRecentSave()
    {
        string mostRecentSave = null;

        Dictionary<string, SaveData> savesData = LoadAllSaves();
        foreach(KeyValuePair<string, SaveData> kvp in savesData)
        {
            string saveID = kvp.Key;
            SaveData saveData = kvp.Value;

            if(saveData == null)
            {
                continue;
            }

            if(mostRecentSave == null)
            {
                mostRecentSave = saveID;
            }
            else
            {
                DateTime mostRecent = DateTime.FromBinary(savesData[mostRecentSave].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(saveData.lastUpdated);

                if(newDateTime > mostRecent)
                {
                    mostRecentSave = saveID;
                }
            }

        }
        return mostRecentSave;
    }
}
