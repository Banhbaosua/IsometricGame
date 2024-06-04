using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInformation : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolderMain;
    [SerializeField] private Transform _weaponHolderOffhand;
    [SerializeField] private CharacterDataComponentMenu _character;
    [SerializeField] private List<Transform> _mainWeapList;
    [SerializeField] private List<Transform> _offHandWeapList;
    public Transform WeaponHolderMain => _weaponHolderMain;
    public Transform WeaponHolderOffhand => _weaponHolderOffhand;

    private void OnEnable()
    {
    }
}
