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

    void ChangeMaterial(bool originalColor)
    {
        if (originalColor)
        {
            _renderer.material = primaryMaterial;
        }
        else
        {
            _renderer.material = secondaryMaterial;
        }
    }

    private void OnDestroy()
    {
        ColorChange.OnChangeColor -= ChangeMaterial;
    }
}
