using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKontroller : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Shooter2 playerControls;
    private Vector2 MOvement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;


    private void Awake()
    {
        playerControls = new Shooter2();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
       PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        MOvement = playerControls.MOvement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", MOvement.x);
        myAnimator.SetFloat("moveY", MOvement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + MOvement * speed * Time.fixedDeltaTime);
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 PlayertoScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < PlayertoScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }


}
