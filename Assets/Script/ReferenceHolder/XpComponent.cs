using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpComponent : MonoBehaviour
{
    [SerializeField] FloatVariable xp;

    public void IncreaseXP(float value)
    {
        xp.RPValue.Value += value;
    }
}
