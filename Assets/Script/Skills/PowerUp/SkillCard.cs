using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] CharacterData characterData;
    [SerializeField] List<SkillCardFrameWrapper> frameWrappers;
    [SerializeField] SkillTable skillTable;
    [SerializeField] Image cardIcon;
    [SerializeField] Image cardFrame;
    [SerializeField] Image skillRarityFrame;
    [SerializeField] Text description;
    [TextAreaAttribute]
    public string Description;
    private Dictionary<Rarity, SkillCardFrameWrapper> rarityToFrame;
    private PowerUps powerUp;
    private Skill skill;
    private Subject<Skill> onSkillChosen;
    public IObservable<Skill> OnSkillChosen=> onSkillChosen;

    public void Choose(PowerUps pu = null, Skill skill = null)
    {
        if (pu != null)
        {
            pu.Apply(characterData);
        }
        else
        {
            onSkillChosen.OnNext(skill);
        }
        transform.parent.gameObject.SetActive(false);
    }

    public void Set(PowerUps pu = null, Skill skill = null)
    {
        if(pu != null)
        {
            powerUp = pu;
            cardIcon.sprite = pu.Icon;
            cardFrame.sprite = rarityToFrame[pu.Rarity].CardFrame;
            skillRarityFrame.enabled = true;
            skillRarityFrame.sprite = rarityToFrame[pu.Rarity].SkillFrame;
            description.text = pu.GetDescription();
        }
        if(skill != null)
        {
            skillRarityFrame.enabled = false;
            this.skill = skill;
            cardIcon.sprite = skill.SkillData.Icon;
            description.text = skill.Description;
        }
    }
    private void Awake()
    {
    }

    public void Initiate()
    {
        onSkillChosen = new Subject<Skill>();
        button.OnClickAsObservable().Subscribe(_ => Choose(powerUp, skill));
        rarityToFrame = frameWrappers.ToDictionary(x => x.Rarity, x => x);
    }

}
[Serializable]
public class SkillCardFrameWrapper
{
    [SerializeField] Sprite cardFrame;
    [SerializeField] Sprite skillFrame;
    [SerializeField] Rarity rarity;
    public Rarity Rarity => rarity;
    public Sprite CardFrame => cardFrame;
    public Sprite SkillFrame => skillFrame;
}
