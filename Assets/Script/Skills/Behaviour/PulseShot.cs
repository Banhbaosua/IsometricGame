using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PulseShot : Skill, ICastOnPlayer
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform pulseCore;
    [SerializeField] Transform pulseExplode;
    [SerializeField] SphereCollider explodeRadius;
    [SerializeField] SphereCollider hitCollider;
    public Transform Player => player;
    private Vector3 ObjectSpawnPos => new Vector3(player.transform.position.x, 1, player.transform.position.z);
    protected override void Initiate()
    {
        base.Initiate();
        transform.forward = player.forward;
    }

    protected override void SkillBehavior()
    {
        GameObject pulseShot = Instantiate(pulseCore.gameObject, ObjectSpawnPos, Quaternion.identity, this.transform);
        GameObject pulseExplodeGO = Instantiate(pulseExplode.gameObject, pulseShot.transform.position, Quaternion.identity, this.transform);
        
        DestroyAfterTime(pulseShot, 5);

        pulseShot.SetActive(true);
        pulseShot.transform.forward = player.forward;
        var moveStream = Observable.EveryFixedUpdate()
            .Select(_ => pulseShot)
            .Subscribe(x => x.transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward)).AddTo(pulseShot);

        pulseShot.GetComponent<SphereCollider>().OnTriggerEnterAsObservable()
            .FirstOrDefault()
            .Select(_ => pulseShot)
            .Subscribe(x =>
            {
                Destroy(x);
                pulseExplodeGO.transform.position = x.transform.position;
                pulseExplodeGO.SetActive(true);

                var enemies = Physics.OverlapSphere(x.transform.position,explodeRadius.radius, LayerMask.GetMask("Enemy"));
                Debug.Log(enemies.Length);
                foreach(var enemy in enemies)
                {
                    DealDamage(enemy.transform.GetComponent<HealthController>());
                }
                Destroy(pulseExplodeGO,1f);
            }).AddTo(pulseExplodeGO);
    }

    protected override void Start()
    {
        base.Start();
    }
}
