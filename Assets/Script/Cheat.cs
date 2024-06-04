using Materials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    [SerializeField] CurrencyInventory currencyInventory;
    [SerializeField] MaterialsInventory materialsInventory;
    
    public void MaxCurrency()
    {
        foreach(KeyValuePair<string, Currency> item in currencyInventory.dictFromList)
        {
            item.Value.AddValue(9999);
        }
    }
}
