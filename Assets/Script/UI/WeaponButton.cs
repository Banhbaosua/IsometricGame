using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int tier;
    [SerializeField] BaseWeapon weaponData;
    [SerializeField] CurrentClassData characterData;
    [SerializeField] GameEvent _onWeaponSelect;
    [SerializeField] GameEvent _onWeaponReview;
    [SerializeField] Image _weaponImage;
    [SerializeField] CharacterClassRefMenu _characterMenu;
    [SerializeField] private Transform lockedFIlter;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (characterData.GetCharacterClassData()!= weaponData.CharacterClassData)
        {
            _onWeaponReview.Notify(this, weaponData);
        }
        else
            _onWeaponSelect.Notify(this, weaponData);
    }

    /// <summary>
    /// method used in in editor
    /// </summary>
    /// <param name="Sender"></param>
    /// <param name="Data"></param>
    public void SetWeaponData(Component Sender, object Data)
    {
        if (Data == null)
            return;
        var data = Data as CharacterClassData;
        weaponData = data.weapons[tier-1];

        SaveDefaultWeapon();
    }

    /// <summary>
    /// method used in editor
    /// </summary>
    /// <param name="Sender"></param>
    /// <param name="Data"></param>
    public void SetWeaponImg(Component Sender, object Data)
    {
        var data = Data as CharacterClassData;
        _weaponImage.sprite = data.weapons[tier - 1].WeapImg;
    }

    public void InitiateData()
    {
        Debug.Log("initiate weapon");
        weaponData = characterData.GetCharacterClassData().weapons[tier-1];
        SetWeaponImg(this, characterData.GetCharacterClassData());
        lockedFIlter.gameObject.SetActive(!weaponData.isUnlocked);
        SaveDefaultWeapon();
    }

    private void SaveDefaultWeapon()
    {
        if (tier == 1)
        {
            SaveData<string> name = new SaveData<string>(weaponData.name);
            SaveUtility.SaveToJSON("currentWeapon", name);
        }
    }
    private void OnEnable()
    {
        InitiateData();
    }
}
