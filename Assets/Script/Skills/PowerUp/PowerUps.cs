using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PowerUpType
{
    Passive,
    SkillUpgrade,
}
public abstract class PowerUps : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] PowerUpType type;


    public PowerUpType Type => type;
    public Sprite Icon => icon;
    [TextAreaAttribute]
    public string Description;

    public virtual void Apply(CharacterData characterData)
    {

    }
}
