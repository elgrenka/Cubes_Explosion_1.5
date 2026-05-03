using System.Collections.Generic;
using UnityEngine;

public class CubeInteractionHandler : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private CubeExplosion _cubeExplosion;

    private void Awake()
    {
        Debug.Log("CubeInteractionHandler.Awake()");

        var raycaster = FindAnyObjectByType<CubeRaycaster>();

        if (raycaster == null)
        {
            Debug.LogError("CubeInteractionHandler: CubeRaycaster не найден!");
        }
        else
        {
            Debug.Log("CubeInteractionHandler: подписываемся на OnCubeHit");
            raycaster.OnCubeHit += HandleCubeClick;
        }
    }

    private void OnDestroy()
    {
        var raycaster = FindAnyObjectByType<CubeRaycaster>();

        if (raycaster != null)
            raycaster.OnCubeHit -= HandleCubeClick;
    }

    public void HandleCubeClick(GameObject clickedCube, Vector3 hitPoint)
    {
        Debug.Log("HandleCubeClick ВЫЗВАН для куба: " + clickedCube.name);

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