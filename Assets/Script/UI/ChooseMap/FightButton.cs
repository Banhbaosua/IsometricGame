using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FightButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CurrentMapLevelInfo currentMapLevelInfo;
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(currentMapLevelInfo.SelectedScene.name, LoadSceneMode.Single);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
