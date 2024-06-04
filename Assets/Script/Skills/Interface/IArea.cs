using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArea 
{
    public float AreaModifier { get; set; }
    public Collider DamageArea { get; }
}
