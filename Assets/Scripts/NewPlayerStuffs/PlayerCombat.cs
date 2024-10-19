using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioManager audioManager;

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

    private float _lastHorizontal;
    private float _lastVertical;
    private PlayerInput _inputActions;


    private void Awake()
    {
        _inputActions = GetComponent<PlayerInput>();
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

            // Determine which attack point to use based on the player's last movement direction
            Transform chosenAttackPoint = null;

            if (_lastHorizontal > 0) // Attack right
            {
                chosenAttackPoint = attackPointRight;
            }
            else if (_lastHorizontal < 0) // Attack left
            {
                chosenAttackPoint = attackPointLeft;
            }
            else if (_lastVertical > 0) // Attack up
            {
                chosenAttackPoint = attackPointUp;
            }
            else if (_lastVertical < 0) // Attack down
            {
                chosenAttackPoint = attackPointDown;
            }

            if (chosenAttackPoint != null)
            {
                // Check for enemies at the chosen attack point
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(chosenAttackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<EnemyHandler>().TakeDamage(attackDamage);
                }
            }

            // Play slash sound from audio manager
            //audioManager.PlaySoundEffect("Slash");
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
