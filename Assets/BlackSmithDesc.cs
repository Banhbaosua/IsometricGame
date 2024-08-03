using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithDesc : MonoBehaviour
{
    [SerializeField] Text skillName;
    [SerializeField] Text skillDescription;
    [SerializeField] Image skillIcon;
    [SerializeField] CurrentClassData currentClassData;
    private BaseWeapon weapon;

    public void SetWeapon(Component sender, object data)
    {
        weapon = (BaseWeapon)data;
        skillName.text = weapon.Name;
        skillDescription.text = weapon.BaseSkill.Description;
        skillIcon.sprite = weapon.BaseSkill.Icon;
    }
    private void OnEnable()
    {
        weapon = currentClassData.GetWeapon();
        SetWeapon(this, weapon);
    }
}
