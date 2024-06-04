using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] CharacterData characterData;
    private PowerUps powerUp;
    public PowerUps PowerUps => powerUp;
    private Subject<PowerUps> onPUSelected;
    public IObservable<PowerUps> OnPUSelected => onPUSelected;

    public void SetPowerUp(PowerUps newPU)
    {
        powerUp = newPU;
    }

    public void ChoosePowerUp()
    {
        powerUp.ApplyPowerUp(characterData);
    }
    // Start is called before the first frame update
    private void Awake()
    {
        button.OnClickAsObservable().Subscribe(_ => ChoosePowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
