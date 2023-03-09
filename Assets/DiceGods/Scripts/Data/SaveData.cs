using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{

    [Header("System Info")]

    public int testNo;

    
    //Init default values for new save
    public SaveData() 
    {
        this.testNo = 0;
    }
}
