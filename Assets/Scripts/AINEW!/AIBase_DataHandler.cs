using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBase_DataHandler : ScriptableObject
{
    public abstract void Strategy(AIBase_LogicHandler logic);

    public abstract void Awake();

    public abstract void Update();

    public abstract void SetupEnemy(Animator anim, Rigidbody2D rb, Collider2D col);

    public abstract void Attack(Collision2D collision);

    public abstract void TakeDamage(int damage);

    public abstract void Die();

    public abstract void Move(Vector2 targetPosition, UnityEngine.Transform transform);

}
