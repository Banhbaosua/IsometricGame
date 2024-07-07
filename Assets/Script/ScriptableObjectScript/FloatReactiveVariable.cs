using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[CreateAssetMenu(fileName = "SOVariable", menuName ="SOVariable/FloatVariable")]
public class FloatReactiveVariable : ScriptableObject
{
    public ReactiveProperty<float> RPValue;
}
