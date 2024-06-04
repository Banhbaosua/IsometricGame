using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] CharacterData characterData;
    [SerializeField] Slider slider;
    [SerializeField] Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = characterData.HealthController.CurrentHealth/characterData.HealthController.MaxHealth;
        healthText.text = characterData.HealthController.CurrentHealth.ToString();
    }
}
