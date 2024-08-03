using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.VirtualTexturing;

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
    private CardRollingSystem cardRollingSystem;
    public IObservable<Unit> OnLevelUP => onLevelUp;

    float GetExpForLevel(int level)
    {
        return baseXp * Mathf.Pow(growthRate, level - 1);
    }
    private void Awake()
    {
        xp.rp.Value = 0;
        level = new ReactiveProperty<int>(0);
        onLevelUp = new Subject<Unit>();
        cardRollingSystem = FindObjectOfType<CardRollingSystem>();
    }

    void Start()
    {
        xp.rp.Subscribe(x =>
        {
            SliderUpdate(x);
        });

        xp.rp.Where(x => x >= GetExpForLevel(level.Value + 1))
            .Subscribe(async _ =>
            {
                await StartCoroutine(LevelUp());
            });

        level.Subscribe(_ =>
            {
                levelText.text = level.Value.ToString();
                xpSlider.value = 0;
            });
    }

    void SliderUpdate(float exp)
    {
        XpToSliderValue(exp);
        previousXpValue = exp;
    }

    void XpToSliderValue(float exp)
    {
        var maxXp = GetExpForLevel(level.Value+1);
        var percent = exp/ maxXp;

        xpSlider.value = percent;
    }

    IEnumerator LevelUp()
    {
        level.Value++;
        onLevelUp.OnNext(Unit.Default);
        yield return new WaitUntil(() => cardRollingSystem.DoneChoosing == true);
        xp.rp.Value = xp.rp.Value - GetExpForLevel(level.Value);
        SliderUpdate(xp.rp.Value);
    }



}
