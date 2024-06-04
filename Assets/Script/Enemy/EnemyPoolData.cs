using SpawnerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnerSystem
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy")]
    public class EnemyPoolData : ScriptableObject
    {
        [SerializeField] EnemyType type;
        [SerializeField] int poolAmount;
        [SerializeField] GameObject prefab;
        [SerializeField] int maxActiveUnit;
        [SerializeField] float spawnDelay;
        public string MonsterName => prefab.name;
        public EnemyType Type => type;
        public int Amount => poolAmount;
        public GameObject Prefab => prefab;
        public int MaxActiveUnit => maxActiveUnit;
        public float SpawnDelay => spawnDelay;
    }
}
