using SpawnerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpawnerSystem
{
    [Serializable]
    public class EnemyPoolData
    {
        [SerializeField] EnemyType type;
        [SerializeField] int poolAmount;
        [SerializeField] GameObject prefab;
        [SerializeField] int maxActiveUnit;
        [SerializeField] float spawnDelay;
        [SerializeField] List<EnemyWave> waves;
        private int _maxActiveUnit;
        public int MaxActiveUnit => _maxActiveUnit;
        private int currentWave = 0;
        public string MonsterName => prefab.name;
        public EnemyType Type => type;
        public int Amount => poolAmount;
        public GameObject Prefab => prefab;
        public float SpawnDelay => spawnDelay;
        public int CurrentWave => currentWave;
        
        public List<EnemyWave> Waves => waves;
        public void Initiate()
        {
            currentWave = 0;
            _maxActiveUnit = maxActiveUnit;
        }
        void StartWave(EnemyWave wave, PoolActiveData activeData)
        {
            _maxActiveUnit += wave.AddictiveEnemy;
            if(wave.HealthModify > 0)
            {
                for(int i = 0; i < activeData.healthScriptList.Count;i++) 
                {
                    activeData.healthScriptList[i].HealthMultiModify(wave.HealthModify);
                }
            }
            currentWave++;
        }
        public static IEnumerator WavesTrigger(PoolActiveData activeData)
        {
            var data = activeData.data;
            data.Initiate();
            if (data.Waves.Count == 0)
                yield return null;

            while (data.Waves.Count > data.CurrentWave)
            {
                yield return new WaitForSeconds(1f);

                float currentTime = Time.time;

                if (currentTime > data.Waves[data.CurrentWave].TriggerTime)
                {
                    var wave = data.Waves[data.CurrentWave];
                    data.StartWave(wave, activeData);
                    Debug.Log(data.MaxActiveUnit);
                }
            }
        }
    }

    [Serializable]
    public class EnemyWave
    {
        [SerializeField] float triggerTime;
        [SerializeField] int addictiveEnemy;
        [SerializeField] float healthModify;

        public int AddictiveEnemy => addictiveEnemy;
        public float TriggerTime => triggerTime;
        public float HealthModify => healthModify;
    }
}
