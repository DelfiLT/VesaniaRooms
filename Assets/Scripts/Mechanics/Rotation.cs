using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    float r;

    private Rigidbody rb;
  
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void RotateRoomRight()
    {
        //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, 225f, 0f) * rotationSpeed * Time.deltaTime);
        //rb.MoveRotation(rb.rotation * deltaRotation);

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, -135f, ref r, rotationSpeed);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void RotateRoomLeft()
    {
       
    }
}
