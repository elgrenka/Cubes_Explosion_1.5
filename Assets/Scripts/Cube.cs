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
        //Vector3 initialForce,
        bool isUseGravity = true,
        bool isRandomColor = true
    )
    {
        transform.localScale = scale;
        SplitChance = splitChance;
        Rigidbody.useGravity = isUseGravity;

        if (isRandomColor)
            SetRandomColor();

        //if (initialForce != Vector3.zero)
        //    AddInitialForce(initialForce);
    }

    //public void SetScale(Vector3 scale)
    //{
    //    transform.localScale = scale;
    //}

    public void SetRandomColor()
    {
        if (_renderer is null)
            return;

        _renderer.material = new Material(_renderer.material)
        {
            color = Random.ColorHSV()
        };
    }

    //public void EnableGravity()
    //{
    //    if (Rigidbody is null)
    //        return;

    //    Rigidbody.useGravity = true;
    //}

    //public void AddInitialForce(Vector3 force)
    //{
    //    Rigidbody?.AddForce(force, ForceMode.Impulse);
    //}

    //public void ApplyExplosionForce(float force, Vector3 center, float radius, float upwardsModifier)
    //{
    //    Rigidbody?.AddExplosionForce(force, center, radius, upwardsModifier, ForceMode.Impulse);
    //}
}