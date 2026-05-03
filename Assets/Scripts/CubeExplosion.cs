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

    public void ApplyExplosion(List<Cube> cubes, Vector3 center)
    {
        PlayEffect(center);

        foreach (Cube cube in cubes)
        {
            if (cube == null)
                continue;

            //Rigidbody rb = cube.GetComponent<Rigidbody>();

            //if (rb == null)
            //    continue;

            //float distance = Vector3.Distance(cube.transform.position, center);

            //if (distance > _explosionRadius)
            //    continue;

            cube.ApplyExplosionForce(_explosionForce, center, _explosionRadius, _upwardsModifier);
        }
    }

    private void PlayEffect(Vector3 position)
    {
        if (_effect == null)
            return;

        GameObject effect = Instantiate(_effect, position, Quaternion.identity);

        Destroy(effect, _effectDuration);
    }
}