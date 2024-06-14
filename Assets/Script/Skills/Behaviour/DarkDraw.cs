using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class DarkDraw : Skill, IArea, ICastOnPlayer
{
    [SerializeField] BoxCollider hitBox;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject drawEffect;
    [SerializeField] private Rigidbody body;

    private float areaMod;
    public float AreaModifier { get => areaMod; set => areaMod.MultiplyValue(value); }

    public Collider DamageArea => hitBox;

    public Transform Player => player;

    protected override void Initiate()
    {
        base.Initiate();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player);
    }

    protected override void SkillBehavior()
    {
        var skillObj = Instantiate(drawEffect, this.transform);
        skillObj.SetActive(true);

        Vector3 worldCenter = hitBox.transform.TransformPoint(hitBox.center);
        Vector3 worldHalfExtents = Vector3.Scale(hitBox.size, hitBox.transform.lossyScale) * 0.5f;
        var enemiesHit = Physics.OverlapBox(worldCenter, worldHalfExtents, Quaternion.identity, LayerMask.GetMask("Enemy"));
        foreach(var enemy in enemiesHit)
        {
            var eff = Instantiate(hitEffect, enemy.transform);
            eff.SetActive(true);    
            DealDamage(enemy.GetComponent<HealthController>());
        }
        Destroy(skillObj, .5f);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        body.MovePosition(player.position);
        body.MoveRotation(Quaternion.LookRotation(player.forward));
    }
}
