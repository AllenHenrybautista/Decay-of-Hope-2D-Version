using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    //Input System
    public static Vector2 Movement;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private InputAction _moveAction;
    private PlayerInput _playerInput;

    //Animation Parameters
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string LastHorizontal = "LastHorizontal";
    private const string LastVertical = "LastVertical";


    private void Awake()
    {
        SetupPlayer();
        ReadInputs();
    }

    private void Update()
    {
        PlayerMove();
        HandleAnimation();
    }

    private void SetupPlayer()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void ReadInputs()
    {
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput != null)
            _moveAction = _playerInput.actions["Move"];        
        else
            Debug.LogError("PlayerInput is Missing");
    }

    private void PlayerMove()
    {
        if (_moveAction != null)
            Movement = _moveAction.ReadValue<Vector2>();

    }

    private void HandleAnimation()
    {
        _movement.Set(Movement.x, Movement.y);
        _rb.velocity = _movement * _speed;
        _animator.SetFloat(Horizontal, _movement.x);
        _animator.SetFloat(Vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(LastHorizontal, _movement.x);
            _animator.SetFloat(LastVertical, _movement.y);
        }
    }
}
