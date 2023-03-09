using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour, IDataPersistence
{
    private int testInt = 0;

    void IDataPersistence.LoadData(SaveData data)
    {
        this.testInt = data.testNo;
    }

    void IDataPersistence.SaveData(ref SaveData data)
    {
        data.testNo = this.testInt;
    }

    private void Start()
    {
        testInt = 10;
    }

    private void Update()
    {
        testInt = 10;
    }

}
