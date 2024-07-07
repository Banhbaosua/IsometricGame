using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUnlockBoard : MonoBehaviour
{
    [SerializeField] Text cost;
    [SerializeField] Image currencyIcon;
    [SerializeField] Button unlockBtn;
    [SerializeField] CurrencyInventory currencyInventory;
    [SerializeField] Image passiveIcon;
    private SkillTreePassiveSO passiveData;

    public void Set(Component sender, object data)
    {
        passiveData = data as SkillTreePassiveSO;
        var currName = passiveData.Data.Requirements.CurrencyType.ToString() + " Soulstones";
        var curr = currencyInventory.dictFromList[currName];

        currencyIcon.sprite = curr.Icon;
        passiveIcon.sprite = passiveData.Icon;

        cost.text = passiveData.Data.Requirements.CurrencyRequire.ToString();

        var passive = passiveData.Data;
        if (passive.Requirements.CurrencyRequire < currencyInventory.dictFromList[currName].Amount)
        { 
            unlockBtn.interactable = true;
            cost.color = Color.white;
        }
        else
        {
            unlockBtn.interactable = false;
            cost.color = Color.red;
        }
    }

    //use for button
    public void UnlockPassive()
    {
        if (passiveData != null)
        {
            passiveData.UnlockTier();
            Set(this, passiveData);
        }
    }
}
