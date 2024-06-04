using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private CharacterClassData characterBonusData;
    private void Start()
    {
        Debug.Log(characterData.MaxHealth);
        Debug.Log(characterData.StatList[StatType.MaxHealth]);
    }
}
