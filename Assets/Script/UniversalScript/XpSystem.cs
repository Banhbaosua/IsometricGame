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
        xp.RPValue.Subscribe(_ =>
        {
            XpToSliderValue();
        });

        xp.RPValue.Where(x => x >= GetExpForLevel(level.Value))
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

    void XpToSliderValue()
    {
        var maxXp = GetExpForLevel(level.Value);
        if (level.Value == 0)
        {
            maxXp = baseXp;
        }
        xpSlider.value = xp.RPValue.Value / maxXp;
    }

    void LevelUp()
    {
        level.Value++;
        onLevelUp.OnNext(Unit.Default);
    }
    
    
    
}
