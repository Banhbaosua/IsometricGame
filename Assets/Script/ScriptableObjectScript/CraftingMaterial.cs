using Materials;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Materials
{
    [CreateAssetMenu(fileName = "CraftingMaterial", menuName = "Crafting/Material")]
    [Serializable]
    public class CraftingMaterial : ScriptableObject, IAdjustable<int>
    {
        [SerializeField] rarity rarity;
        [SerializeField] droppableObject droppableObject;
        [SerializeField] int amount;
        [SerializeField] Sprite matSprite;

        public Sprite MatSprite => matSprite;
        public int Amount => amount;

        public void AddValue(int value)
        {
            this.amount += value;
            SaveValue();
        }

        public void SaveValue()
        {
            SaveUtility.SaveToJSON<CraftingMaterialData>(this.name, new CraftingMaterialData(amount));
        }

        public void LoadValue()
        {
            if (SaveUtility.LoadFromJSON<CraftingMaterialData>(this.name) != null)
                amount = SaveUtility.LoadFromJSON<CraftingMaterialData>(this.name).amount;;
        }
    }

    [Serializable]
    public class CraftingMaterialData
    {
        public int amount;
        public CraftingMaterialData(int amount)
        {
            this.amount = amount;
        }
    }
}