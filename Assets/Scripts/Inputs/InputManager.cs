using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{

    #region Events
    public delegate void StartSwipe(Vector2 position, float time);
    public event StartSwipe OnStartSwipe;
    public delegate void EndSwipe(Vector2 position, float time);
    public event EndSwipe OnEndSwipe;
    public delegate void Touch(Ray ray);
    public event Touch OnTouch;
    #endregion

    private TouchControls touchControls;
    private Camera mainCamera;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        Singleton();
        touchControls = new TouchControls();
        mainCamera = Camera.main;
    }

    void Singleton()
    {
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; }
    }

    void Start()
    {
        touchControls.Touch.PrimaryContact.started += ctx => StartPrimaryTouch(ctx);
        touchControls.Touch.PrimaryContact.canceled += ctx => EndPrimaryTouch(ctx);
    }

    private void StartPrimaryTouch(InputAction.CallbackContext context)
    {
        if (OnStartSwipe != null) OnStartSwipe(Utils.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        if (OnTouch != null)
            OnTouch(Utils.ScreenPointToRay(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
    }

    private void EndPrimaryTouch(InputAction.CallbackContext context)
    {
        if (OnEndSwipe != null) OnEndSwipe(Utils.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }
}
