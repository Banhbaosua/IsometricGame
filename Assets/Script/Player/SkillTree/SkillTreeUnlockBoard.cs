using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUnlockBoard : MonoBehaviour
{
    [SerializeField] Text cost;
    [SerializeField] Text description;
    [SerializeField] Image currencyIcon;
    [SerializeField] Button unlockBtn;
    [SerializeField] CurrencyInventory currencyInventory;
    [SerializeField] Image passiveIcon;
    private SkillTreePassiveComponent passiveData;

    public void Set(Component sender, object data)
    {
        passiveData = data as SkillTreePassiveComponent;
        var currName = passiveData.SkillTreeSO.NextTierData.Requirements.CurrencyType.ToString() + " Soulstones";
        var curr = currencyInventory.dictFromList[currName];

        currencyIcon.sprite = curr.Icon;
        currencyIcon.gameObject.SetActive(true);
        passiveIcon.sprite = passiveData.SkillTreeSO.Icon;

        cost.text = passiveData.SkillTreeSO.NextTierData.Requirements.CurrencyRequire.ToString();
        cost.gameObject.SetActive(true);

        description.text = passiveData.SkillTreeSO.Description;
        description.gameObject.SetActive(true);

        var passive = passiveData.SkillTreeSO.NextTierData;
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
            passiveData.SkillTreeSO.UnlockTier();
            passiveData.EnableYellowLine();
            Set(this, passiveData);
        }
    }
}
