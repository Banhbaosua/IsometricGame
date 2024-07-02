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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(data.IsUnlocked)
            mapLevelInfo.SelectLevel(levelData);
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
