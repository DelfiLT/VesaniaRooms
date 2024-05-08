using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private LayerMask interactiveMask;
    private InputManager inputManager;
    
    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    
    private void OnTouch(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            InteractableObject touchedObject = hit.transform.gameObject.GetComponent<InteractableObject>();
            if(touchedObject != null)
            {
                touchedObject.HandleMovement();
            }
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
