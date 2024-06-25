using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyCurveProjectile : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] Transform launchPoint;
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform rangeCircle;
    
    private Transform target;
    public float firingAngle = 45.0f;
    public float gravity;



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyController.OnAttack.Subscribe(_ => LaunchProjectile());
    }

    void LaunchProjectile()
    {
        var projectile = Instantiate(Projectile,launchPoint.position,Quaternion.identity,null);
        
        StartCoroutine(SimulateProjectile(projectile.transform));
    }
    IEnumerator SimulateProjectile(Transform proj)
    {
        proj.gameObject.SetActive(true);
        var circle = Instantiate(rangeCircle.gameObject, target.position, Quaternion.Euler(90,0,0), null);

        var circleFill = circle.transform.GetChild(0);
        var circleFillMaxScale = circleFill.localScale;

        circle.SetActive(true);
        proj.gameObject.SetActive(true);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(proj.position, target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        proj.rotation = Quaternion.LookRotation(target.position - proj.position);

        float elapse_time = 0;
        circleFill.localScale = Vector3.zero;

        while (elapse_time < flightDuration)
        {
            proj.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;
            circleFill.localScale = circleFillMaxScale * (elapse_time / flightDuration);
            if (elapse_time >= flightDuration)
            {
                Destroy(proj.gameObject);
                Destroy(circle.gameObject);
            }
            yield return null;
        }
    }


}




