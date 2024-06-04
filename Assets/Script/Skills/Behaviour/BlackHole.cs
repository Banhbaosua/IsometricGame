using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Skill, IArea, IDuration, ICastOnArea
{
    [SerializeField] private float duration;
    [SerializeField] private float areaMod;
    [SerializeField] private Collider damageArea;
    [SerializeField] private Collider castArea;
    public float Duration { get => duration; set => duration.MultiplyValue(value); }
    public float AreaModifier { get => areaMod; set => areaMod.MultiplyValue(value); }
    public Collider DamageArea { get => damageArea;}

    public Collider CastArea => castArea;

    public Transform Player => throw new System.NotImplementedException();

    protected override void DealDamage(HealthController healthCtl)
    {
        base.DealDamage(healthCtl);
    }

    protected override void Initiate()
    {
        base.Initiate();
    }

    protected override void SingleCast()
    {
        throw new System.NotImplementedException();
    }

    protected override void SkillBehavior()
    {
        base.SkillBehavior();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
