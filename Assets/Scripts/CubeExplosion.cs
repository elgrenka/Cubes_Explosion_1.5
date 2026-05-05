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

    public void ApplyExplosion(List<Cube> cubes, Vector3 explosionCenter)
    {
        PlayExplosionEffect(explosionCenter);

        if (cubes == null)
            return;

        foreach (Cube cube in cubes)
        {
            cube?.ApplyExplosionForce(
                _explosionForce,
                explosionCenter,
                _explosionRadius,
                _upwardsModifier
            );
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