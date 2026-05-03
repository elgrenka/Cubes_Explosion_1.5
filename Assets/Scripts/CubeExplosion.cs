using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [Header("Настройки взрыва")]
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _explosionRadius = 50f;
    [SerializeField] private float _upwardsModifier = 0f;
    [SerializeField] private GameObject _effect;
    [SerializeField] private float _effectDuration = 4f;

    public void ApplyExplosionToObjects(List<GameObject> targetObjects, Vector3 explosionCenter)
    {
        PlayExplosionEffect(explosionCenter);

        foreach (GameObject obj in targetObjects)
        {
            if (obj == null)
                continue;

            Rigidbody rb = obj.GetComponent<Rigidbody>();

            if (rb == null)
                continue;

            float distance = Vector3.Distance(obj.transform.position, explosionCenter);

            if (distance > _explosionRadius)
                continue;

            rb.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius,
                                 _upwardsModifier, ForceMode.Impulse);
        }
    }

    private void PlayExplosionEffect(Vector3 position)
    {
        if (_effect == null)
            return;

        GameObject explosion = Instantiate(_effect, position, Quaternion.identity);

        Destroy(explosion, _effectDuration);
    }
}