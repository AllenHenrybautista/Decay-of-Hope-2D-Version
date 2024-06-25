using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DumbZombie", menuName = "AI/DumbZombie")]
public class DumbZombie : EnemyAI
{

    public bool isNear = false;
    public Collider2D trigger;

    public override void think(EnemyLogic logic)
    {
        //get the player object
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (isNear)
        {
            //if the player is near, attack
            var move = logic.GetComponent<EnemyBasicMove>();
            if (move)
            {
                move.MoveTowardsTarget(target.transform.position);
            }
        }
        else

        {
            var move = logic.GetComponent<EnemyBasicMove>();
            move.MoveTowardsTarget(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)));
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isNear = true;
        }
    }
}
