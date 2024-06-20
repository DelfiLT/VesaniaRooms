using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectClue : MonoBehaviour
{
    [SerializeField] private GameObject notesClue;

    [SerializeField] private LayerMask clueMask;
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnTouch(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 100, clueMask))
        {
            Destroy(hit.transform.gameObject);
            notesClue.SetActive(true);
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
