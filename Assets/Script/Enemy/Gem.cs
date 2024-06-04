using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] XpComponent xpComp;
    [SerializeField] float flyDampTime;

    Vector3 _velo = Vector3.zero;
    private const float DISTANCETOGIVEXP = 1f;
    Vector3 _velocity = Vector3.zero;
    private Subject<Unit> onGameCollect;
    public IObservable<Unit> OnGameCollect => onGameCollect;
    private float xp;
    public float Xp => xp;
    public void SetXp(float xp)
    {
        this.xp = xp;
    }
    public void Initiate()
    {
        onGameCollect = new Subject<Unit>();
        xpComp = GetComponent<XpComponent>();
    }
    
    public void FlyTowardPlayer(Transform player)
    {
        StartCoroutine(FlyBehaviour(player));
    }

    IEnumerator FlyBehaviour(Transform player)
    {
        while (true) 
        {
            yield return new WaitForEndOfFrame();
            var pos = new Vector3(player.position.x, 1f, player.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, pos,ref _velo,Time.deltaTime*flyDampTime);
            float squaredDistance = Vector3.SqrMagnitude(transform.position - pos);
            if (squaredDistance < DISTANCETOGIVEXP*DISTANCETOGIVEXP)
            {
                xpComp.IncreaseXP(xp);
                StopCoroutine(nameof(FlyBehaviour));
                this.gameObject.SetActive(false);
                onGameCollect.OnNext(Unit.Default);
            }
        }
    }
}
