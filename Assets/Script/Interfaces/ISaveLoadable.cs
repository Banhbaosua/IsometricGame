using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveLoadable
{
    public void Save(string fileName);
    public void Load();
}
