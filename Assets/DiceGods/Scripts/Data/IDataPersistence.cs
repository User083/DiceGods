using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    //no need to pass by ref as data will just be read
    void LoadData(SaveData data);
    //pass by reference as data will be manipulated
    void SaveData(ref SaveData data);

}
