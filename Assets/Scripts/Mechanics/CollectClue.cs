using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectClue : MonoBehaviour
{

    [SerializeField] private LayerMask clueMask;
    [SerializeField] private GameObject notesClue;
    [SerializeField] private Animator noteAnimator;
    [SerializeField] private GameObject noteParticle;

    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnTouch(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 100, clueMask))
        {
            Instantiate(noteParticle, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z ), Quaternion.identity);
            notesClue.SetActive(true);
            noteAnimator.SetTrigger("animationNote");
            Destroy(hit.transform.gameObject);
        }
    }

    private void OnEnable()
    {
        inputManager.OnTouch += OnTouch;
    }

    private void OnDisable()
    {
        inputManager.OnTouch -= OnTouch;
    }
}
