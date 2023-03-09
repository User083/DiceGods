using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attribute", menuName = "DiceGods/Attribute", order = 1)]
[System.Serializable]

public class Attribute : ScriptableObject
{
    [SerializeField]private string attName;
    [SerializeField] private string attDesc;
    private float attModifier;
    [SerializeField] private int attBaseValue;
    private int attMaxValue;
    private int attFinalValue;
    [SerializeField] private List<Modifier> attModifiers;


    private void Awake()
    {
        getModifiers();
        calcFinalValue();
    }

    public void setMaxValue(int maxValue)
    {
        attMaxValue = maxValue;
    }

    public void getModifiers()
    {
        if(attModifiers.Count > 0)
        {
            foreach(Modifier mod in attModifiers)
            {
                if(mod.totalModValue != 0)
                {
                    attModifier = attModifier + mod.totalModValue;
                }
            }
        }
        else
        {
            attModifier = 1;
        }
    }

    public void calcFinalValue()
    {
        if(attMaxValue!= 0)
        {
            attFinalValue = Mathf.RoundToInt((float)attBaseValue * attModifier);
        } 
        else
        {
            attFinalValue = attBaseValue;
        }
        
    }

}
