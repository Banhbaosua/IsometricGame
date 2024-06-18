using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System;
using static UnityEngine.Rendering.DebugUI;

public class IceField : Skill, ICastOnArea
{
    [SerializeField] SphereCollider castArea;
    [SerializeField] SphereCollider effectArea;
    [SerializeField] StatusEffectComponent statusEffectComponent;
    [SerializeField] float existInterval;
    [SerializeField] float slowApplyInterval;
    [SerializeField] int entities;
    [SerializeField] GameObject iceFieldGO;
    private int currentEntity = 0;
    public Transform Player => player;

    public Collider CastArea => castArea;

    protected override void SkillBehavior()
    {
        if (currentEntity < entities)
        {
            Debug.Log(currentEntity);
            var enemies = Physics.OverlapSphere(player.position, castArea.radius, LayerMask.GetMask("Enemy"));
            if (enemies.Length > 0)
            {
                var random = UnityEngine.Random.Range(0, enemies.Length-1);
                var pos = new Vector3(enemies[random].transform.position.x, 0.01f, enemies[random].transform.position.z);
                GameObject iceField = Instantiate(iceFieldGO, pos, Quaternion.identity, null);
                currentEntity++;
                iceField.SetActive(true);

                ApplySlow(iceField);

                var iceFieldDuration = Observable.Timer(TimeSpan.FromSeconds(existInterval))
                    .Subscribe(_ =>
                    {
                        Debug.Log("destroy");
                        currentEntity--;
                        Destroy(iceField);
                    });

                var iceFieldApplyStream = Observable.Interval(TimeSpan.FromSeconds(slowApplyInterval))
                    .Subscribe(_ =>
                    {
                        ApplySlow(iceField);
                    }).AddTo(iceField);
            }
        }
    }

    private void ApplySlow(GameObject iceField)
    {
        var effectEnemies = Physics.OverlapSphere(iceField.transform.position, effectArea.radius, LayerMask.GetMask("Enemy"));
        foreach (var ene in effectEnemies)
        {
            statusEffectComponent.ApplyStatus(ene.GetComponent<IEffectable>());
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
}
