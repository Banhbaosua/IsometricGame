using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Currency", menuName = "Currency/Currency")]
[Serializable]
public class Currency : ScriptableObject, IAdjustable<int>
{
    [SerializeField] private CurrencyType type;
    [SerializeField] private int amount;
    [SerializeField] private Sprite icon;
    public int Amount => amount;
    public Sprite Icon => icon;
    public void AddValue(int value)
    {
        this.amount += value;
        SaveValue();
    }

    public void SaveValue()
    {
        SaveUtility.SaveToJSON<CurrencyData>(this.name, new CurrencyData(amount));
    }

    public void LoadValue()
    {
        if(SaveUtility.LoadFromJSON<CurrencyData>(this.name) != null)
            amount = SaveUtility.LoadFromJSON<CurrencyData>(this.name).amount;
        Debug.Log(amount);
    }

    private void OnEnable()
    {
        LoadValue();
    }
}

[Serializable]
public class CurrencyData
{
    public int amount;
    public CurrencyData(int amount)
    {
        this.amount = amount;
    }
}

public enum CurrencyType
{
    Corrupted,
    Hateful,
    Minor,
    Rogue,
    Vile,
    Wicked,
}
