using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTabManager : MonoBehaviour
{
    [SerializeField] Currency currency;
    [SerializeField] Text minorText; 
    private void Update()
    {
        minorText.text = currency.Amount.ToString();
    }
}
