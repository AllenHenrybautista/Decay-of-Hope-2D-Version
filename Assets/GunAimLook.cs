using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAimLook : MonoBehaviour
{
    //create script that will move the gun where the mouse is pointing
    private Camera cam;
    private Vector2 mousePos;
    private Vector2 lookDir;
    private float angle;
    private Transform gunTransform;

    private void Start()
    {
        cam = Camera.main;
        gunTransform = transform;
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - (Vector2)gunTransform.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        gunTransform.rotation = Quaternion.Euler(0, 0, angle);
    }   
}
