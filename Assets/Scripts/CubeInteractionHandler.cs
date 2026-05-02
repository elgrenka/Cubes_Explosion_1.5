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
        _cubeExplosion.ApplyExplosionToObjects(spawnedCubes, explosionCenter);
    }

    private bool CalculateSplitChance(int generation)
    {
        float splitProbability = Mathf.Pow(0.5f, generation);
        return Random.value <= splitProbability;
    }
}