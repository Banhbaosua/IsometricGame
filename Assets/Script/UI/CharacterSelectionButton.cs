using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectionButton : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField] private GameEvent _onCharBtnClick;
    [SerializeField] private GameEvent _onCharSelect;
    [SerializeField] private GameEvent _onWeaponSelect;
    [SerializeField] private CurrentClassData _currentClassData;
    private CharacterClassData _characterClassData;
    private BaseWeapon _weaponInReview;

    private void OnEnable()
    {
        _characterClassData = _currentClassData.GetCharacterClassData();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_characterClassData != _currentClassData.GetCharacterClassData())
        {
            _onCharSelect.Notify(this, _characterClassData);
            _onWeaponSelect.Notify(this, _weaponInReview);
        }
    }

    public void SetClass(Component sender, object data)
    {
        _characterClassData = (CharacterClassData)data;
    }

    public void SetReviewWeapon(Component sender, object weapon)
    {
        _weaponInReview = (BaseWeapon)weapon;
    }
}
