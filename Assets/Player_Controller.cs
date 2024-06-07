using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed = 5.0f;

    public Rigidbody2D rb;

    public void Update()
    {
        Input.GetAxisRaw("Horizontal");
        Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        
    }

}
