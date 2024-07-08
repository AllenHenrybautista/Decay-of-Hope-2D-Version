using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Shooting : MonoBehaviour
{
    //re-create the PlayerCombat script here but this one is for shooting and melee attack

    [SerializeField] private Animator _animator;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerController_Shooting playerController_Shooting;

    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 100;
    public int playerHealth = 100;
    public int remainingHealth = 0;
    public int currentHealth = 0;
    public int currentRemainingHealth = 0;

    [Header("AttackPoints")]
    public Transform attackPointLeft;
    public Transform attackPointRight;
    public Transform attackPointUp;
    public Transform attackPointDown;


    private float _lastHorizontal;
    private float _lastVertical;
    private float _dieHorizontal;
    private float _dieVertical;


}
