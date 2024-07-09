using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FightButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CurrentMapLevelInfo currentMapLevelInfo;
    [SerializeField] LoadingAsync loadingAsync;
    public void OnPointerClick(PointerEventData eventData)
    {
        loadingAsync.LoadScene(currentMapLevelInfo.SelectedScene);
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
