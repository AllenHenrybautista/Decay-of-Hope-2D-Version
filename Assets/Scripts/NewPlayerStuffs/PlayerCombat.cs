using System;
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


    public void UpdateDirection(float lastHorizontal, float lastVertical)
    {
        _lastHorizontal = lastHorizontal;
        _lastVertical = lastVertical;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _animator.SetFloat("ShootHorizontal", _lastHorizontal);
            _animator.SetFloat("ShootVertical", _lastVertical);
            _animator.SetTrigger("Shoot");


            //fix this issue NullReferenceException while executing 'performed' callbacks of 'Movement/Attack[/Keyboard/space]'UnityEngine.InputSystem.LowLevel.NativeInputRuntime /<> c__DisplayClass7_0:< set_onUpdate > b__0(UnityEngineInternal.Input.NativeInputUpdateType, UnityEngineInternal.Input.NativeInputEventBuffer *)UnityEngineInternal.Input.NativeInputSystem:NotifyUpdate(UnityEngineInternal.Input.NativeInputUpdateType, intptr)
            GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(_lastHorizontal, _lastVertical).normalized * 10;

        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _animator.SetFloat("StabHorizontal", _lastHorizontal);
            _animator.SetFloat("StabVertical", _lastVertical);
            _animator.SetTrigger("Stab");

            attacksequence();

            //detect enemies in range of attack and please fix this issue NullReferenceException while executing 'performed' callbacks of 'Movement/Attack[/Keyboard/ctrl]'UnityEngine.InputSystem.LowLevel.NativeInputRuntime /<> c__DisplayClass7_0:< set_onUpdate > b__0(UnityEngineInternal.Input.NativeInputUpdateType, UnityEngineInternal.Input.NativeInputEventBuffer *)UnityEngineInternal.Input.NativeInputSystem:NotifyUpdate(UnityEngineInternal.Input.NativeInputUpdateType, intptr)

            Collider2D[] hitEnemiesLeft = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);
            Collider2D[] hitEnemiesRight = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);
            Collider2D[] hitEnemiesUp = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayers);
            Collider2D[] hitEnemiesDown = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemiesLeft)
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
        GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody2D>().simulated = true;
    }

    void Die()
    {
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        _animator.SetTrigger("Die");
    }
}
