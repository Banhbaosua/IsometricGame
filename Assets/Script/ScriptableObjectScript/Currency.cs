using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Currency", menuName = "Currency/Currency")]
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
        SaveUtility.SaveToJSON<Currency>(this.name, this);
    }

    public void LoadValue()
    {
        SaveUtility.LoadSOFromJSON(this.name, this);
        Debug.Log(amount);
    }

    private void OnEnable()
    {
        LoadValue();
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
