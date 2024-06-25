using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private List<AudioClip> interactionClips = new List<AudioClip>();
    private bool canInteract = true;
    private bool moved = false;

    [SerializeField, Range(-0.1f, 0.1f)] float xPos;
    [SerializeField, Range(-0.1f, 0.1f)] float zPos;

    public void HandleMovement()
    {
        if (canInteract)
        {
            StartCoroutine(Interact());
            SoundManager.Instance.RandomizedSFX(interactionClips);
            if (moved)
            {
                transform.LeanMoveLocal(new Vector3(transform.localPosition.x - xPos, transform.localPosition.y,
                transform.localPosition.z + zPos), 1)
                .setEaseInOutQuad();
            } else
            {
                transform.LeanMoveLocal(new Vector3(transform.localPosition.x + xPos, transform.localPosition.y,
                transform.localPosition.z - zPos), 1)
                .setEaseInOutQuad();
            }

            moved = !moved;
        }
    }

    private IEnumerator Interact()
    {
        canInteract = false;
        yield return new WaitForSeconds(1f);
        canInteract = true;
    }
}
