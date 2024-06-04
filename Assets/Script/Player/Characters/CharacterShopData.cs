using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class CharacterShopData : ShopData
{
    private readonly CharacterClassData _characterClassData;

    public CharacterShopData(CharacterClassData characterClassData)
    {
        _characterClassData = characterClassData;
        var data = SaveUtility.LoadFromJSON<CharacterShopData>(_characterClassData.name);
        if(data == null)
            return;
        isBought = data.isBought;
    }
    public override void OnBuy()
    {
        base.OnBuy();
        SaveUtility.SaveToJSON<CharacterShopData>(_characterClassData.name, this);
    }

    public override void OnEquip()
    {
        base.OnEquip();
    }
}