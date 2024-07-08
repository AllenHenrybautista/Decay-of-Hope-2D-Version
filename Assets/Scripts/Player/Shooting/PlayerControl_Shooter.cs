using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl_Shooter : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    //Input System
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private InputAction _moveAction;
    private PlayerInput _playerInput;
    private PlayerCombat _playerCombat;

    //Animation Parameters
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string LastHorizontal = "LastHorizontal";
    private const string LastVertical = "LastVertical";
    private const string DieVertical = "DieVertical";
    private const string DieHorizontal = "DieHorizontal";


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
        LookAtMouse();
    }

    private void SetupPlayer()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void ReadInputs()
    {
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput != null)
        {
            _moveAction = _playerInput.actions["Move"];
        }

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
            //_playerCombat.UpdateDirection(_movement.x, _movement.y);
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepSFX == null)
            return;

        if (Movement != Vector2.zero && Time.time >= nextFootstepTime)
        {
            footstepSFX.Play();
            nextFootstepTime = Time.time + footstepInterval;
        }
    }
    //incorporate mouse look here without sprite flipping, instead use the animator directions
    private void LookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        if ( mousePos.x < playerScreenPoint.x)
        {
            _animator.SetFloat(LastHorizontal, 1);
        }
        else
        {
            _animator.SetFloat(Horizontal, 0);
        }

        if (mousePos.y < playerScreenPoint.y)
        {
            _animator.SetFloat(Vertical, -1);
        }
        else
        {
            _animator.SetFloat(Vertical, 0);
        }
    }


}
