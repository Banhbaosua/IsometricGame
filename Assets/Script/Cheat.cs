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
            item.Value.AddValue(999999);
        }
    }

    public void MaxMat()
    {
        foreach(KeyValuePair<string, CraftingMaterial> item in materialsInventory.dictFromList)
        {
            item.Value.AddValue(999999);
        }
    }
}
