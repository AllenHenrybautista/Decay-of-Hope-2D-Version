using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public int Stamina = 100;
    public UnityEvent OnHealthChanged = new UnityEvent();
    private Animator animator;

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        HealthBarAdjust();

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    private void HealthBarAdjust()
    {
        OnHealthChanged.Invoke();
        Debug.Log("nag addjust pero di gumagalaw");
    }

    void Die()
    {
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
    }
}
