using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : MonoBehaviour
{
    [SerializeField] private Material primaryMaterial;
    [SerializeField] private Material secondaryMaterial;

    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        ColorChange.OnChangeColor += ChangeMaterial;
    }
    void ChangeMaterial()
    {
        if (_renderer.material.name != secondaryMaterial.name + " (Instance)")
        {
            _renderer.material = secondaryMaterial;
            return;
        } 
        if (_renderer.material.name != primaryMaterial.name + " (Instance)")
        {
            _renderer.material = primaryMaterial;
        }
    }

    private void OnDestroy()
    {
        ColorChange.OnChangeColor -= ChangeMaterial;
    }
}
