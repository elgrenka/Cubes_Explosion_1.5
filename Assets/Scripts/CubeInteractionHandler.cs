using System.Collections.Generic;
using UnityEngine;

public class CubeInteractionHandler : MonoBehaviour
{
    [SerializeField] private CubeFactory _factory;
    [SerializeField] private CubeExplosion _explosion;

    private void Awake()
    {
        CubeRaycaster raycaster = FindAnyObjectByType<CubeRaycaster>();

        if (raycaster != null)
            raycaster.OnCubeHit += HandleClick;
        else
            Debug.LogError("CubeRaycaster не найден");
    }

    private void OnDestroy()
    {
        CubeRaycaster raycaster = FindAnyObjectByType<CubeRaycaster>();

        if (raycaster != null)
            raycaster.OnCubeHit -= HandleClick;
    }

    public void HandleClick(Cube cube, Vector3 hitPoint)
    {
        int generation = cube.Generation;

        //Debug.Log("HandleCubeClick ВЫЗВАН для куба: " + clickedCube.name);
        //CubeData cubeData = clickedCube.GetComponent<CubeData>();
        //bool shouldSplit = CalculateSplitChance(generation);

        if (ShouldSplit(generation))
        {
            Vector3 position = cube.transform.position;
            Vector3 scale = cube.transform.localScale * 0.5f;

            List<Cube> newCubes = _factory.SpawnSplitCubes(position, scale, generation + 1);

            _explosion.ApplyExplosion(newCubes, position);

        }

        Destroy(cube.gameObject);

        //else
        //{
        //    Destroy(clickedCube);
        //}
    }

    private bool ShouldSplit(int generation)
    {
        float chance = Mathf.Pow(0.5f, generation);
        return Random.value <= chance;
    }

    //private void ApplyExplosionToSpawnedCubes(List<GameObject> spawnedCubes, Vector3 explosionCenter)
    //{
    //    _cubeExplosion.ApplyExplosionToObjects(spawnedCubes, explosionCenter);
    //}

    //private bool CalculateSplitChance(int generation)
    //{
    //    float splitProbability = Mathf.Pow(0.5f, generation);
    //    return Random.value <= splitProbability;
    //}
}