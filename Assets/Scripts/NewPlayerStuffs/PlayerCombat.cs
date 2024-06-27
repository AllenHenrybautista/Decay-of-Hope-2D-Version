using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 100;
    public int playerHealth = 100;
    public int remainingHealth = 0;

    [Header("AttackPoints")]
    public Transform attackPointLeft;
    public Transform attackPointRight;
    public Transform attackPointUp;
    public Transform attackPointDown;

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

            attacksequence();

            //implement attack point for each direction
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);
            Collider2D[] hitEnemiesRight = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);
            Collider2D[] hitEnemiesUp = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayers);
            Collider2D[] hitEnemiesDown = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemiesRight)
            {
                enemy.GetComponent<EnemyHandler>().TakeDamage(attackDamage);
            }

            foreach (Collider2D enemy in hitEnemiesUp)
            {
                enemy.GetComponent<EnemyHandler>().TakeDamage(attackDamage);
            }

            foreach (Collider2D enemy in hitEnemiesDown)
            {
                enemy.GetComponent<EnemyHandler>().TakeDamage(attackDamage);
            }

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHandler>().TakeDamage(attackDamage);
               
            }

            //play slash sound from audio manager
            audioManager.PlaySoundEffect("Slash");

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointLeft == null)
            return;

        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
        Gizmos.DrawWireSphere(attackPointDown.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator attacksequence()
    {
        yield return new WaitForSeconds(0.5f);
    }

    void Die()
    {
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
    }
}
