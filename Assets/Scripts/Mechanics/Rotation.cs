using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    private bool canInteract = true;

    [SerializeField] private float angle;

    private void Awake()
    {
        StartCoroutine(Interact());
    }

    private void Start()
    {
        SwipeDetection.OnRotateRight += RotateRoomRight;
        SwipeDetection.OnRotateLeft += RotateRoomLeft;
    }

    public void RotateRoomRight()
    {
        if (canInteract)
        {
            StartCoroutine(Interact());
            transform.LeanRotateY(transform.eulerAngles.y + angle, 1)
                .setEaseInOutQuad();
        }
    }

    public void RotateRoomLeft()
    {
        if (canInteract)
        {
            StartCoroutine(Interact());
            transform.LeanRotateY(transform.eulerAngles.y - angle, 1)
                .setEaseInOutQuad();
        }
    }

    private void OnDestroy()
    {
        SwipeDetection.OnRotateRight -= RotateRoomRight;
        SwipeDetection.OnRotateLeft -= RotateRoomLeft;
    }

    private IEnumerator Interact()
    {
        canInteract = false;
        yield return new WaitForSeconds(1f);
        canInteract = true;
    }
}
