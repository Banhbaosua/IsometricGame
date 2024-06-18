using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectable
{
    public void SetModifiedSpeed(float speed);
    public void Stun(bool stun);
}
