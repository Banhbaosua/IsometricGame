using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICastOnArea 
{
    public Transform Player { get; }
    public Collider CastArea { get; }
}
