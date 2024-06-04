using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterButton : MonoBehaviour ,IPointerClickHandler
{
    [SerializeField] private CharacterClassData _characterClassData;
    [SerializeField] private GameEvent _onCharBtnClick;
    [SerializeField] GameObject _menuChar;

    public void OnPointerClick(PointerEventData eventData)
    {
        _onCharBtnClick.Notify(this, _characterClassData);
    }
}
