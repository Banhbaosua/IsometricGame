using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBurst : MonoBehaviour
{
    [SerializeField] MultipleObjectsMake impactMaker;
    [SerializeField] SphereCollider dmgCollider;
    [SerializeField] SunBurstController sunBurstCtl;
    private float timer;
    private Transform enemy;
    Collider[] enemiesHit;

    public void SetEnemy(Transform enemy)
    {
        this.enemy = enemy;
    }
    IEnumerator Damage()
    {
        while (true)
        {
            enemiesHit = Physics.OverlapSphere(enemy.position, dmgCollider.radius, LayerMask.GetMask("Enemy"));
            foreach(Collider enemy in enemiesHit)
            {
                enemy.GetComponent<HealthController>().TakeDamage(sunBurstCtl.Damage);
            }
            yield return new WaitForSeconds(impactMaker.m_makeDelay);
        }
    }
    private void OnEnable()
    {
        timer = Time.time;
        enemy = sunBurstCtl.AttachEnemy;
        StartCoroutine(Damage());
    }

    private void Update()
    {
        this.transform.position = enemy.position;
        if (Time.time - timer > impactMaker.m_makeDelay * impactMaker.m_makeCount)
        {
            Destroy(this.gameObject);
            sunBurstCtl.RemoveAttachEnemy(enemy);
            sunBurstCtl.RemoveEntity(this.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            dmgCollider.enabled = false;
            Debug.Log("hit");
        }
    }
}
