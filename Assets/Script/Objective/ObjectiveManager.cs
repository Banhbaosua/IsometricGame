using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] CurrentMapLevelInfo levelInfo;
    [SerializeField] Transform monsterObjBar;
    [SerializeField] Text monsterCount;
    [SerializeField] Text bossCount;
    private int currentObjectiveIndex;
    private CompositeDisposable disposables = new CompositeDisposable();
    private Subject<Unit> spawnBossRequest;
    public IObservable<Unit> spawnBossResponse => spawnBossRequest;
    public ReactiveProperty<int> currentKilledMonster;
    public ReactiveProperty<int> currentKilledBoss;
    private List<Objectives> objectives => levelInfo.LevelSetting.Objives;
    private bool isComplete;
    public bool IsCompleted => isComplete;

    private void Awake()
    {
        currentObjectiveIndex = 0;
        spawnBossRequest = new Subject<Unit>();
        currentKilledMonster = new ReactiveProperty<int>(0);
        currentKilledBoss    = new ReactiveProperty<int>(0);
        Initiate();
    }

    public void Initiate()
    {
        objectives[currentObjectiveIndex].Initiate();
        currentKilledMonster
            .Subscribe(x =>
            { 
                var kill = Mathf.Min(x, objectives[currentObjectiveIndex].Monster);
                UpdateMonsterUI(kill, objectives[currentObjectiveIndex].Monster);
            })
            .AddTo(disposables);

        currentKilledBoss
            .Subscribe(x =>
            {
                var kill = Mathf.Min(x, objectives[currentObjectiveIndex].Bosses);
                UpdateBossUI(kill, objectives[currentObjectiveIndex].Bosses);
            })
            .AddTo(disposables);

        currentKilledMonster
            .Where(_ => objectives[currentObjectiveIndex].Bosses > 0)
            .Where(x => x >= objectives[currentObjectiveIndex].Monster)
            .FirstOrDefault()
            .Subscribe(_ =>
            {
                monsterObjBar.gameObject.SetActive(false);
                spawnBossRequest.OnNext(Unit.Default);
            }).AddTo(disposables);

        currentKilledBoss
            .Where(x => x == objectives[currentObjectiveIndex].Bosses)
            .Subscribe(_ =>
            {
                objectives[currentObjectiveIndex].Complete();
            }).AddTo(disposables);

        objectives[currentObjectiveIndex].OnComplete.Subscribe(_ =>
        {
            currentKilledMonster.Value = 0;
            currentKilledBoss.Value = 0;
            monsterObjBar.gameObject.SetActive(true);
            if (currentObjectiveIndex == objectives.Count - 1)
            {
                isComplete = true;
            }
            else
            {
                currentObjectiveIndex++;
                Initiate();
            }
        }).AddTo(disposables);
    }

    void UpdateMonsterUI(int killCount, int killRequire)
    {
        monsterCount.text = "Kill  monters  to  spawn  boss: " + killCount.ToString()+ "/"+ killRequire.ToString();
    }

    void UpdateBossUI(int killCount, int killRequire)
    {
        bossCount.text = "Defeat  bosses: " + killCount.ToString() + "/" + killRequire.ToString();
    }
}

[Serializable]
public class Objectives
{
    [SerializeField] int monsters;
    [SerializeField] int bosses;
    private Subject<Unit> onComplete;
    public IObservable<Unit> OnComplete => onComplete;
    public int Monster => monsters;
    public int Bosses => bosses;

    public void Initiate()
    {
        onComplete = new Subject<Unit>();
    }
    public void Complete()
    { 
        onComplete.OnNext(Unit.Default);
    }
}
