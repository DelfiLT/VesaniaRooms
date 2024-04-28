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
    
    private void OnTouch(Vector2 position, float time)
    {
        RaycastHit hit; 
        Debug.DrawRay(position, Vector3.forward, Color.magenta);
        
        if (Physics.Raycast(position, Vector3.forward, out hit, 100, interactiveMask))
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
        inputManager.OnStartTouch += OnTouch;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= OnTouch;
    }
}
