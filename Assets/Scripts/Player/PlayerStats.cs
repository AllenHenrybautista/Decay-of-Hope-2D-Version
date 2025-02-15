using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Stats")]
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("Stamina Stats")]
    public int maxStamina = 100;
    public int currentStamina = 100;
    public float staminaDrainRate = 10f;
    public float staminaRegenRate = 5f;

    [Header("Hunger Stats")]
    public int maxHunger = 100;
    public int currentHunger = 100;
    public float hungerDrainRate = 0.5f;

    // Unity Events to notify changes
    public UnityEvent OnHealthChanged = new UnityEvent();
    public UnityEvent OnStaminaChanged = new UnityEvent();
    public UnityEvent OnHungerChanged = new UnityEvent();

    public bool CanSprint => currentStamina > 0;

    private void Update()
    {
        RegenerateStamina();
        DrainHunger();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged.Invoke();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle the death logic here (e.g., disable player controls, play death animation)
        Debug.Log("Player died");
    }

    // Stamina Logic
    public void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += (int)(staminaRegenRate * Time.deltaTime);
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            OnStaminaChanged.Invoke();
        }
    }

    public void DrainStamina()
    {
        if (currentStamina > 0)
        {
            currentStamina -= (int)(staminaDrainRate * Time.deltaTime);
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            OnStaminaChanged.Invoke();
        }
    }

    // Hunger Logic
    private void DrainHunger()
    {
        if (currentHunger > 0)
        {
            currentHunger -= (int)(hungerDrainRate * Time.deltaTime);
            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
            OnHungerChanged.Invoke();
        }
        else
        {
            TakeDamage(1); // Apply damage if hunger reaches 0
        }
    }

    // Regenerate Hunger (can be triggered when eating)
    public void RegenerateHunger(int amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        OnHungerChanged.Invoke();
    }
}