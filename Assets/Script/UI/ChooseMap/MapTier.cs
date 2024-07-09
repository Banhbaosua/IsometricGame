using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MapTier : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] MapTierData data;
    [SerializeField] Image lockIcon;
    [SerializeField] SpawnerData levelData;
    [SerializeField] CurrentMapLevelInfo mapLevelInfo;
    [SerializeField] Image frame;
    [SerializeField] Sprite hightlightFrame;
    [SerializeField] Sprite normalFrame;
    [SerializeField] GameEvent onLevelTierChoose;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(data.IsUnlocked)
            mapLevelInfo.SelectLevel(levelData);
        onLevelTierChoose.Notify(this, this);
        Debug.Log("click");
    }

    private void OnEnable()
    {
        LoadData();

        if (data.IsUnlocked)
            lockIcon.gameObject.SetActive(false);
        else
            lockIcon.gameObject.SetActive(true);
    }

    public void SaveData()
    {
        SaveUtility.SaveToJSON(mapLevelInfo.SelectedScene.name + data.Tier, data);
    }

    void LoadData()
    {
        if(SaveUtility.LoadFromJSON<MapTierData>(mapLevelInfo.SelectedScene.name) != null)
            data = SaveUtility.LoadFromJSON<MapTierData>(mapLevelInfo.SelectedScene.name);
    }

    public void Highlight(Component sender, object data)
    {
        if(sender == this)
        {
            frame.sprite = hightlightFrame;
        }
        else
            frame.sprite = normalFrame;
    }
}

[Serializable]
public class MapTierData
{
    [SerializeField] int tier;
    [SerializeField] bool isUnlocked;
    [SerializeField] float progress;
    public int Tier => tier;
    public bool IsUnlocked => isUnlocked;
    public float Progress => progress;
}
