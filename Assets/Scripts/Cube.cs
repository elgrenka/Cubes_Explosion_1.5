using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Renderer _renderer;

    [field: SerializeField] public int Generation { get; private set; }

    public Rigidbody Rigidbody => _rigidbody;

    public void Initialize(
        int generation,
        Vector3 scale,
        Vector3 initialForce,
        bool useGravity = true,
        bool isRandomColor = true
    )
    {
        Generation = generation;
        transform.localScale = scale;

        if (useGravity && _rigidbody)
            _rigidbody.useGravity = true;

        if (isRandomColor)
            SetRandomColor();

        if (initialForce != Vector3.zero)
            AddInitialForce(initialForce);
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
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

    public void EnableGravity()
    {
        if (_rigidbody is null)
            return;

        _rigidbody.useGravity = true;
    }

    public void AddInitialForce(Vector3 force)
    {
        _rigidbody?.AddForce(force, ForceMode.Impulse);
    }

    public void ApplyExplosionForce(float force, Vector3 center, float radius, float upwardsModifier)
    {
        _rigidbody?.AddExplosionForce(force, center, radius, upwardsModifier, ForceMode.Impulse);
    }
}