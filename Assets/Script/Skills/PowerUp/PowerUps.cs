using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PowerUpType
{
    Passive,
    SkillUpgrade,
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
}
public abstract class PowerUps : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] PowerUpType type;

    protected Rarity rarity;
    public Rarity Rarity => rarity;
    public PowerUpType Type => type;
    public Sprite Icon => icon;
    [TextAreaAttribute]
    public string Description;
    protected virtual void SetRarity(Rarity rarity)
    {
        this.rarity = rarity;
    }

    public virtual void Apply(CharacterData characterData)
    {

    }

    public virtual string GetDescription()
    {
        return Description;
    }
}
