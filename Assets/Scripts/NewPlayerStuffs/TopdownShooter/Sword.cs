using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sword : MonoBehaviour
{
    private Shooter2 playerControls;
    private Animator anim;
    private PlayerKontroller playerKontroller;
    private ActiveWeapon activeWeapon;


    private void Awake()
    {
        playerControls = new Shooter2();
        anim = GetComponent<Animator>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        playerKontroller = GetComponentInParent<PlayerKontroller>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.performed += _ => Attack();
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenpoint = Camera.main.WorldToScreenPoint(playerKontroller.transform.position);

        float angle  = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenpoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 180, angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
