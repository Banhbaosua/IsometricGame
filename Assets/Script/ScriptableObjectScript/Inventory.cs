using Materials;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Inventory<T> : ScriptableObject where T : ScriptableObject
{
    [SerializeField] List<T> list;
    public Dictionary<string, T> dictFromList;

    private void OnEnable()
    {
        dictFromList = list.ToDictionary(x => x.name, x => x as T);
    }

    public void AddAmount(int amount)
    {

    }
}
