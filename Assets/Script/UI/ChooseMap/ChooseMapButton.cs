using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseMapButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Transform mapBoard;
    public void OnPointerClick(PointerEventData eventData)
    {
        mapBoard.gameObject.SetActive(true);
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
