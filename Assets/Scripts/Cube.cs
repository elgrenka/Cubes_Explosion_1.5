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

    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();

        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
    }

    public void Initialize(int generation)
    {
        Generation = generation;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void SetRandomColor()
    {
        if (_renderer == null)
            return;

        _renderer.material = new Material(_renderer.material)
        {
            color = Random.ColorHSV()
        };
    }

    public void EnableGravity()
    {
        if (_rigidbody == null)
            return;

        _rigidbody.useGravity = true;
    }

    public void AddInitialForce(Vector3 force)
    {
        if (_rigidbody == null)
            return;

        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void ApplyExplosionForce(float force, Vector3 center, float radius, float upwardsModifier)
    {
        if (_rigidbody == null)
            return;

        _rigidbody.AddExplosionForce(force, center, radius, upwardsModifier, ForceMode.Impulse);
    }
}