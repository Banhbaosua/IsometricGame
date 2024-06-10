using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class XpSystem : MonoBehaviour
{
    [SerializeField] GameEvent onXpReceiving;
    [SerializeField] FloatReactiveVariable xp;
    [SerializeField] Slider xpSlider;
    [SerializeField] Text levelText;
    [SerializeField] float baseXp;
    [SerializeField] float growthRate;
    private ReactiveProperty<int> level;
    private Subject<Unit> onLevelUp;
    private float previousXpValue = 0f;
    public IObservable<Unit> OnLevelUP => onLevelUp;

    float GetExpForLevel(int level)
    {
        return baseXp * Mathf.Pow(growthRate, level - 1);
    }
    private void Awake()
    {
        xp.RPValue.Value = 0;
        level = new ReactiveProperty<int>(0);
        onLevelUp = new Subject<Unit>();
    }

    void Start()
    {
        xp.RPValue.Subscribe(x =>
        {
            XpToSliderValue(x);
            previousXpValue = x;
        });

        xp.RPValue.Where(x => x >= GetExpForLevel(level.Value + 1)) //.Select(x => x -= GetExpForLevel(level.Value))
            .Subscribe(x =>
            {
                LevelUp();
            });

        level.Subscribe(_ =>
            {
                levelText.text = level.Value.ToString();
                xpSlider.value = 0;
            });
    }

    void XpToSliderValue(float exp)
    {
        var maxXp = GetExpForLevel(level.Value+1);
        var percent = (exp-previousXpValue)/ maxXp;

        xpSlider.value += percent;
    }

    void LevelUp()
    {
        level.Value++;
        onLevelUp.OnNext(Unit.Default);
    }
    
    
    
}
