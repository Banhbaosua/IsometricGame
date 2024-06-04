using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private SkillData skillData;
    [SerializeField] protected CharacterData characterData;

    private float coolDown;
    private float damage;
    private float castFreq;

    public float SkillCoolDown 
    { 
        get => coolDown;
        set
        { 
            coolDown.MultiplyValue(1/value);
            if (coolDown < 0.2f)
                coolDown = 0.2f;
        } 
    }
    public float Damage { get => damage; set => damage.MultiplyValue(value); }
    public float CastFreq {  get => castFreq + characterData.CastFrequencyModifier; set => castFreq.AddFloatValue(value); }
    private IEnumerator Execute()
    {
        while (true)
        {
            SkillBehavior();
            yield return new WaitForSeconds(SkillCoolDown*(1 + castFreq));
        }
    }

    protected virtual void SingleCast()
    {

    }

    protected virtual void SkillBehavior()
    {

    }

    protected virtual void Initiate()
    {
        coolDown = skillData.BaseCoolDown;
        damage = skillData.BaseDamage;
        castFreq = 0f;
        SkillCoolDown = coolDown/(1+(float)characterData.CastFrequencyModifier);
        Damage = (float)characterData.DamageModifier;
    }
    protected virtual void DealDamage(HealthController healthCtl)
    {
        healthCtl.TakeDamage(Damage);
    }

    protected virtual void Start()
    {
        Initiate();
        StartCoroutine(Execute());
    }
}
