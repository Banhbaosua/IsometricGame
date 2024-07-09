using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCharDescript : MonoBehaviour
{
    [SerializeField] CurrentClassData currentClassData;
    [SerializeField] List<Text> description;

    private void OnEnable()
    {
        Set(this, currentClassData.GetCharacterClassData());
    }
    public void Set(Component sender, object data)
    {
        foreach(var des in description)
        {
            des.gameObject.SetActive(false);
        }
        var character = data as CharacterClassData;
        for(int i =0; i < character.BonusStatList.Count;i++) 
        {
            var stat = character.BonusStatList[i];
            description[i].text = stat.Type.ToString() +" + "+stat.Value.ToString();
            description[i].gameObject.SetActive(true);
        }
    }
}
