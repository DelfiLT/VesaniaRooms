using UnityEngine;

public class Menu : MonoBehaviour
{
    private SwipeDetection _swipeDetection;

    private void Awake()
    {
        _swipeDetection = SwipeDetection.Instance;
    }

    private void RotateRight()
    {
        transform.LeanRotateY(transform.eulerAngles.y + 90f, 1)
            .setEaseInOutQuad();
    }
    
    private void RotateLeft()
    {
        transform.LeanRotateY(transform.eulerAngles.y - 90f, 1)
            .setEaseInOutQuad();
    }

    private void OnEnable()
    {
        _swipeDetection.OnSwipeRight += RotateRight;
        _swipeDetection.OnSwipeLeft += RotateLeft;
    }

    private void OnDisable()
    {
        _swipeDetection.OnSwipeRight -= RotateRight;
        _swipeDetection.OnSwipeLeft -= RotateLeft;
    }
}
