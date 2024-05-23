using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class SwipeDetection : MonoBehaviour
{
    #region Events
    public delegate void RotateRight();
    public static RotateRight OnRotateRight;
    public delegate void RotateLeft();
    public static RotateLeft OnRotateLeft;
    #endregion

    [Header("Configs")]
    [SerializeField]
    private float minDistance = .2f;
    [SerializeField]
    private float maxTime = 1f;
    [SerializeField, Range(0,1)]
    private float directionThreshold = .9f;

    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    public static SwipeDetection Instance { get; private set; }

    private void Awake()
    {
        inputManager = InputManager.Instance;
        Singleton();
    }

    void Singleton()
    {
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; }
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {        
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector3.Distance(startPosition, endPosition) >= minDistance && (endTime - startTime) <= maxTime) 
        {
            Vector3 direction3D = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction3D.x, direction3D.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if(Vector2.Dot(Vector2.right, direction) > directionThreshold) 
        {
            OnRotateRight?.Invoke();
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            OnRotateLeft?.Invoke();
        }
    }

    private void OnEnable()
    {
        inputManager.OnStartSwipe += SwipeStart;
        inputManager.OnEndSwipe += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartSwipe -= SwipeStart;
        inputManager.OnEndSwipe -= SwipeEnd;
    }
}
