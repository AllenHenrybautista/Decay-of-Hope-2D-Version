using System.Collections;
using System.Collections.Generic;

using UnityEngine.Audio;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Animator _anim;
    [SerializeField] private RangeTrigger rangeTrigger;
    [SerializeField] private float moveSpeed = 0.8f;
    [SerializeField] private float roamSpeed = 0.5f;


    [SerializeField] private float roamPoint1X = 0;
    [SerializeField] private float roamPoint1Y = 0;
    [SerializeField] private float roamPoint2X = 0;
    [SerializeField] private float roamPoint2Y = 0;

    private Vector2 currentRoamTarget;

    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private bool isRoaming = false;

    // Flag to determine if the player is near this specific enemy
    public bool isNear = false;

    private void Start()
    {
        currentHealth = maxHealth;
        _anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

        // Subscribe to the RangeTrigger events
        rangeTrigger.OnPlayerEnterTrigger += OnPlayerEnterRange;
        rangeTrigger.OnPlayerExitTrigger += OnPlayerExitRange;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the RangeTrigger events to prevent memory leaks
        rangeTrigger.OnPlayerEnterTrigger -= OnPlayerEnterRange;
        rangeTrigger.OnPlayerExitTrigger -= OnPlayerExitRange;
    }

    private void Update()
    {
        // Logic to decide behavior based on player proximity
        if (isNear)
        {
            MoveTowardsTarget(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
        else
        {
            if (isRoaming)
            {
                Roam();
            }
            else
            {
                _anim.SetFloat("LastHorizontal", 0);
               
                roamSpeed = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision logic with player
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(10);
            _anim.SetTrigger("Attack");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        moveSpeed = 0;
        roamSpeed = 0;
        GetComponent<Rigidbody2D>().simulated = false;
        audioManager.PlaySoundEffect("ZombieDeath");
        _anim.SetTrigger("Die");
        Destroy(gameObject, 3f);
    }

    public void MoveTowardsTarget(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void Roam()
    {
        Vector2 currentPosition = transform.position;
        Vector2 direction = (currentRoamTarget - currentPosition).normalized;
        Vector2 smoothedPosition = currentPosition + direction * roamSpeed * Time.deltaTime;
        transform.position = smoothedPosition;
        _anim.SetFloat("Horizontal", direction.x);
        _anim.SetFloat("Vertical", direction.y);

        // Check if the enemy has reached the current target position
        if (Vector2.Distance(currentPosition, currentRoamTarget) < 0.1f)
        {
            // Switch the target position
            if (currentRoamTarget == new Vector2(roamPoint1X, roamPoint1Y))
            {
                currentRoamTarget = new Vector2(roamPoint2X, roamPoint2Y);
            }
            else
            {
                currentRoamTarget = new Vector2(roamPoint1X, roamPoint1Y);
            }
        }
    }

    private void OnPlayerEnterRange()
    {
        // When player enters the range trigger, set isNear to true
        isNear = true;
    }

    private void OnPlayerExitRange()
    {
        // When player exits the range trigger, set isNear to false
        isNear = false;
    }
}

