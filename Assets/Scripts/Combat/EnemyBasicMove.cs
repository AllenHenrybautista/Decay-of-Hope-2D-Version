using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicMove : MonoBehaviour
{
    [SerializeField]private Animator _anim;

    public Rigidbody2D rb;
    public float moveSpeed = 2f;

    public bool isNear = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isNear = true;
        }
    }
    public void MoveTowardsTarget(Vector2 targetposition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetposition, moveSpeed * Time.deltaTime);
        _anim.SetFloat("Horizontal", targetposition.x);
        _anim.SetFloat("Vertical", targetposition.y);
    }
}
