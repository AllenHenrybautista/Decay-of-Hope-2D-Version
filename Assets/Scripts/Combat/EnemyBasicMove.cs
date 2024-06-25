using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicMove : MonoBehaviour
{
   public Rigidbody2D rb;
   public float moveSpeed = 2f;

    public void MoveTowardsTarget(Vector2 targetposition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetposition, moveSpeed * Time.deltaTime);
    }
}
