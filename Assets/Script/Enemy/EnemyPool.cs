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
        [SerializeField] CurrentMapLevelInfo currentMapLevelInfo;
        private SpawnerData poolData => currentMapLevelInfo.LevelSetting;
        [SerializeField] GameObject expGemPref;
        [SerializeField] Transform gemPoolParent;

        [SerializeField] ObjectiveManager objectiveManager;

        [Header("Map limit")]
        [SerializeField] Transform leftMap;
        [SerializeField] Transform rightMap;
        [SerializeField] Transform topMap;
        [SerializeField] Transform bottomMap;

        private Transform player;
        private Subject<string> onEnemySpawnReQuest;
        private Dictionary<string, PoolActiveData> enemies;
        private Queue<GameObject> expGemPool;
        private int requiredExpGemAmount;
        public IObservable<string> OnEnemySpawnRequest => onEnemySpawnReQuest;

        private const float CAMERADISTANCE = 15f;
        private const int GEMAMOUNTIFINSUFFIC = 10;
        private Vector3[] outsideCamPos;
        private Subject<Unit> onBossGemCollect;
        public Subject<Unit> OnBossGemCollect => onBossGemCollect;
        CompositeDisposable disposables = new CompositeDisposable();

        private void Awake()
        {
            
            onBossGemCollect = new Subject<Unit>();
            onEnemySpawnReQuest = new Subject<string>();
            OnEnemySpawnRequest.Subscribe(enemy => SpawnEnemy(enemy)).AddTo(this);

            
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
            player = GameObject.FindGameObjectWithTag("Player").transform;
            objectiveManager.spawnBossResponse.Subscribe(_ => SpawnEnemy("boss")).AddTo(this);
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
            enemy.SetActive(true);
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

        public void ReturnEnemy(GameObject enemy, bool isBoss = false) 
        {
            if (isBoss)
            {
                enemies["boss"].enemiesList.Enqueue(enemy);
                return;
            }
            enemies[enemy.name].enemiesList.Enqueue(enemy);
        }

        void ReturnGem(GameObject gem)
        {
            expGemPool.Enqueue(gem);
        }
        GameObject DequeueXpGem(float xp, bool isBossGem = false)
        {
            if(expGemPool.Count > 0) 
            { 
                var gem = expGemPool.Dequeue();
                gem.transform.localScale = new Vector3(1,1,1);
                var gemComp = gem.GetComponent<Gem>();
                gemComp.GetComponent<Gem>().SetXp(xp);
                if (!isBossGem)
                    OnBossGemCollect.Subscribe(_ =>
                    { 
                        if(gem.activeSelf)
                            gemComp.FlyTowardPlayer(player); 
                    }).AddTo(this);
                else
                {
                    gemComp.OnGameCollect.Subscribe(_ => onBossGemCollect.OnNext(Unit.Default)).AddTo(this);
                    gem.transform.localScale = gem.transform.localScale * 3;
                }
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
            foreach (EnemyPoolData data in poolData.List)
            {
                var enemyList = new Queue<GameObject>();
                var healthScriptList = new List<HealthController>();
                var enemiesController = new List<EnemyController>();
                var objParent = new GameObject(data.MonsterName);
                objParent.transform.parent = this.transform;

                for (int i = 0; i < data.Amount; i++)
                {
                    var enemy = Instantiate(data.Prefab, objParent.transform);
                    enemy.SetActive(false);
                    var controller = enemy.GetComponentInChildren<EnemyController>();
                    enemiesController.Add(controller);
                    enemy.name = enemy.name.Replace("(Clone)", "").Trim();
                    

                    if (data.Type == EnemyType.Normal || data.Type == EnemyType.Elite)
                    {
                        controller.OnEnemyDeath
                            .Subscribe(_ =>
                            {
                                objectiveManager.currentKilledMonster.Value = objectiveManager.currentKilledMonster.Value + 1;
                            }).AddTo(enemy);

                        controller.OnEnemyDeath
                        .Subscribe(_ =>
                        {
                            enemies[data.MonsterName].currentActiveMonster--;
                            controller.SpawnXpGem(DequeueXpGem(controller.Xp));
                            ReturnEnemy(enemy);
                        }).AddTo(enemy);
                    }
                    if(data.Type == EnemyType.Boss)
                    {
                        controller.OnEnemyDeath
                            .Subscribe(_=>
                            {
                                objectiveManager.currentKilledBoss.Value = objectiveManager.currentKilledBoss.Value + 1;
                                controller.SpawnXpGem(DequeueXpGem(controller.Xp,true));
                                ReturnEnemy(enemy,true);
                            }).AddTo(enemy);
                    }
                    healthScriptList.Add(enemy.GetComponentInChildren<HealthController>());
                    enemyList.Enqueue(enemy);
                }

                if (data.Type == EnemyType.Boss)
                {
                    enemies.Add("boss", new PoolActiveData(
                    0,
                    enemyList,
                    data,
                    healthScriptList,
                    enemiesController));
                }
                else
                {
                    enemies.Add(data.MonsterName, new PoolActiveData(
                        0,
                        enemyList,
                        data,
                        healthScriptList,
                        enemiesController));
                }
                    requiredExpGemAmount += data.MaxActiveUnit;
            }
            GenerateXpGem(requiredExpGemAmount, gemPoolParent.transform);

            foreach (KeyValuePair<string, PoolActiveData> keyValuePair in enemies)
            {
                var data = keyValuePair.Value.data;

                StartCoroutine(EnemyPoolData.WavesTrigger(keyValuePair.Value));

                var enemySpawnStream = Observable.Interval(TimeSpan.FromSeconds(data.SpawnDelay))
                    .Select(actives => keyValuePair.Value.currentActiveMonster)
                    .Where(actives => actives < data.MaxActiveUnit)
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

    public class PoolActiveData
    {
        public EnemyPoolData data;
        public int currentActiveMonster;
        public Queue<GameObject> enemiesList;
        public List<HealthController> healthScriptList;
        public List<EnemyController> enemies;
        
        public PoolActiveData(int currentActiveMonster, Queue<GameObject> queue, EnemyPoolData data, List<HealthController> healthList, List<EnemyController> enemies)
        {
            this.currentActiveMonster = currentActiveMonster;
            enemiesList = queue;
            this.data = data;
            healthScriptList = healthList;
            this.enemies = enemies;
        }
    }
    
}
