using System.Collections.Generic;
using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [Header("Настройки появления")]
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private float _spawnRadius = 3f;
    [SerializeField] private float _initialForce = 5f;

    public List<Cube> SpawnSplitCubes(Vector3 position, Vector3 scale, int generation)
    {
        int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        List<Cube> cubes = new List<Cube>(count);

        for (int i = 0; i < count; i++)
        {
            cubes.Add(CreateCube(position, scale, generation));
        }

        return cubes;
    }

    private Cube CreateCube(Vector3 basePosition, Vector3 scale, int generation)
    {
        Vector3 spawnOffset = Random.insideUnitSphere * _spawnRadius;
        Vector3 spawnPosition = basePosition + spawnOffset;
        Vector3 initialForce = Random.insideUnitSphere * _initialForce;

        Cube cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

        cube.Initialize(generation, scale, initialForce, true, true);

        return cube;
    }
}