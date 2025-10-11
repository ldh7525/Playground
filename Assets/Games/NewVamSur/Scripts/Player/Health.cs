using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    public Action OnDie;
    public Action OnDamage;

    private void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnDamage?.Invoke();

        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        OnDie?.Invoke();
    }
}
