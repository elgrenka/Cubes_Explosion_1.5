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

    public List<Cube> SpawnSplitCubes(Vector3 position, Vector3 scale, float parentSplitChance)
    {
        int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        float childSplitChance = parentSplitChance * 0.5f;

        List<Cube> cubes = new List<Cube>(count);

        for (int i = 0; i < count; i++)
        {
            cubes.Add(CreateCube(position, scale, childSplitChance));
        }

        return cubes;
    }

    private Cube CreateCube(Vector3 basePosition, Vector3 scale, float splitChance)
    {
        Vector3 spawnOffset = Random.insideUnitSphere * _spawnRadius;
        Vector3 spawnPosition = basePosition + spawnOffset;

        Cube cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

        cube.Initialize(scale, splitChance, isUseGravity: true, isRandomColor: true);

        Vector3 initialForce = Random.insideUnitSphere * _initialForce;
        cube.Rigidbody.AddForce(initialForce, ForceMode.Impulse);

        return cube;
    }
}