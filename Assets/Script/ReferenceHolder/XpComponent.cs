using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpComponent : MonoBehaviour
{
    [SerializeField] FloatReactiveVariable xp;

    public void IncreaseXP(float value)
    {
        xp.rp.Value += value;
    }
}
