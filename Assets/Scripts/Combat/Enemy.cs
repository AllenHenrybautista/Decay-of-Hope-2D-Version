using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyAI", menuName = "AI/EnemyAI")]
public class Enemy : EnemyAI
{
    public string TargetTag;
    public int Speed = 0;
    public int Health = 100;
    public int CurrentHealth = 0;
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

    //create a damage logic
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
