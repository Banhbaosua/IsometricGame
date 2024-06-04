using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDPS : MonoBehaviour
{
    [SerializeField] DotEffectData dotData;
    private IDamagable damagable;
    private int timeCounter;
    public DotEffectData DotEffectData 
    { 
        get 
        { return dotData; }
        set
        { 
            dotData = value;
        }
    }
    private void Start()
    {
        damagable = GetComponent<IDamagable>();
        timeCounter = 0;
        StartCoroutine(ApplyDOT());
    }

    IEnumerator ApplyDOT()
    {
        yield return new WaitForSeconds(dotData.DamageInterVal);
        damagable.TakeDamage(dotData.DamagePertick);
        timeCounter++;
        if (timeCounter < dotData.Duration)
            Destroy(this);

    }
}
