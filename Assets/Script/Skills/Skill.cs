using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using System;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private SkillData skillData;
    [SerializeField] protected CharacterData characterData;
    [SerializeField] protected SkillTable inventory;

    private float coolDown;
    private float damage;
    private float castFreq;
    private float timer;
    private bool inCooldown;
    protected Transform player;

    public SkillData SkillData => skillData;
    public string Description => skillData.Description;
    public Sprite Icon => skillData.Icon;
    public float Timer => timer;

    public float SkillCoolDown 
    { 
        get => coolDown * (1 + castFreq);
        set
        { 
            coolDown.MultiplyValue(1/value);
            if (coolDown < 0.2f)
                coolDown = 0.2f;
        } 
    }
    public float Damage { get => damage; set => damage.MultiplyValue(value); }
    public float CastFreq {  get => castFreq + characterData.CastFrequencyModifier; set => castFreq.AddFloatValue(value); }

    public GameObject InstantiateSkill(Transform parent)
    {
        return Instantiate(skillData.Prefab, parent);
    }
    private IEnumerator Execute()
    {
        while (true)
        {
            SkillBehavior();
            timer = SkillCoolDown;
            inCooldown = true;
            yield return new WaitForSeconds(SkillCoolDown);
        }
    }

    private void Update()
    {
        if(inCooldown)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
                inCooldown = false;
        }
    }

    protected virtual void SingleCast()
    {

    }

    protected abstract void SkillBehavior();

    protected virtual void Initiate()
    {
        coolDown = skillData.BaseCoolDown;
        damage = skillData.BaseDamage;
        castFreq = 0f;
        SkillCoolDown = coolDown/(1+(float)characterData.CastFrequencyModifier);
        Damage = (float)characterData.DamageModifier;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected virtual void DealDamage(HealthController healthCtl)
    {
        healthCtl.TakeDamage(Damage);
    }

    protected virtual void DestroyAfterTime(GameObject skillObject, float time)
    {
        var timer = Observable.Interval(TimeSpan.FromSeconds(time))
                                .Subscribe(_ => Destroy(skillObject)).AddTo(skillObject);
    }

    protected virtual void Start()
    {
        Initiate();
        StartCoroutine(Execute());
    }
}
