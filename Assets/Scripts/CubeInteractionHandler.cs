using System.Collections.Generic;
using UnityEngine;

public class CubeInteractionHandler : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private CubeExplosion _cubeExplosion;

    public void HandleCubeClick(GameObject clickedCube, Vector3 hitPoint)
    {
        CubeData cubeData = clickedCube.GetComponent<CubeData>();
        int generation = cubeData?.Generation ?? 0;

        bool shouldSplit = CalculateSplitChance(generation);

        if (shouldSplit)
        {
            Vector3 originalPosition = clickedCube.transform.position;
            Vector3 originalScale = clickedCube.transform.localScale;

            List<GameObject> spawnedCubes = _cubeFactory.SpawnSplitCubes(
                originalPosition,
                originalScale * 0.5f,
                generation + 1
            );

            ApplyExplosionToSpawnedCubes(spawnedCubes, originalPosition);

            Destroy(clickedCube);
        }
        else
        {
            Destroy(clickedCube);
        }
    }

    private void ApplyExplosionToSpawnedCubes(List<GameObject> spawnedCubes, Vector3 explosionCenter)
    {
        foreach (GameObject cube in spawnedCubes)
        {
            if (cube.TryGetComponent<Rigidbody>(out var rb))
            {
                Vector3 direction = (cube.transform.position - explosionCenter).normalized;
                float distance = Vector3.Distance(cube.transform.position, explosionCenter);

                float forceMultiplier = Mathf.Clamp01(1f - distance / 5f);
                rb.AddForce(direction * 500f * forceMultiplier, ForceMode.Impulse);
            }
        }

        PlayExplosionEffect(explosionCenter);
    }

    private void PlayExplosionEffect(Vector3 position)
    {
        if (_cubeExplosion != null)
        {
            _cubeExplosion.PlayExplosionEffect(position);
        }
    }

    private bool CalculateSplitChance(int generation)
    {
        float splitProbability = Mathf.Pow(0.5f, generation);
        return Random.value <= splitProbability;
    }
}