using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{

    [Header("System Info")]

    

    public int testNo;

    public long lastUpdated;

    
    //Init default values for new save
    public SaveData() 
    {
        this.testNo = 0;
    }
}
