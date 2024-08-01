using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    [SerializeField] private GameObject currentRoom;
    [SerializeField] private GameObject nextRoom;

    [SerializeField] private LayerMask doorMask;
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnTouch(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 100, doorMask))
        {
            //TODO: Implement change room animation
            currentRoom.SetActive(false);
            nextRoom.SetActive(true);
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
