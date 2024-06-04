using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBurstController : Skill, IDuration, IArea,ICastOnArea
{
    [SerializeField] float duration;
    [SerializeField] float areaMod;
    [SerializeField] Collider damageArea;
    [SerializeField] SphereCollider castArea;
    [SerializeField] MultipleObjectsMake coremaker;
    [SerializeField] MultipleObjectsMake impactMaker;
    [SerializeField] Transform maker;
    [SerializeField] float maxEntity;
    private Transform player;
    private Transform attachedEnemy;
    private List<Transform> attachedEnemyList;
    private Collider[] enemies = new Collider[0];
    private List<Transform> Entities;
    public float Duration { get => duration; set => duration = value; }
    public float AreaModifier 
    { 
        get => areaMod + characterData.AreaModifier; 
        set => areaMod.MultiplyValue(value); }

    public Collider DamageArea => damageArea;

    public Collider CastArea => castArea;

    public Transform Player  => player;

    public Transform AttachEnemy => attachedEnemy;

    protected override void DealDamage(HealthController healthCtl)
    {
        base.DealDamage(healthCtl);
    }

    protected override void Initiate()
    {
        base.Initiate();
        impactMaker.m_makeDelay = impactMaker.m_makeDelay/(1+characterData.CastFrequencyModifier/100);
        impactMaker.m_makeCount = Mathf.CeilToInt(duration/impactMaker.m_makeDelay);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attachedEnemyList = new List<Transform>();
        Entities = new List<Transform>();
    }

    protected override void SkillBehavior()
    {
        base.SkillBehavior();

        StartCoroutine(FindEnemy());
    }

    IEnumerator FindEnemy()
    {
        if (Entities.Count < maxEntity)
        {
            bool foundEnemy = false;
            while (!foundEnemy)
            {
                yield return new WaitForFixedUpdate();
                if (enemies.Length == 0)
                    continue;

                var random = Random.Range(0, enemies.Length - 1);

                if (attachedEnemyList.Contains(enemies[random].transform))
                    continue;

                attachedEnemy = enemies[random].transform;
                attachedEnemyList.Add(enemies[random].transform);

                var gameObj = Instantiate(maker.gameObject, this.transform);
                gameObj.SetActive(true);
                Entities.Add(gameObj.transform);
                StopCoroutine(FindEnemy());
            }
        }
    }

    public void RemoveAttachEnemy(Transform attachedEnemy)
    {
        attachedEnemyList.Remove(attachedEnemy);
    }
    
    public void RemoveEntity(Transform entity)
    {
        Entities.Remove(entity);
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        this.transform.position = player.position;
        if (attachedEnemy != null)
            this.transform.position = attachedEnemy.position;
    }
    private void FixedUpdate()
    {
        enemies = Physics.OverlapSphere(player.position, castArea.radius, LayerMask.GetMask("Enemy"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.GetMask("Enemy"))
            Debug.Log("hit");
    }
}
