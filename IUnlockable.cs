using System.Collections;
using System.Collections.Generic;
using UnityEngine;

protected interface IUnlockable 
{
    List<MaterialRequirement> UnlockRequirement { get; set; }
    bool isUnlocked { get;}
    public bool IsUnlocked => isUnlocked; 
    public void SetUnlock() { }
}
[System.Serializable]
public class MaterialRequirement
{
    [SerializeField] string name;
    [SerializeField] Sprite matSprite;
    //[SerializeField] int requirement;
    public string Name => name;
    public Sprite MatSprite => matSprite;
    //public int Requirement => requirement;
}
