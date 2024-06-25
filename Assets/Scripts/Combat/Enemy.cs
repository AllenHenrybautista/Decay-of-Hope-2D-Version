using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyAI", menuName = "AI/EnemyAI")]
public class Enemy : EnemyAI
{
    public string TargetTag;

    public int MaxHealth = 100;
    public int CurrentHealth;
    public int Speed = 0;
    public string Name;

    public enum EnemyType
    {
        Melee,
        Ranged,
        Boss
    }

    public override void think(EnemyLogic logic)
    {
        GameObject target = GameObject.FindGameObjectWithTag(TargetTag);
        if (target)
        {
            var move = logic.GetComponent<EnemyBasicMove>();
            if (move)
            {
                move.MoveTowardsTarget(target.transform.position);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }





























    /*
     * 
     * 
     * 
    
    
    
    
    
    
    
    EnemyAI enemyAI;

    public int MaxHealth = 100;
    public int CurrentHealth;

    public Rigidbody2D rb;
    public float speed;


    private void Start()
    {
        //Make sure that the enemy has the correct health based on scriptable object AI

    }


    public void ChasePlayer(Vector2 targetPosition)
    {
       transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
      Debug.Log("Enemy died!");
        Destroy(gameObject);
    }*/
}
