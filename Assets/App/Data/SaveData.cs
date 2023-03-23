using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    [Header("File Info")]
    public string fileName;
    public string saveID;
    public long timeStamp;

    [Header("System Info")]
    public SystemData parentSystem;

    [Header("Content Info")]
    public List<Character> characterList;
    public List<Item> itemList;

    public SaveData(string fileName) 
    {
        this.fileName = fileName;
        parentSystem = new SystemData("New System");
    }
}
