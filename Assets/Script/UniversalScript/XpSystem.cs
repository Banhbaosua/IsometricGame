using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class XpSystem : MonoBehaviour
{
    [SerializeField] GameEvent onXpReceiving;
    [SerializeField] FloatVariable xp;
    [SerializeField] Slider xpSlider;
    [SerializeField] Text levelText;
    [SerializeField] float baseXp;
    [SerializeField] float growthRate;
    private ReactiveProperty<int> level;

    float GetExpForLevel(int level)
    {
        return baseXp * Mathf.Pow(growthRate, level - 1);
    }
    private void Awake()
    {
        xp.RPValue.Value = 0;
        level = new ReactiveProperty<int>(0);
    }

    void Start()
    {
        xp.RPValue.Subscribe(_ =>
        {
            var maxXp = GetExpForLevel(level.Value);
            if (level.Value == 0)
            {
                maxXp = baseXp;
            }
            xpSlider.value = xp.RPValue.Value / maxXp;
        });

        xp.RPValue.Where(x => x >= GetExpForLevel(level.Value))
            .Subscribe(_ => level.Value++);

        level.Subscribe(_ =>
            {
                levelText.text = level.Value.ToString();
                xpSlider.value = 0;
            });
    }
    
    
    
}
