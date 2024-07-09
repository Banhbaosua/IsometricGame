using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] List<Objectives> list;
    [SerializeField] Transform monsterObjBar;
    private ReactiveProperty<int> currentKilledMonster;
    private Objectives currentObjective;

    private void Awake()
    {
        currentObjective = list[0];
    }

    void StartObjective(Objectives objective)
    {
        currentObjective = objective;
        currentObjective.Initiate();
    }
}

public class Objectives
{
    [SerializeField] int monster;
    [SerializeField] GameObject boss;
    private Subject<Unit> onComplete;
    public IObservable<Unit> OnComplete;
    private bool bossKilled;

    public void Check(int currentKill)
    {
        if (currentKill == monster)
        {
            SpawnBoss();
        }
    }

    public void SpawnBoss()
    {
        ///spawn boss
    }
}
