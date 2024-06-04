using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace SpawnerSystem
{
    public enum EnemyType
    {
        Normal,
        Elite,
        Boss,
    }
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] List<EnemyPoolData> poolData;
        [SerializeField] GameObject expGemPref;
        [SerializeField] Transform gemPoolParent;
        [SerializeField] Transform leftMap;
        [SerializeField] Transform rightMap;
        [SerializeField] Transform topMap;
        [SerializeField] Transform bottomMap;
        private Subject<string> onEnemySpawnReQuest;
        private Dictionary<string, PoolActiveData> enemies;
        private Queue<GameObject> expGemPool;
        private int requiredExpGemAmount;
        public IObservable<string> OnEnemySpawnRequest => onEnemySpawnReQuest;

        private const float CAMERADISTANCE = 15f;
        private const int GEMAMOUNTIFINSUFFIC = 10;
        private Vector3[] outsideCamPos;

        CompositeDisposable disposables = new CompositeDisposable();

        private void Awake()
        {
            onEnemySpawnReQuest = new Subject<string>();
            OnEnemySpawnRequest.Subscribe(enemy => SpawnEnemy(enemy));
            enemies = new Dictionary<string, PoolActiveData>();
            expGemPool = new Queue<GameObject>();

            outsideCamPos = new Vector3[]
        {
            new Vector3(-0.1f,UnityEngine.Random.Range(-0.1f,1.1f),CAMERADISTANCE),
            new Vector3(1.1f, UnityEngine.Random.Range(-0.1f,1.1f),CAMERADISTANCE),
            new Vector3(UnityEngine.Random.Range(-0.1f,1.1f),-0.1f,CAMERADISTANCE),
            new Vector3(UnityEngine.Random.Range(-0.1f,1.1f),1.1f,CAMERADISTANCE),
        };
    }
        private void Start()
        {
            InitiatePool();
        }

        public void SpawnEnemy(string monsterName)
        {
            Vector3 pos = Camera.main.ViewportToWorldPoint(outsideCamPos[UnityEngine.Random.Range(0, 3)]);
            pos.y = 0;
            pos = CheckMapLimit(pos);
            var enemy = enemies[monsterName].enemiesList.Dequeue();
            enemy.GetComponentInChildren<EnemyController>().Spawned();
            enemies[monsterName].currentActiveMonster++;
            enemy.transform.position = pos;
            enemy.gameObject.SetActive(true);
        }

        Vector3 CheckMapLimit(Vector3 pos)
        {
            if (pos.x < leftMap.position.x)
                pos.x = leftMap.position.x + 0.1f;
            if (pos.x > rightMap.position.x)
                pos.x = rightMap.position.x - 0.1f;
            if (pos.y > topMap.position.y)
                pos.y = topMap.position.y - 0.1f;
            if (pos.y < bottomMap.position.y)
                pos.y = bottomMap.position.y + 0.1f;
            return pos;
        }

        public void ReturnEnemy(GameObject enemy) 
        {
            enemies[enemy.name].enemiesList.Enqueue(enemy);
        }

        void ReturnGem(GameObject gem)
        {
            expGemPool.Enqueue(gem);
        }
        GameObject DequeueXpGem(float xp)
        {
            if(expGemPool.Count > 0) 
            { 
                var gem = expGemPool.Dequeue();
                gem.GetComponent<Gem>().SetXp(xp);

                return gem;
            }
            else
            {
                GenerateXpGem(GEMAMOUNTIFINSUFFIC, gemPoolParent);
                var gem = expGemPool.Dequeue();
                gem.GetComponent<Gem>().SetXp(xp);

                return gem;
            }
        }

        void InitiatePool()
        {
            foreach (EnemyPoolData data in poolData)
            {
                var enemyList = new Queue<GameObject>();
                var objParent = new GameObject(data.MonsterName);
                objParent.transform.parent = this.transform;

                for (int i = 0; i < data.Amount; i++)
                {
                    var enemy = Instantiate(data.Prefab, objParent.transform);
                    enemy.SetActive(false);
                    var controller = enemy.GetComponentInChildren<EnemyController>();
                    //enemy.name = enemy.name.Replace("(Clone)", "").Trim();
                    controller.OnEnemyDeath
                        .Subscribe(_ =>
                        {
                            enemies[data.MonsterName].currentActiveMonster--;
                            controller.SpawnXpGem(DequeueXpGem(controller.Xp));
                            ReturnEnemy(enemy);
                        });

                    enemyList.Enqueue(enemy);
                }

                enemies.Add(data.MonsterName, new PoolActiveData(0, enemyList, data));
                requiredExpGemAmount += data.MaxActiveUnit;
            }
            GenerateXpGem(requiredExpGemAmount, gemPoolParent.transform);

            foreach (KeyValuePair<string, PoolActiveData> keyValuePair in enemies)
            {
                var enemySpawnStream = Observable.Interval(TimeSpan.FromSeconds(keyValuePair.Value.data.SpawnDelay))
                    .Select(actives => keyValuePair.Value.currentActiveMonster)
                    .Where(actives => actives < keyValuePair.Value.data.MaxActiveUnit)
                    .Subscribe(_ => onEnemySpawnReQuest.OnNext(keyValuePair.Key))
                    .AddTo(disposables);
            }
        }

        void GenerateXpGem(int amount, Transform parent)
        {
            for (int i = 0; i < amount; i++)
            {
                var xpGem = Instantiate(expGemPref, parent.transform);
                var gemComp = xpGem.GetComponent<Gem>();
                gemComp.GetComponent<Gem>().Initiate();
                gemComp.GetComponent<Gem>().OnGameCollect.Subscribe(_ => ReturnGem(xpGem));
                expGemPool.Enqueue(xpGem);
            }
        }
    }

    class PoolActiveData
    {
        public EnemyPoolData data;
        public int currentActiveMonster;
        public Queue<GameObject> enemiesList;
        
        public PoolActiveData(int currentActiveMonster, Queue<GameObject> queue, EnemyPoolData data)
        {
            this.currentActiveMonster = currentActiveMonster;
            enemiesList = queue;
            this.data = data;
        }
    }
    
}
