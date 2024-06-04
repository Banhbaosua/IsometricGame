using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dot", menuName ="Data/DotData")]
public class DotEffectData : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] float dotDamage;
    [SerializeField] int   dotStacks;
    [SerializeField] float damageInterval;
    [SerializeField] float perTickDamagePercent;
    [SerializeField] float duration;

    public int DoTStacks
    {
        get { return dotStacks; }
        set
        {
            dotStacks += value;
        }
    }

    public float DamageInterVal => damageInterval;
    public float DamagePertick => dotDamage*perTickDamagePercent;
    public float Duration => duration;
}
