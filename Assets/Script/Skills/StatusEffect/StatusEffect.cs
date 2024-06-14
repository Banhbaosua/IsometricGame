using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "StatusEffect", menuName = "StatusEffect/Data")]
public abstract class StatusEffect : ScriptableObject
{
    [SerializeField] float duration;
    [SerializeField] float damage;
    [SerializeField] float interval;
    [SerializeField] Transform statusVfx;
    private IDisposable timerDisposable;
    private Dictionary<IEffectable, bool> effectState;
    public Dictionary<IEffectable, bool> EffectState => effectState;
    public virtual void Apply(IEffectable objApplyTo)
    {
        if (!effectState.ContainsKey(objApplyTo))
        {
            effectState.Add(objApplyTo, true);
        }
        else
        {
            if (effectState[objApplyTo] == false)
            {
                effectState[objApplyTo] = true;
            }
            else
                return;
        }

        StatusStart(objApplyTo);

        timerDisposable = Observable.Timer(TimeSpan.FromSeconds(duration))
            .Subscribe(_ =>
            {
                StatusStop(objApplyTo);
                effectState[objApplyTo] = false;
            });
    }

    protected abstract void StatusStart(IEffectable objApplyTo);
    protected abstract void StatusStop(IEffectable objApplyTo);
    public virtual void Initiate()
    {
        effectState = new Dictionary<IEffectable, bool>();
    }
    private void OnDestroy()
    {
        timerDisposable?.Dispose();
        timerDisposable = null;
    }
}
