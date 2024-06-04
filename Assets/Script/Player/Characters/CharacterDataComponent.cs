using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataComponent : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private CurrentClassData currentClassData;
    [SerializeField] private Transform rightWeapHolder;
    [SerializeField] private Transform leftWeapHolder;
    public CharacterData CharacterData=>characterData;
    private void Awake()
    {
        InitiateWeaponData();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitiateWeaponData()
    {
        EnableWeapon(rightWeapHolder);
        EnableWeapon(leftWeapHolder);
    }

    private void EnableWeapon(Transform weaponHolder)
    {
        foreach(Transform weapon in weaponHolder)
        {
            if (weapon.GetSiblingIndex() == currentClassData.GetWeapon().Tier - 1)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
        }
    }
}
