using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passive",menuName = "PowerUp/Passive")]
public class Passive : PowerUps
{
    [SerializeField] List<StatWeightWrapper> list;
    private AddictiveStat rolledStat;
    public AddictiveStat RolldedStat => rolledStat;
    public override void Apply(CharacterData characterData)
    {
        characterData.StatList[rolledStat.Type].AddAddictiveStats(rolledStat.Value, this);
    }

    public void RollRarity()
    {
        float rd = Random.Range(0f, 100f);
        int index;
        for(index = 0; index< list.Count;++index)
        {
            rd -= list[index].Weight;
            if (rd < 0)
            {
                rolledStat = list[index].AddictiveStat;
                SetRarity(list[index].Rarity);
                return;
            }
        }
    }

    public override string GetDescription()
    {
        return Description + " " + rolledStat.Value.ToString();
    }
}
[System.Serializable]
public class StatWeightWrapper
{
    [SerializeField] AddictiveStat addictiveStat;
    [SerializeField] float weight;
    [SerializeField] Rarity rarity;
    public float Weight => weight;
    public AddictiveStat AddictiveStat => addictiveStat;
    public Rarity Rarity => rarity;
}
