using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdjustable<T>
{
    public void AddValue(T value);
    public void SaveValue();
}
