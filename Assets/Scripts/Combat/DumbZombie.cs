using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DumbZombie", menuName = "AI/DumbZombie")]
public class DumbZombie : EnemyAI
{
    public bool isNear = false;

    public override void think(EnemyLogic logic)
    {
        //get the player object
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        var move = logic.GetComponent<EnemyBasicMove>();

        if (move)
        {
            if (isNear && target != null)
            {
                //if the player is near, attack
                move.MoveTowardsTarget(target.transform.position);
            }
            else
            {
                //move randomly
                move.MoveTowardsTarget(new Vector2(Random.Range(-4, 10), Random.Range(-4, 10)));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNear = true;
        }
    }
}
