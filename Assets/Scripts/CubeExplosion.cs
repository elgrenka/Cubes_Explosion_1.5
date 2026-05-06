using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [Header("Настройка взрыва при разделении")]
    [SerializeField] private float _splitExplosionForce = 100f;
    [SerializeField] private float _splitExplosionRadius = 50f;

    [Header("Настройки взрыва при неудаче")]
    [SerializeField] private float _failBaseForce = 200f;
    [SerializeField] private float _failBaseRadius = 20f;
    [SerializeField] private float _failUpwardsModifier = 0f;

    [Header("Общие настройки")]
    [SerializeField] private GameObject _effect;
    [SerializeField] private float _effectDuration = 4f;
    [SerializeField] private LayerMask _affectedLayers = -1;

    public void ApplyExplosion(List<Cube> cubes, Vector3 explosionCenter)
    {
        PlayExplosionEffect(explosionCenter);

        if (cubes == null)
            return;

        foreach (Cube cube in cubes)
        {
            cube?.ApplyExplosionForce(_splitExplosionForce, explosionCenter, _splitExplosionRadius, 0f);
        }
    }

    public void ExplodeFromCube(Cube sourceCube)
    {
        if (sourceCube == null)
            return;

        Vector3 center = sourceCube.transform.position;
        float cubeScale = Mathf.Max(sourceCube.transform.localScale.x, 0.1f);
        float force = _failBaseForce / cubeScale;
        float radius = _failBaseRadius / cubeScale;

        PlayExplosionEffect(center);


        Collider[] hitColliders = Physics.OverlapSphere(center, radius, _affectedLayers);

        //Debug.DrawRay(center, Vector3.up * radius, Color.red, 2f);
        Debug.Log($"Explosion: found {hitColliders.Length} colliders within radius {radius}");

        foreach (Collider collider in hitColliders)
        {
            Rigidbody rb = collider.attachedRigidbody;

            //if (rb != null && rb != sourceCube.Rigidbody)
            //{
            //    rb.AddExplosionForce(force, center, radius, _failUpwardsModifier, ForceMode.Impulse);
            //}

            if (rb != null)
            {
                if (rb != sourceCube.Rigidbody)
                {
                    rb.AddExplosionForce(force, center, radius, _failUpwardsModifier, ForceMode.Impulse);
                    Debug.Log($"Applied force to {rb.name}, force={force}");
                }
                else
                {
                    Debug.Log($"Skipping source cube: {rb.name}");
                }
            }
            else
            {
                Debug.Log($"Collider {collider.name} has no Rigidbody");
            }
        }
    }

    private void PlayExplosionEffect(Vector3 position)
    {
        if (_effect is null)
            return;

        GameObject effect = Instantiate(_effect, position, Quaternion.identity);

        Destroy(effect, _effectDuration);
    }
}