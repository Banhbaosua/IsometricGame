using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CurrentMapLevelInfo currentMapLevelInfo;
    [SerializeField] string map;
    [SerializeField] Transform selectedFrame;
    [SerializeField] GameEvent onMapChoose;

    public void OnPointerClick(PointerEventData eventData)
    {
        currentMapLevelInfo.SelectMap(map);
        onMapChoose.Notify(this, true);
    }

    public void EnableSelectedFrame(Component sender, object data)
    {
        if (sender == this)
            selectedFrame.gameObject.SetActive(true);
        else
            selectedFrame.gameObject.SetActive(false);
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
