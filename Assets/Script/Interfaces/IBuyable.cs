using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuyable
{
    public bool IsBought { get; }

    public void OnBuy();
    public void OnEquip();
        
}
