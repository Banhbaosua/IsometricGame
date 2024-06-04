using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passive",menuName = "PowerUp/Passive")]
public class Passive : PowerUps
{
    [SerializeField] List<StatWeightWrapper> list;
    private AddictiveStat rolledStat;
    public AddictiveStat RolldedStat => rolledStat;
    private string _descripton;
    public override void Apply(CharacterData characterData)
    {
        characterData.StatList[rolledStat.Type].AddAddictiveStats(rolledStat.Value, this);
    }

    public void RollRarity()
    {
        float rd = Random.Range(0f, 100f);
        Debug.Log(rd);
        int index;
        for(index = 0; index< list.Count;++index)
        {
            rd -= list[index].Weight;
            if (rd < 0)
            {
                rolledStat = list[index].AddictiveStat;
                return;
            }
        }
        _descripton = Description + " " + rolledStat.Value.ToString();
    }
}
[System.Serializable]
public class StatWeightWrapper
{
    [SerializeField] AddictiveStat addictiveStat;
    [SerializeField] float weight;
    public float Weight => weight;
    public AddictiveStat AddictiveStat => addictiveStat;
}
