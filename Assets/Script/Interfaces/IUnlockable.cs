using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Materials;

public interface IUnlockable
{
    List<MaterialRequirement> UnlockRequirement { get; }
    bool isUnlocked { get;}
    public void SetUnlock(bool val) { }
}
[System.Serializable]
public class MaterialRequirement
{
    [SerializeField] CraftingMaterial material;
    [SerializeField] int requirement;
    public CraftingMaterial Material => material;
    public int Requirement => requirement;
}