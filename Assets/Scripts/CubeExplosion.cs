using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [Header("Настройка взрыва при разделении")]
    [SerializeField] private float _splitExplosionForce = 100f;
    [SerializeField] private float _splitExplosionRadius = 50f;

    [Header("Настройки взрыва при неудаче")]
    [SerializeField] private float _failBaseForce = 500f;
    [SerializeField] private float _failBaseRadius = 200f;
    [SerializeField] private float _failUpwardsModifier = 0f;

    [Header("Визуальные эффекты")]
    [SerializeField] private ParticleSystem _successEffectPrefab;
    [SerializeField] private ParticleSystem _failEffectPrefab;
    [SerializeField] private float _effectDuration = 4f;

    [Header("Звуковые эффекты")]
    [SerializeField] private AudioClip _successSound;
    [SerializeField] private AudioClip _failSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField][Range(0f, 1f)] private float _soundVolume = 1f;

    [Header("Общие настройки")]
    [SerializeField] private LayerMask _affectedLayers = -1;

    private const float MinCubeScale = 0.1f;
    private const float UpwardsModifier = 0f;

    public void ApplyExplosion(List<Cube> cubes, Vector3 explosionCenter)
    {
        PlayExplosionEffect(_successEffectPrefab, explosionCenter);
        PlayExplosionSound(_successSound);

        if (cubes == null)
            return;

        foreach (Cube cube in cubes)
        {
            if (cube?.Rigidbody != null)
            {
                cube.Rigidbody.AddExplosionForce(
                    _splitExplosionForce,
                    explosionCenter,
                    _splitExplosionRadius,
                    UpwardsModifier,
                    ForceMode.Impulse
                );

            }
        }
    }

    public void ExplodeFromCube(Cube sourceCube)
    {
        if (sourceCube == null)
            return;

        Vector3 center = sourceCube.transform.position;
        float cubeScale = Mathf.Max(sourceCube.transform.localScale.x, MinCubeScale);
        float force = _failBaseForce / cubeScale;
        float radius = _failBaseRadius / cubeScale;

        PlayExplosionEffect(_failEffectPrefab, center);
        PlayExplosionSound(_failSound);

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, _affectedLayers);

        foreach (Collider collider in hitColliders)
        {
            Rigidbody hitRigidbody = collider.attachedRigidbody;

            if (hitRigidbody != null && hitRigidbody != sourceCube.Rigidbody)
            {
                hitRigidbody.AddExplosionForce(force, center, radius, _failUpwardsModifier, ForceMode.Impulse);
            }
        }
    }

    private void PlayExplosionEffect(ParticleSystem effectPrefab, Vector3 position)
    {
        if (effectPrefab is null)
            return;

        ParticleSystem effect = Instantiate(effectPrefab, position, Quaternion.identity);

        Destroy(effect.gameObject, _effectDuration);
    }

    private void PlayExplosionSound(AudioClip clip)
    {
        if (clip is null || _audioSource is null)
            return;

        _audioSource.PlayOneShot(clip, _soundVolume);
    }
}