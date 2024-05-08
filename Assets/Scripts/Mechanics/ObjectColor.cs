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

    private void OnEnable()
    {
        ColorChange.OnChangeColor += ChangeMaterial;
    }

    private void OnDisable()
    {
        ColorChange.OnChangeColor -= ChangeMaterial;
    }
}
