using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAI", menuName = "AI/EnemyAI")]
public class Enemy : EnemyAI
{
    public string TargetTag;
    public float Speed = 0.8f;
    public int Health = 100;
    public int CurrentHealth = 0;
    public string Name;

    public override void think(EnemyLogic logic)
    {
        var move = logic.GetComponent<EnemyHandler>();
        GameObject target = GameObject.FindGameObjectWithTag(TargetTag);

        if (target != null)
        {
            // Check if the specific enemy instance is near the player
            if (move.isNear)
            {
                move.MoveTowardsTarget(target.transform.position);
            }
            else
            {
                move.Roam();
            }
        }
    }

    // Damage logic
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
        Destroy(this);
    }
}

