using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionCutter : Skill, IArea, IFollowCursor, ICastOnPlayer
{
    [SerializeField] float areaModifier;
    [SerializeField] Collider damageArea;
    [SerializeField] MultipleObjectsMake objectMaker;
    [SerializeField] ObjectMove objectMove;
    public float AreaModifier { get => areaModifier; set => areaModifier.MultiplyValue(value); }

    public Collider DamageArea => damageArea;

    public Transform Player => player;

    protected override void DealDamage(HealthController healthCtl)
    {
        base.DealDamage(healthCtl);
    }

    protected override void Initiate()
    {
        base.Initiate();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.forward = player.forward;
    }

    protected override void SkillBehavior()
    {
        base.SkillBehavior();
        objectMaker.transform.position = player.transform.position;
        objectMaker.transform.forward = player.transform.forward;
        objectMaker.m_makeCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        objectMove.HitObj(other.transform);
        DealDamage(other.GetComponent<HealthController>());
    }

    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {

    }
}
