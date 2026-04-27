using System.Collections.Generic;
using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [Header("Настройки появления")]
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private float _spawnRadius = 3f;
    [SerializeField] private float _initialForce = 5f;
    [SerializeField] private ParticleSystem _effect;

    public List<GameObject> SpawnSplitCubes(Vector3 position, Vector3 scale, int generation)
    {
        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        List<GameObject> spawnedCubes = new List<GameObject>();

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject cube = CreateCube(position, scale, generation);
            spawnedCubes.Add(cube);
        }

        return spawnedCubes;
    }

    private GameObject CreateCube(Vector3 basePosition, Vector3 scale, int generation)
    {
        Vector3 spawnOffset = Random.insideUnitSphere * _spawnRadius;
        Vector3 spawnPosition = basePosition + spawnOffset;

        GameObject cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
        cube.transform.localScale = scale;

        SetupCubeData(cube, generation);
        SetupCubePhysics(cube);
        SetupCubeVisuals(cube);

        return cube;
    }

    private void SetupCubeData(GameObject cube, int generation)
    {
        CubeData cubeData = cube.GetComponent<CubeData>() ?? cube.AddComponent<CubeData>();
        cubeData.Generation = generation;
    }

    private void SetupCubePhysics(GameObject cube)
    {
        Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>() ?? cube.AddComponent<Rigidbody>();
        cubeRigidbody.AddForce(Random.insideUnitSphere * _initialForce, ForceMode.Impulse);

        cubeRigidbody.useGravity = true;
    }

    private void SetupCubeVisuals(GameObject cube)
    {
        Renderer cubeRenderer = cube.GetComponent<Renderer>();

        if (cubeRenderer is null)
            return;

        cubeRenderer.material = new Material(cubeRenderer.material)
        {
            color = Random.ColorHSV()
        };
    }
}