using Materials;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingAreaButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int _tier;
    [SerializeField] CurrentClassData _currentClassData;
    [SerializeField] private MaterialsInventory _matInventory;

    [SerializeField] private Image _weaponImage;
    [SerializeField] private Text _weaponName;
    [SerializeField] private List<Image> _material;

    [SerializeField] private List<Text> _materialReqText;
    [SerializeField] private Transform unlockedCheckImg;
    [SerializeField] GameEvent onWeaponChoose;
    [SerializeField] Transform materialsReqTrans;
    [SerializeField] private Transform selectedFilter;
    BaseWeapon weapon;

    private void OnEnable()
    {
        InitiateCraftingInfor();
    }

    public void InitiateCraftingInfor()
    {
        var classWeaponList = _currentClassData.GetCharacterClassData();
        weapon = classWeaponList.weapons[_tier - 1];
        _weaponImage.sprite = weapon.WeapImg;
        _weaponName.text = weapon.Name;

        var reqList = weapon.UnlockRequirement;
        for (int i = 0; i < reqList.Count; i++) 
        {
            _material[i].transform.parent.gameObject.SetActive(true);
            _material[i].sprite = _matInventory.dictFromList[reqList[i].Material.name].MatSprite;
            _materialReqText[i].text = _matInventory.dictFromList[reqList[i].Material.name].Amount +"/"+ reqList[i].Requirement.ToString();
        }
        unlockedCheckImg.gameObject.SetActive(weapon.isUnlocked);
        materialsReqTrans.gameObject.SetActive(!weapon.isUnlocked);
    }

    public void SetCrafted(Component sender, object weapon)
    {
        if(weapon != null && this.weapon == (BaseWeapon)weapon) 
        { 
            this.unlockedCheckImg.gameObject.SetActive(true);
            materialsReqTrans.gameObject.SetActive(false);
        }
    }
    public void EnableSelectFilter(Component sender, object weapon) 
    { 
        if(sender == this)
            selectedFilter.gameObject.SetActive(true) ;
        else
            selectedFilter.gameObject.SetActive(false) ;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        onWeaponChoose.Notify(this, weapon);
    }
}
