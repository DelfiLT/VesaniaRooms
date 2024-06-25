using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotation : MonoBehaviour
{
    private bool canInteract = true;

    [SerializeField] private float angle;
    [SerializeField] private GameObject[] frontSideObjects;
    [SerializeField] private GameObject[] backSideObjects;
    [SerializeField] private List<AudioClip> swipeClips = new List<AudioClip>();
    
    private bool frontSide;

    private void Awake()
    {
        frontSide = true;
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

    private void SwitchSide()
    {
        frontSide = !frontSide;
            
        if (frontSide)
        {
            foreach (GameObject frontSideObject in frontSideObjects)
            {
                if (!frontSideObject) return;
                frontSideObject.SetActive(true);
            }

            foreach (GameObject backSideObject in backSideObjects)
            {
                if(!backSideObject) return;
                backSideObject.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject frontSideObject in frontSideObjects)
            {
                if (!frontSideObject) return;
                frontSideObject.SetActive(false);
            }

            foreach (GameObject backSideObject in backSideObjects)
            {
                if(!backSideObject) return;
                backSideObject.SetActive(true);
            }
        }
    }
    
    private void OnDestroy()
    {
        SwipeDetection.OnRotateRight -= RotateRoomRight;
        SwipeDetection.OnRotateLeft -= RotateRoomLeft;
    }

    private IEnumerator Interact()
    {
        SoundManager.Instance.RandomizedSFX(swipeClips);
        canInteract = false;
        yield return new WaitForSeconds(1f);
        SwitchSide();
        canInteract = true;
    }
}
