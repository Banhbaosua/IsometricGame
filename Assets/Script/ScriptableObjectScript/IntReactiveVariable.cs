using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "SOVariable", menuName = "SOVariable/IntVariable")]
public class IntReactiveVariable : ScriptableObject
{
    public ReactiveProperty<int> rp;
}
