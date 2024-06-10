using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveTable", menuName = "Inventory/PassiveTable")]
public class PowerUpTable : ScriptableObject
{
    [SerializeField] List<PowerUps> list;
    public List<PowerUps> List => list;

    public void Add(PowerUps pu)
    {
        list.Add(pu);
    }

    public void LoadSkillsFromResources()
    {
        string path = "Skills/Passive/";
        list = Resources.LoadAll<PowerUps>(path).ToList();
    }
}
