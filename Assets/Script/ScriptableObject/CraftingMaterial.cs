using Materials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Materials
{
    [CreateAssetMenu(fileName = "CraftingMaterial", menuName = "Crafting/Material")]
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
        }

        public void SaveValue()
        {
            
        }
    }
}