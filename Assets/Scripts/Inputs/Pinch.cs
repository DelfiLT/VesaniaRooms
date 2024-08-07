using Lean.Touch;
using System.Collections.Generic;
using UnityEngine;

public class Pinch : MonoBehaviour
{
    [SerializeField] private float minFOV;
    [SerializeField] private float maxFOV;

    private Camera _camera;
    private float originalFOV;

    void Start()
    {
        _camera = Camera.main;
        originalFOV = _camera.fieldOfView;
    }

    void OnEnable()
    {
        LeanTouch.OnGesture += HandlePinch;
        LeanTouch.OnFingerUp += HandleFingerUp;
    }

    void OnDisable()
    {
        LeanTouch.OnGesture -= HandlePinch;
        LeanTouch.OnFingerUp -= HandleFingerUp;
    }

    void HandlePinch(List<LeanFinger> fingers)
    {
        if (fingers.Count > 1)
        {
            float pinchRatio = LeanGesture.GetPinchRatio(fingers, 1.0f);
            if (pinchRatio != 1.0f)
            {
                float newFOV = _camera.fieldOfView * pinchRatio;
                _camera.fieldOfView = Mathf.Clamp(newFOV, minFOV, maxFOV);
            }
        }
    }

    void HandleFingerUp(LeanFinger finger)
    {
        _camera.fieldOfView = originalFOV;
    }
}
