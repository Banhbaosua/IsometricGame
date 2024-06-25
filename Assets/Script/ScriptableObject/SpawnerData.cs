using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpawnerSystem;

[CreateAssetMenu(fileName = "LevelSetting",menuName ="Setting/LevelSetting")]
public class SpawnerData : ScriptableObject
{
    [SerializeField] List<EnemyPoolData> list;
    public List<EnemyPoolData> List => list;
    [SerializeField] List<EnemyWave> waves;
}

[System.Serializable]
public class EnemyWave
{
    [SerializeField] float waveDuration;
    [SerializeField] List<EnemyPoolData> enemyList;
}

