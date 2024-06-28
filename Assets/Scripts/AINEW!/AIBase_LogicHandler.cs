using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase_LogicHandler : MonoBehaviour
{
    public AIBase_DataHandler dataHandler;
    private Animator _anim;
    private Rigidbody2D rb;
    private Collider2D _triggerCollider;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _triggerCollider = GetComponent<Collider2D>();
        dataHandler.SetupEnemy(_anim, rb, _triggerCollider);
    }

    private void Update()
    {
        dataHandler.Strategy(this);
    }

    private void move(Vector2 targetPosition)
    {
        dataHandler.Move(targetPosition, transform);
    }

}
