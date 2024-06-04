using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public event Action<bool> OnDeath;
    private float currentHealth;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public HealthController(float maxHealth) 
    { 
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0) 
        {
            currentHealth -= damage;
            if(currentHealth <= 0) 
            {
                OnDeath(true);
            }
        }
    }

    public void MaxHealthModify(float value)
    {
        maxHealth += value;
    }

    public void MaxHealthModify()
    {
        currentHealth = maxHealth;
    }
}
