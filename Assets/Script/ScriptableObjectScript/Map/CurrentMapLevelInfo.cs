using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MapLevelInfo", menuName = "MapLevelInfo")]
public class CurrentMapLevelInfo : ScriptableObject
{
    [SerializeField] string selectedScene;
    [SerializeField] SpawnerData levelSetting;
    private MapTierData mapTierData;
    public string SelectedScene => selectedScene;
    public SpawnerData LevelSetting => levelSetting;
    public MapTierData MapTierData => mapTierData;

    public void SelectMap(string scene)
    {
        selectedScene = scene;
    }

    public void SelectLevel((SpawnerData,MapTierData) data)
    {
        this.levelSetting = data.Item1;
        this.mapTierData = data.Item2;
        Debug.Log(MapTierData.Tier);
    }
}
