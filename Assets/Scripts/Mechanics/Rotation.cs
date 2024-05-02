using UnityEngine;

public class Rotation : MonoBehaviour
{
    private SwipeDetection _swipeDetection;

    private void Awake()
    {
        _swipeDetection = SwipeDetection.Instance;
    }

    private void RotateRoomRight()
    {
        transform.LeanRotateY(transform.eulerAngles.y + 180f, 1)
            .setEaseInOutQuad();
    }

    private void RotateRoomLeft()
    {
        transform.LeanRotateY(transform.eulerAngles.y - 180f, 1)
            .setEaseInOutQuad();
    }

    private void OnEnable()
    {
        _swipeDetection.OnSwipeRight += RotateRoomRight;
        _swipeDetection.OnSwipeLeft += RotateRoomLeft;
    }

    private void OnDisable()
    {
        _swipeDetection.OnSwipeRight -= RotateRoomRight;
        _swipeDetection.OnSwipeLeft -= RotateRoomLeft;
    }
}
