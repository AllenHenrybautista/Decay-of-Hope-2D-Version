using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _sprintMultiplier = 1.5f;

    //Input System
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private InputAction _moveAction;
    private PlayerInput _playerInput;
    private PlayerCombat _playerCombat;
    private InputAction _sprintAction;
    private bool _isSprinting = false;

    private PlayerStats _playerStats;

    //Animation Parameters
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string LastHorizontal = "LastHorizontal";
    private const string LastVertical = "LastVertical";


    public static Vector2 Movement;
    public AudioSource footstepSFX;
    public float footstepInterval = 0.5f;
    private float nextFootstepTime;

    private void Awake()
    {
        SetupPlayer();
        ReadInputs();
    }

    private void Update()
    {
        PlayerMove();
        HandleAnimation();
        PlayFootstepSound();
    }

    private void SetupPlayer()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerCombat = GetComponent<PlayerCombat>();
        _playerStats = GetComponent<PlayerStats>();
    }

    private void ReadInputs()
    {
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput != null)
        {
            _moveAction = _playerInput.actions["Move"];
            _sprintAction = _playerInput.actions["Sprint"];
            _sprintAction.started += ctx => _isSprinting = true;
            _sprintAction.canceled += ctx => _isSprinting = false;
        }
                
        else
            Debug.LogError("PlayerInput is Missing");
    }

    private void PlayerMove()
    {
        if (_moveAction != null)
            Movement = _moveAction.ReadValue<Vector2>();

        if (!_playerStats.CanSprint)
        {
            _isSprinting = false;
        }

        float speed = _isSprinting ? _speed * _sprintMultiplier : _speed;

        _rb.velocity = Movement * speed;

        if (_isSprinting)
        {
            _playerStats.DrainStamina();
        }
        else
        {
            _playerStats.RegenerateStamina();
        }
    }

    private void Pickup()
    {
        _moveAction = _playerInput.actions["pickup"];
    }

    private void HandleAnimation()
    {
        _movement.Set(Movement.x, Movement.y);
        _animator.SetFloat(Horizontal, _movement.x);
        _animator.SetFloat(Vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(LastHorizontal, _movement.x);
            _animator.SetFloat(LastVertical, _movement.y);
        }

        _playerCombat.UpdateDirection(
            _animator.GetFloat(LastHorizontal),
            _animator.GetFloat(LastVertical)
        );
    }

    private void PlayFootstepSound()
    {
        if (footstepSFX == null)
            return;
        
        if (Movement != Vector2.zero && Time.time >= nextFootstepTime)
        {
            footstepSFX.Play();
            nextFootstepTime = Time.time + (_isSprinting ? footstepInterval / 1.5f : footstepInterval);
        }
    }
}
