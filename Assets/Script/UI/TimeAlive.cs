using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAlive : MonoBehaviour
{
    [SerializeField] Text text;
    private float currentTime;
    private void Start()
    {
        currentTime = 0;
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        float min = Mathf.FloorToInt(currentTime / 60);
        float sec = Mathf.FloorToInt(currentTime % 60);
        text.text = "Time alive: "+min.ToString() + ":" + sec.ToString("00");
    }
}
