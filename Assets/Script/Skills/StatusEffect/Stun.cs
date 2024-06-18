using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stun", menuName = "StatusEffect/Stun")]
public class Stun : StatusEffect
{
    protected override void StatusStart(IEffectable objApplyTo)
    {
        objApplyTo.Stun(true);
    }

    protected override void StatusStop(IEffectable objApplyTo)
    {
        objApplyTo.Stun(false);
    }
}
