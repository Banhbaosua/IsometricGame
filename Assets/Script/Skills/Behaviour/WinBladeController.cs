using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WinBladeController : Skill, ICastOnPlayer
{
    [SerializeField] SphereCollider chainRange;
    [SerializeField] GameObject wbPref;
    [SerializeField] int chainTimes;
    [SerializeField] float moveSpeed;

    private Vector3 objectSpawnPos => new Vector3(player.transform.position.x, 1, player.transform.position.z);
    public Transform Player => player;

    protected override void Initiate()
    {
        base.Initiate();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.forward = player.forward;
    }

    protected override void SkillBehavior()
    {
        base.SkillBehavior();
        GameObject windBlade = Instantiate(wbPref, objectSpawnPos, Quaternion.identity, this.transform);
        var timer = Observable.Interval(TimeSpan.FromSeconds(4))
                                .Subscribe(_ => Destroy(windBlade)).AddTo(windBlade);
        windBlade.transform.forward = player.forward;
        windBlade.SetActive(true);
        var windBladeMove = Observable.EveryFixedUpdate()
                                    .Select(x => windBlade)
                                    .Subscribe(x => x.transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward)).AddTo(windBlade);
        windBlade.GetComponent<SphereCollider>().OnTriggerEnterAsObservable()
            .FirstOrDefault()
            .Subscribe(x =>
            {
                windBladeMove.Dispose();
                DealDamage(x.GetComponent<HealthController>());
                var nearest = NearestEnemyList(chainTimes, x.transform);
                if (nearest.Length > 0)
                    FlyTowardNearest(windBlade.transform, nearest);
                else
                    Destroy(windBlade);
            }).AddTo(windBlade);
    }

    private void OnTriggerEnter(Collider other)
    {
        DealDamage(other.GetComponent<HealthController>());
    }

    Transform FindNearestEnemy(SphereCollider collider, Transform enemy, Transform[] enemyChecked = null)
    {
        Collider[] enemies = Physics.OverlapSphere(enemy.position, collider.radius, LayerMask.GetMask("Enemy"));
        enemies = enemies.Where(value => value != enemy.GetComponent<Collider>()).ToArray();
        if (enemyChecked != null)
        {
            List<Collider> enemyList = new List<Collider>(enemies);
            enemies = enemyList.Where(enemy => !enemyChecked.Contains(enemy.transform)).ToArray();
        }

        if (enemies.Length > 0)
        {
            Transform nearest = enemies[0].transform;
            foreach (Collider ene in enemies)
            {
                float squaredDistanceFromEnemy = Vector3.SqrMagnitude(enemy.position - ene.transform.position);
                float squaredDistanceFromCurrentNearest = Vector3.SqrMagnitude(enemy.position - nearest.position);
                if (squaredDistanceFromEnemy < squaredDistanceFromCurrentNearest)
                    nearest = ene.transform;
            }
            return nearest;
        }
        else
            return null;
    }

    Transform[] NearestEnemyList(int chainCount, Transform enemy)
    {
        var nearistEnemies = new List<Transform>();
        var currentEnemy = enemy;
        var enemyCheck = new List<Transform>();
        Transform nearist = null;
        enemyCheck.Add(enemy);
        for(int i = 0; i< chainCount; i++)
        {
            nearist = FindNearestEnemy(chainRange, currentEnemy,enemyCheck.ToArray());
            if (nearist == null)
                break;
            nearistEnemies.Add(nearist);
            enemyCheck.Add(nearist);
            currentEnemy = nearist;
        }

        return nearistEnemies.ToArray();
    }

    void FlyTowardNearest(Transform windBlade ,Transform[] enemy)
    {
        var moveStream = Observable.EveryFixedUpdate();
        
        if (enemy.Length > 0)
        {
            int currentIndex = 0;
            int chainCount = 0;
            if(enemy.Length < chainTimes)
                chainCount = enemy.Length;
            else
                chainCount = chainTimes;
            
            var moveSubscription = moveStream
                .Where(_ => currentIndex < chainCount)
                .Select(_ => new Vector3(enemy[currentIndex].position.x, 1, enemy[currentIndex].position.z))
                .Select(enemy => (enemy - windBlade.position).normalized)
                .Subscribe(direction =>
                {
                    windBlade.transform.forward = direction;
                    windBlade.transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);

                });

            //var hitCheck = obj.GetComponent<Collider>().OnTriggerEnterAsObservable()
            //    .Where(_ => currentIndex < chainCount)
            //    .Where(hitEnemy => hitEnemy.gameObject == enemy[currentIndex].gameObject)
            //    .Select(_ => obj)
            //    .Subscribe(_ =>
            //    {
            //        if (currentIndex < chainCount)
            //        {
            //            currentIndex++;
            //            if (currentIndex == chainCount)
            //                Destroy(obj.gameObject);
            //        }
            //    });
            var hitcheck = moveStream
                .Where(_ => currentIndex < chainCount)
                .Select(_ => windBlade)
                .Where(windBlade => Vector3.SqrMagnitude(windBlade.transform.position - enemy[currentIndex].transform.position) < 1f)
                .Subscribe(windBlade =>
                {
                    DealDamage(enemy[currentIndex].GetComponent<HealthController>());
                    if (currentIndex < chainCount)
                    {
                        currentIndex++;
                        if (currentIndex == chainCount)
                            Destroy(windBlade.gameObject);
                    }
                });

            windBlade.OnDestroyAsObservable().Subscribe(_ =>
            {
                moveSubscription?.Dispose();
            });
        }
    }
}
