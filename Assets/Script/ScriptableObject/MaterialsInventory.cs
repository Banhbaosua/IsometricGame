using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Materials 
{
    enum rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Lengendary,
    }

    public enum droppableObject
    {
        Enemy,
        Ore,
    }

    [CreateAssetMenu(fileName = "CraftingInventory", menuName = "Crafting/Inventory")]
    public class MaterialsInventory : Inventory<CraftingMaterial>
    {
        
    }
}