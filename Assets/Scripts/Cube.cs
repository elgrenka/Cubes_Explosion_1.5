using UnityEngine;

[DisallowMultipleComponent]

public class Cube : MonoBehaviour
{
    [field: SerializeField] public int Generation { get; set; }

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Renderer _renderer;

    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();

        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
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

    public void EnablePhysics(Vector3 initialForce)
    {
        if (_rigidbody == null)
            return;

        _rigidbody.useGravity = true;
        _rigidbody.AddForce(initialForce, ForceMode.Impulse);
    }

    public void ApplyExplosionForce(float force, Vector3 center, float radius, float upwardsModifier)
    {
        if (_rigidbody == null)
            return;

        _rigidbody.AddExplosionForce(force, center, radius, upwardsModifier, ForceMode.Impulse);
    }
}