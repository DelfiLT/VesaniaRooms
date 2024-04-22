using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Rotation : MonoBehaviour
{
    public void RotateRoomRight()
    {
        transform.LeanRotateY(transform.eulerAngles.y + 180f, 1)
            .setEaseInOutQuad();
    }

    public void RotateRoomLeft()
    {
        transform.LeanRotateY(transform.eulerAngles.y - 180f, 1)
            .setEaseInOutQuad();
    }
}
