using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


[CreateAssetMenu(fileName = "Zombie", menuName = "AI/ZombieBase")]
public class ZombieBase : AIBase_DataHandler
{
    AudioManager audioManager;
    //create a ID for each enemy to prevent duplicated enemies to affect each other
    private static int nextID = 0;
    private int enemyID;
    private int enemyIndex;

    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject blood;
    [SerializeField] private Collider2D _TriggerCollider;
    [SerializeField] private RangeTrigger rangeTrigger;


    [Header("Zombie Stats")]
    public string Name;
    public string Description;
    [SerializeField] private int Speed = 1;
    [SerializeField] private float RoamSpeed = 0.5f;
    [SerializeField] private int Health = 100;
    [SerializeField] private int CurrentHealth = 0;

    private Vector2 currentVelocity;

    //bool flags
    public bool isNear = false;
    public bool isTakingDamage = false;

    private float _horizontal;
    private float _vertical;


    public override void Awake()
    {
        SetupEnemy(_anim, rb, _TriggerCollider);
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void SetupEnemy(Animator anim, Rigidbody2D rb, Collider2D col)
    {
        CurrentHealth = Health;
        audioManager = FindObjectOfType<AudioManager>();
        _anim = anim;
        this.rb = rb;
        _TriggerCollider = col;
        enemyID = nextID;
        nextID++;
    }

    public override void Die()
    {
        Speed = 0;
        RoamSpeed = 0;
        rb.simulated = false;
        _anim.SetTrigger("Die");
        //Instantiate(blood, rb.transform.position, Quaternion.identity);
        Speed = Mathf.Max(Speed, 1);
    }

    public override void Move(Vector2 targetPosition, UnityEngine.Transform transform)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        _anim.SetFloat("Horizontal", targetPosition.x);
        _anim.SetFloat("Vertical", targetPosition.y);
    }

    public override void Strategy(AIBase_LogicHandler logic)
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        //if the player is near the zombie then move towards the player
        /*if (RangeTrigger.isNear)
        {
            Move(target.transform.position, logic.transform);
        }
        else
        {
            Vector2 randomPosition = new Vector2(Random.Range(-10, 50), Random.Range(-10, 50));
            Vector2 currentPosition = logic.transform.position;
            Vector2 direction = (randomPosition - currentPosition).normalized;
            Vector2 smoothedPosition = currentPosition + direction * RoamSpeed * Time.deltaTime;
            _anim.SetFloat("Horizontal", smoothedPosition.x);
            _anim.SetFloat("Vertical", smoothedPosition.y);
            Move(smoothedPosition, logic.transform);
        } */
    }

    public override void Attack(Collision2D collide)
    {
        if (collide.gameObject.CompareTag("Player"))
        {
            collide.gameObject.GetComponent<PlayerCombat_NEW>().TakeDamage(10);
            _anim.SetTrigger("Attack");
            _anim.SetFloat("Attackhorizontal", _horizontal);
            _anim.SetFloat("Attackvertical", _vertical);
            Debug.Log(Equals("Player" + " took damage"));
        }
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }

        Health = Mathf.Max(Health, 0);
    }

}
