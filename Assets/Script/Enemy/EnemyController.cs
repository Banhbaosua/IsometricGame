using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEditor.Rendering;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using UniRx;
using System;

public enum EnemyState
{
    idle,
    chase,
    attack,
    death,
}

public class Enemy
{
    public StateEvent OnUpdate;
    public StateEvent<bool> OnDeath;
    public StateEvent OnHit;
}
[RequireComponent(typeof(HealthController))]
public class EnemyController : MonoBehaviour, IEffectable
{
    StateMachine<EnemyState, Enemy> enemyFSM;

    private HealthController healthController;
    private Transform target;
    private NavMeshAgent navMeshAgent;
    private Vector3 agentVel;

    [SerializeField] float attackDistance;
    [SerializeField] Animator animator;
    [SerializeField] float timeBeforAttackExec;
    [SerializeField] float attackCoolDown;
    [SerializeField] Collider attackBox;
    [SerializeField] float attackDamage;

    [SerializeField] float xp;

    private float moveSpeed;
    private ReactiveProperty<float> modifiedSpeed;
    private float speed => moveSpeed + modifiedSpeed.Value;
    private Subject<Unit> onEnemyDeath;
    private Subject<Unit> onEnemySpawn;
    private Subject<Unit> onAttack;
    public IObservable<Unit> OnEnemyDeath => onEnemyDeath;
    public IObservable<Unit> OnEnemySpawn => onEnemySpawn;
    public IObservable<Unit> OnAttack => onAttack;
    public float Xp => xp;
    private void Awake()
    {
        onEnemyDeath = new Subject<Unit>();
        onEnemySpawn = new Subject<Unit>();
        onAttack = new Subject<Unit>();
        OnEnemyDeath.Subscribe(_ => ResetStat());
        OnEnemyDeath.Subscribe(_ =>
        {
            Observable.Timer(TimeSpan.FromSeconds(3))
            .Subscribe(_ => this.transform.parent.gameObject.SetActive(false));
        });

        OnEnemySpawn.Subscribe(_ =>
        {
            SetTarget();
            Initiate();
        });

        
        Initiate();
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        enemyFSM.Driver.OnUpdate.Invoke();
    }

    void Initiate()
    {
        enemyFSM = new StateMachine<EnemyState, Enemy>(this);
        enemyFSM.ChangeState(EnemyState.chase);

        healthController = GetComponent<HealthController>();
        healthController.MaxHealthModify();
        healthController.OnDeath += enemyFSM.Driver.OnDeath.Invoke;

        navMeshAgent = GetComponent<NavMeshAgent>();
        moveSpeed = navMeshAgent.speed;
        modifiedSpeed = new ReactiveProperty<float>();
        modifiedSpeed.Subscribe(_ => navMeshAgent.speed = speed);
    }

    void idle_Enter()
    {
        Debug.Log("idle enter");
    }
    void idle_OnUpdate()
    {
        Debug.Log("idle");
        animator.SetFloat("Forward", 0);
    }

    void idle_Exit()
    {
        EnemyMovementHandle(false);
    }

    void chase_OnUpdate()
    {
        EnemyMovementHandle(false);
    }

    void chase_OnDeath(bool value)
    {
        if(value)
        {
            enemyFSM.ChangeState(EnemyState.death);
        }
    }

    void attack_Enter()
    {
        StartCoroutine(EnemyAttackHandle());
    }

    void attack_Exit()
    {
        StopAllCoroutines();
        navMeshAgent.isStopped = false;
    }
    void attack_OnUpdate()
    {
        if ((transform.position - target.position).sqrMagnitude > attackDistance * attackDistance)
        {
            StartCoroutine(AttackToChase());
        }
        EnemyMovementHandle(true);
    }

    void attack_OnDeath(bool value)
    {
        if(value)
        {
            enemyFSM.ChangeState(EnemyState.death);
        }
    }

    void death_Enter()
    {
        navMeshAgent.isStopped = true;
        healthController.OnDeath -= enemyFSM.Driver.OnDeath.Invoke;
        animator.SetFloat("Forward", 0);
        animator.SetTrigger("Death");
        animator.SetFloat("RandomDeath", UnityEngine.Random.Range(0, 2));
        onEnemyDeath.OnNext(Unit.Default);
    }

    void EnemyMovementHandle(bool isAttacking)
    {
        transform.LookAt(target);
        navMeshAgent.SetDestination(target.position);
        if ((transform.position - target.position).sqrMagnitude < attackDistance * attackDistance)
        {
            navMeshAgent.isStopped = true;

            enemyFSM.ChangeState(EnemyState.attack);
        }
        if (isAttacking)
        {
            animator.SetFloat("Forward", 0);
            return;
        }

        animator.SetFloat("Forward", (transform.position - target.position).magnitude, 0.1f, Time.deltaTime);
    }

    IEnumerator EnemyAttackHandle()
    {
        while (true)
        {
            animator.SetTrigger("Attack");
            animator.SetTrigger("ExecuteAttack");
            onAttack.OnNext(Unit.Default);
            yield return new WaitForSeconds(attackCoolDown);
        }
    }

    IEnumerator AttackToChase()
    {
        yield return new WaitForSeconds(0.5f);
        enemyFSM.ChangeState(EnemyState.chase);
    }

    void EnableAttHitBox()
    {
        attackBox.enabled = true;
    }

    void DisableAttHitBox()
    {
        attackBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<CharacterDataComponent>();
        if (player != null) 
        {
            player.CharacterData.HealthController.TakeDamage(attackDamage);
        }
    }

    void ResetStat()
    {

    }

    void SetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Spawned()
    {
        onEnemySpawn.OnNext(Unit.Default);
    }

    public void SpawnXpGem(GameObject XpGem)
    {
        var pos = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        XpGem.transform.position = pos;
        XpGem.SetActive(true);
    }

    public void SetModifiedSpeed(float speed)
    {
        modifiedSpeed.Value = navMeshAgent.speed * (speed / 100);
    }

    public void Stun(bool stun)
    {
        if (stun)
        {
            enemyFSM.ChangeState(EnemyState.idle);
            navMeshAgent.isStopped =true;
        }
        else
        {
            enemyFSM.ChangeState(EnemyState.chase);
            navMeshAgent.isStopped = false;
        }
    }
}
