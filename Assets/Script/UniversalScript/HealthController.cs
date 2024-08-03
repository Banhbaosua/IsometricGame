using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public event Action<bool> OnDeath;
    private float currentHealth;
    private float modifiedHealth;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth + maxHealth*modifiedHealth;
    public HealthController(float maxHealth) 
    { 
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0) 
        {
            currentHealth = Mathf.Max(0,currentHealth-damage);
            if(currentHealth <= 0) 
            {
                OnDeath(true);
            }
        }
    }

    public void HealthMultiModify(float value)
    {
        modifiedHealth += value;
    }

    public void SetMaxHealth(float value)
    {
        maxHealth = value;
    }

    public void ResetHealth()
    {
        currentHealth = MaxHealth;
    }
}
