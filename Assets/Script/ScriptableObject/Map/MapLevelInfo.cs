using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MapLevelInfo", menuName = "MapLevelInfo")]
public class MapLevelInfo : ScriptableObject
{
    [SerializeField] Object selectedScene;
    [SerializeField] SpawnerData levelSetting;

    public Object SelectedScene => selectedScene;
    public SpawnerData LevelSetting => levelSetting;

    public void SelectMap(Object scene)
    {
        selectedScene = scene;
    }

    public void SelectLevel(SpawnerData levelSetting)
    {
        this.levelSetting = levelSetting;
    }
}
