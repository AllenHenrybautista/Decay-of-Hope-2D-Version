using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHealth : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyBasicMove _movement;

    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        _animator.SetBool("Death", true);
        
        _movement.enabled = false;
        WaitForSeconds wait = new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
