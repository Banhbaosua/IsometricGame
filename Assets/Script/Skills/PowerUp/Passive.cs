using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passive",menuName = "PowerUp/Passive")]
public class Passive : PowerUps
{
    [SerializeField] AddictiveStat addictiveStat;

    public override void ApplyPowerUp(CharacterData characterData)
    {
        characterData.StatList[addictiveStat.Type].AddAddictiveStats(addictiveStat.Value, this);
    }
}
