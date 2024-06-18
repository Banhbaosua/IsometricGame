using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
[RequireComponent(typeof(StatusEffectComponent))]
public class LightningField : Skill, ICastOnArea
{
    [SerializeField] SphereCollider castArea;
    [SerializeField] SphereCollider skillEffectArea;
    [SerializeField] GameObject skillPrefab;
    [SerializeField] StatusEffectComponent statusEffectComponent;

    private Collider[] enemies = new Collider[0];
    public Transform Player => player;

    public Collider CastArea => castArea;

    protected override void SkillBehavior()
    {
        StartCoroutine(SpawnSkillObject(player.transform,castArea.radius));
    }

    Collider[] FindEnemies(Transform position,float radius)
    { 
        return enemies = Physics.OverlapSphere(position.position, radius, LayerMask.GetMask("Enemy"));
    }

    IEnumerator SpawnSkillObject(Transform position, float radius)
    {
        while(true)
        {
            yield return new WaitForFixedUpdate();
            enemies = FindEnemies(position, radius);
            if (enemies.Length > 0)
            {
                var rd = UnityEngine.Random.Range(0, enemies.Length - 1);
                GameObject lightningField = Instantiate(skillPrefab, enemies[rd].transform.position, Quaternion.identity, null);
                lightningField.SetActive(true);

                var effectEnemies = FindEnemies(lightningField.transform, skillEffectArea.radius);
                foreach (var enemies in effectEnemies)
                {
                    statusEffectComponent.ApplyStatus(enemies.transform.GetComponent<IEffectable>());
                }
                DestroyAfterTime(lightningField, 2f);
                break;
            }
        }
    }
}
