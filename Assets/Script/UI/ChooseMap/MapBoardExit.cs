using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapBoardExit : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Transform mapBoard;
    public void OnPointerClick(PointerEventData eventData)
    {
        mapBoard.gameObject.SetActive(false);
    }
}
