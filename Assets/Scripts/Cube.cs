using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public float SplitChance { get; private set; }

    public void Initialize(
        Vector3 scale,
        float splitChance,
        bool isUseGravity = true,
        bool isRandomColor = true
    )
    {
        transform.localScale = scale;
        SplitChance = splitChance;
        Rigidbody.useGravity = isUseGravity;

        if (isRandomColor)
            SetRandomColor();
    }

    public void SetRandomColor()
    {
        if (_renderer is null)
            return;

        _renderer.material = new Material(_renderer.material)
        {
            color = Random.ColorHSV()
        };
    }
}