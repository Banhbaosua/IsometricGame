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

    public MapTierData Data => data;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(data.IsUnlocked)
            mapLevelInfo.SelectLevel((levelData,data));
        onLevelTierChoose.Notify(this, this);
    }

    private void OnEnable()
    {
        LoadData();

        UIUpdate();
    }

    public void Unlock()
    {
        data.Unlock();
        UIUpdate();
    }

    public void UIUpdate()
    {
        if (data.IsUnlocked)
        {
            lockIcon.gameObject.SetActive(false);
            frame.raycastTarget = true;
        }
        else
        {
            lockIcon.gameObject.SetActive(true);
            frame.raycastTarget = false;
        }
    }

    void LoadData()
    {
        if (data.Tier == 1 || data.Tier == 2)
            data.Unlock();
        if(SaveUtility.LoadFromJSON<MapTierData>(data.Tier.ToString()) != null)
            data = SaveUtility.LoadFromJSON<MapTierData>(data.Tier.ToString());
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
    [SerializeField] bool isCompleted;
    public int Tier => tier;
    public bool IsUnlocked => isUnlocked;
    public float Progress => progress;
    public bool IsCompleted => isCompleted;

    public void Unlock()
    {
        isUnlocked = true;
    }

    public void Complete()
    {
        isCompleted = true;
    }

    public void Save(string mapName)
    {
        SaveUtility.SaveToJSON(mapName+tier.ToString(), this);
    }
}
