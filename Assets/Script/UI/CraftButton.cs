using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameEvent onWeaponCraft;
    [SerializeField] Transform craftedObj;
    [SerializeField] Transform unCraftedObj;
    [SerializeField] Text unCraftedText;
    [SerializeField] CurrentClassData currentClassData;
    [SerializeField] CurrencyInventory currencyInventory;
    BaseWeapon weapon;

    private void OnEnable()
    {
        InitiateData();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currencyInventory.dictFromList["Minor Soulstones"].Amount > weapon.MinorSoulReq)
            onWeaponCraft.Notify(this, weapon);
    }

    public void SetWeaponToCraft(Component sender, object weapon)
    {
        if(weapon.GetType() == typeof(BaseWeapon))
            this.weapon = (BaseWeapon)weapon;
    }

    public void SetUI(Component sender, object weapon)
    {
        if (weapon.GetType() == typeof(BaseWeapon))
        {
            BaseWeapon weap = (BaseWeapon)weapon;
            string text = weap.MinorSoulReq.ToString();

            craftedObj.gameObject.SetActive(weap.isUnlocked);
            unCraftedObj.gameObject.SetActive(!weap.isUnlocked);
            if(!weap.isUnlocked)
                unCraftedText.text = text;
        }
    }

    public void CraftWeapon(Component sender, object weapon)
    {
        if (weapon != null)
        {
            this.weapon.SetUnlock(true);
            currencyInventory.dictFromList["Minor Soulstones"].AddValue(-this.weapon.MinorSoulReq);
            craftedObj.gameObject.SetActive(true);
            unCraftedObj.gameObject.SetActive(false);
        }
    }

    public void InitiateData()
    {
        weapon = currentClassData.GetCharacterClassData().weapons[1];
        craftedObj.gameObject.SetActive(weapon.isUnlocked);
        unCraftedObj.gameObject.SetActive(!weapon.isUnlocked);

        if (!weapon.isUnlocked)
            unCraftedText.text = weapon.MinorSoulReq.ToString();
    }
}
