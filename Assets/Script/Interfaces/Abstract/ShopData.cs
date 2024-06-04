using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopData : IBuyable
{
    [SerializeField] protected bool isBought;
    public bool IsBought => isBought;

    public virtual void OnBuy()
    {
        isBought = true;
    }

    public virtual void OnEquip()
    {
        
    }
}
