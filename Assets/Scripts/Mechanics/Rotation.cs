using UnityEngine;

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
