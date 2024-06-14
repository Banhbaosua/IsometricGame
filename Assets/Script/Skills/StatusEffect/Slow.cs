using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow", menuName = "StatusEffect/Slow")]
public class Slow : StatusEffect
{
    [SerializeField] float slowAmount;

    protected override void StatusStart(IEffectable objApplyTo)
    {
        objApplyTo.SetModifiedSpeed(-slowAmount);
    }

    protected override void StatusStop(IEffectable objApplyTo)
    {
        objApplyTo.SetModifiedSpeed(0f);
    }
}
