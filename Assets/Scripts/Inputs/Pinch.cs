using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinch : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float minFOV = 10f;
    [SerializeField] private float maxFOV = 20f;

    void Start()
    {
        _camera = Camera.main;
    }

    void OnEnable()
    {
        LeanTouch.OnGesture += HandlePinch;
    }

    void OnDisable()
    {
        LeanTouch.OnGesture -= HandlePinch;
    }

    void HandlePinch(List<LeanFinger> fingers)
    {
        if (LeanGesture.GetPinchRatio(fingers) != 1.0f)
        {
            float pinchRatio = LeanGesture.GetPinchRatio(fingers);
            float newFOV = _camera.fieldOfView / pinchRatio;

            _camera.fieldOfView = Mathf.Clamp(newFOV, minFOV, maxFOV);
        }
    }

}
