using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyHandler : MonoBehaviour
{

    [SerializeField] private AudioManager audioManager;

    //Movement handler
    [SerializeField] private Animator _anim;
    public Rigidbody2D rb;
    public float moveSpeed = 2f;
    public bool isNear = false;

    //Health Stuff
    public int maxHealth = 100;
    public int currentHealth;
    public bool isTakingDamage = false;

    public GameObject blood;


    private float _horizontal;
    private float _vertical;


    private void Start()
    {
        currentHealth = maxHealth;
        _anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        moveSpeed = 0;
        GetComponent<Rigidbody2D>().simulated = false;
        audioManager.PlaySoundEffect("ZombieDeath");
        _anim.SetTrigger("Die");
        Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(10);
            _anim.SetTrigger("Attack");
            _anim.SetFloat("Attackhorizontal", _horizontal);
            _anim.SetFloat("Attackvertical", _vertical);
            Debug.Log(Equals("Player" + " took damage"));
        }
    }

    public void MoveTowardsTarget(Vector2 targetposition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetposition, moveSpeed * Time.deltaTime);
        _anim.SetFloat("Horizontal", targetposition.x);
        _anim.SetFloat("Vertical", targetposition.y);
    }

}