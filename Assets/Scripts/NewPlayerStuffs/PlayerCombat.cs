using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public int playerHealth = 100;
    public int remainingHealth = 0;

    AudioManager audioManager;

    private float _lastHorizontal;
    private float _lastVertical;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void UpdateDirection(float lastHorizontal, float lastVertical)
    {
        _lastHorizontal = lastHorizontal;
        _lastVertical = lastVertical;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _animator.SetFloat("AttackHorizontal", _lastHorizontal);
            _animator.SetFloat("AttackVertical", _lastVertical);
            _animator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
               Debug.Log("hit" + enemy.name);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
    }
}
