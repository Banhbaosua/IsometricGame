using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAlive : MonoBehaviour
{
    [SerializeField] Text text;

    private void Update()
    {
        float min = Mathf.FloorToInt(Time.time / 60);
        float sec = Mathf.FloorToInt(Time.time % 60);
        text.text = "Time alive: "+min.ToString() + ":" + sec.ToString("00");
    }
}
