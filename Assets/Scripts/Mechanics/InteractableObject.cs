using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private bool canInteract = true;
    private bool moved = false;

    [SerializeField, Range(0, 0.1f)] float xPos;
    [SerializeField, Range(0, 0.1f)] float zPos;

    public void HandleMovement()
    {
        if (canInteract)
        {
            StartCoroutine(Interact());
            if (moved)
            {
                transform.LeanMove(new Vector3(transform.position.x - xPos, transform.position.y,
                transform.position.z + zPos), 1)
                .setEaseInOutQuad();
            } else
            {
                transform.LeanMove(new Vector3(transform.position.x + 0.1f, transform.position.y,
                transform.position.z - zPos), 1)
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
