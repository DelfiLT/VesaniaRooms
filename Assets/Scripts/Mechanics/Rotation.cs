using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Rotation : MonoBehaviour
{
    public float currentYAngle = 45;

    public void RotateRoomRight()
    {
        currentYAngle = currentYAngle - 180;
        transform.Rotate(Vector3.down, transform.eulerAngles.y + currentYAngle);
    }

    public void RotateRoomLeft()
    {

    }
}
