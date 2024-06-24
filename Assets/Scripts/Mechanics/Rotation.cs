using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotation : MonoBehaviour
{
    private bool canInteract = true;

    [SerializeField] private float angle;
    [SerializeField] private List<AudioClip> swipeSounds = new List<AudioClip>();

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
            CallSound();
        }
    }

    public void RotateRoomLeft()
    {
        if (canInteract)
        {
            StartCoroutine(Interact());
            transform.LeanRotateY(transform.eulerAngles.y - angle, 1)
                .setEaseInOutQuad();
            CallSound();
        }
    }

    private void OnDestroy()
    {
        SwipeDetection.OnRotateRight -= RotateRoomRight;
        SwipeDetection.OnRotateLeft -= RotateRoomLeft;
    }

    private void CallSound()
    {
        SoundManager.Instance.RandomizedSFX(swipeSounds);
    }
    private IEnumerator Interact()
    {
        canInteract = false;
        yield return new WaitForSeconds(1f);
        canInteract = true;
    }
}
