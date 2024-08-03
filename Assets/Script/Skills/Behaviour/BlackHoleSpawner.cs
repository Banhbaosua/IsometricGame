using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

public class BlackHoleSpawner : Skill, IArea, IDuration, ICastOnArea
{
    [SerializeField] private float duration;
    [SerializeField] private float areaMod;
    [SerializeField] private Collider damageArea;
    [SerializeField] private SphereCollider castArea;
    [SerializeField] GameObject prefab;
    [SerializeField] float pullForce;
    private Dictionary<GameObject,Dictionary<GameObject, EnemyController>> bhToEnemiesCtl;
    CompositeDisposable disposables;
    public float Duration { get => duration; set => duration.MultiplyValue(value); }
    public float AreaModifier { get => areaMod; set => areaMod.MultiplyValue(value); }
    public Collider DamageArea { get => damageArea;}

    public Collider CastArea => castArea;

    public Transform Player => player;

    protected override void DealDamage(HealthController healthCtl)
    {
        base.DealDamage(healthCtl);
    }

    protected override void Initiate()
    {
        base.Initiate();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bhToEnemiesCtl = new Dictionary<GameObject, Dictionary<GameObject, EnemyController>>();
        disposables = new CompositeDisposable();
    }

    protected override void SingleCast()
    {
        throw new System.NotImplementedException();
    }

    protected override void SkillBehavior()
    {
        var enemies = Physics.OverlapSphere(
            player.position, 
            castArea.radius, 
            LayerMask.GetMask("Enemy"));
        if (enemies.Length == 0)
            return;

        var random = UnityEngine.Random.Range(0, enemies.Length - 1);

        var blackHole = Instantiate(
            prefab, 
            enemies[random].transform.position ,
            Quaternion.identity ,
            this.transform);
        bhToEnemiesCtl[blackHole] = new Dictionary<GameObject, EnemyController>();
        blackHole.transform.position = new Vector3(
            blackHole.transform.position.x, 
            enemies[random].transform.position.y,
            blackHole.transform.position.z);

        var sphereCollider = blackHole.GetComponentInChildren<SphereCollider>();
        blackHole.SetActive(true);

        sphereCollider.OnTriggerStayAsObservable()
            .Where(other => other.attachedRigidbody)
            .Subscribe(other =>
            {
                if (!bhToEnemiesCtl[blackHole].ContainsKey(other.gameObject))
                {
                    var enemyctl = other.GetComponent<EnemyController>();
                    bhToEnemiesCtl[blackHole].Add(other.gameObject, enemyctl);
                }

                float gravityIntensity = Vector3.Distance(blackHole.transform.position, other.transform.position) / sphereCollider.radius;
                var direction = blackHole.transform.position - other.transform.position;
                direction = new Vector3(direction.x,other.transform.position.y,direction.z);
                bhToEnemiesCtl[blackHole][other.gameObject].DisableNavMesh();
                other.attachedRigidbody.AddForce(gravityIntensity * pullForce * Time.deltaTime * direction.normalized);
                bhToEnemiesCtl[blackHole][other.gameObject].EnableNavMesh();
            }).AddTo(blackHole);

        var liveTimeStream = Observable.Interval(TimeSpan.FromSeconds(duration))
            .Subscribe(_ => 
            {
                bhToEnemiesCtl.Remove(blackHole);
                Destroy(blackHole);
                disposables?.Dispose();
            }).AddTo(blackHole);
    }

    protected override void Start()
    {
        base.Start();
    }
}
