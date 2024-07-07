using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TabButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TabType tabType;
    [SerializeField] GameObject _content;
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] private Sprite _unselectedSprite;
    [SerializeField] private GameEvent _tabOpenEvent;
    [SerializeField] private List<Transform> objectToEnables;
    private Image _btnImg;

    private void Awake()
    {
        if (tabType == TabType.Fight)
            _content.SetActive(true);
        else
            _content.SetActive(false);

        _btnImg = GetComponent<Image>();
        
    }
    void Start()
    {
        TabManager.Instance.AddTabBtnToList(this);
    }

    public void EnableTabButton(Component sender, object data)
    {
        if (sender != this)
        {
            _btnImg.sprite = _unselectedSprite;
            _content.SetActive(false);
            foreach(Transform t in objectToEnables) 
            { 
                t.gameObject.SetActive(false);
            }
        }
        if (sender == this)
        {
            _btnImg.sprite = _selectedSprite;
            _content.SetActive(true);
            foreach (Transform t in objectToEnables)
            {
                t.gameObject.SetActive(true);
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _tabOpenEvent.Notify(this, null);
    }
}
