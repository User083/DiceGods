using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attribute", menuName = "DiceGods/Attributes", order = 1)]
[System.Serializable]

public class Attribute : ScriptableObject
{
    public string attributeName;
    public string attributeValue;
}
