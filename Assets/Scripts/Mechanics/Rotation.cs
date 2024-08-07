using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private GameObject[] frontSideObjects;
    [SerializeField] private GameObject[] backSideObjects;
    [SerializeField] private List<AudioClip> swipeClips = new List<AudioClip>();
    
    private bool frontSide;
    private bool canInteract = true;

    private void Awake()
    {
        frontSide = true;
        LeanTouch.Instance.SwipeThreshold = 10f;
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerSwipe += HandleSwipe;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= HandleSwipe;
    }

    private void HandleSwipe(LeanFinger finger)
    {
        if (canInteract)
        {
            Vector2 swipeDelta = finger.SwipeScreenDelta;
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    RotateRoomRight();
                }
                else
                {
                    RotateRoomLeft();
                }
            }
        }
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

    private IEnumerator Interact()
    {
        SoundManager.Instance.RandomizedSFX(swipeClips);
        canInteract = false;
        yield return new WaitForSeconds(1f);
        SwitchSide();
        canInteract = true;
    }
}
