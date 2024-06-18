using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectComponent : MonoBehaviour
{
    [SerializeField] List<StatusEffect> effects;
    public List<StatusEffect> Effects => effects;

    private void Awake()
    {
        foreach (var effect in Effects) 
        {
            effect.Initiate();
        }
    }
    public void ApplyStatus(IEffectable obj)
    {
        foreach (var effect in Effects)
        {
            effect.Apply(obj);
        }
    }
    private void Update()
    {
    }
}
