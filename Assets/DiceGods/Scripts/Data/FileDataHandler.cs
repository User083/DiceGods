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

    public SaveData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
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

    public void Save(SaveData data)
    {
        
        string fullPath = Path.Combine(dataDirPath, dataFileName);
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

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for(int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
