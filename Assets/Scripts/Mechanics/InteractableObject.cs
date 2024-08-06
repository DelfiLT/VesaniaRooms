using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private List<AudioClip> interactionClips = new List<AudioClip>();
    private bool canInteract = true;
    private bool moved = false;

    [Header("Rotation")]
    [SerializeField] private bool rotation;
    [SerializeField, Range(-180f, 180f)] float angleX;
    [SerializeField, Range(-180f, 180f)] float angleY;

    [Header("Movement")]
    [SerializeField] private bool movement;
    [SerializeField, Range(-0.1f, 0.1f)] float xPos;
    [SerializeField, Range(-0.1f, 0.1f)] float zPos;

    public void HandleMovement()
    {
        if (canInteract)
        {
            StartCoroutine(Interact());
            SoundManager.Instance.RandomizedSFX(interactionClips);

            if (movement)
            {
                if (moved)
                {
                    transform.LeanMoveLocal(new Vector3(transform.localPosition.x - xPos, transform.localPosition.y,
                    transform.localPosition.z + zPos), 1)
                    .setEaseInOutQuad();
                }
                else
                {
                    transform.LeanMoveLocal(new Vector3(transform.localPosition.x + xPos, transform.localPosition.y,
                    transform.localPosition.z - zPos), 1)
                    .setEaseInOutQuad();
                }

                moved = !moved;
            }

            if (rotation)
            {
                if (moved)
                {
                    LeanTween.rotateLocal(this.gameObject, new Vector3(angleX, angleY, 0), 1).setEaseInOutQuad();
                }
                else
                {
                    LeanTween.rotateLocal(this.gameObject, new Vector3(0, 0, 0), 1).setEaseInOutQuad();
                }

                moved = !moved;
            }
        }
    }

    private IEnumerator Interact()
    {
        canInteract = false;
        yield return new WaitForSeconds(1f);
        canInteract = true;
    }
}