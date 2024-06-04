using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] CharacterData characterData;
    [SerializeField] Sprite common;
    [SerializeField] Sprite uncommon;
    [SerializeField] Sprite rare;
    [SerializeField] Sprite epic;
    [SerializeField] Sprite legendary;
    [SerializeField] SkillInventory skillInventory;
    [SerializeField] Sprite cardIcon;
    [TextAreaAttribute]
    public string Description;

    private PowerUps powerUp;
    private Skill skill;
    public PowerUps PowerUps => powerUp;

    public void Choose()
    {
        powerUp.Apply(characterData);
    }

    public void Set(PowerUps pu = null, Skill skill = null)
    {
        if(pu != null)
        {
            powerUp = pu;
            cardIcon = pu.Icon;
            Description = pu.Description;
        }
        if(skill != null)
        {
            this.skill = skill;
            cardIcon = skill.SkillData.Icon;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        button.OnClickAsObservable().Subscribe(_ => Choose());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
