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
    //[SerializeField] private ParticleSystem _effect;

    public List<Cube> SpawnSplitCubes(Vector3 position, Vector3 scale, int generation)
    {
        int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        List<Cube> cubes = new List<Cube>();

        for (int i = 0; i < count; i++)
        {
            cubes.Add(CreateCube(position, scale, generation));
        }

        return cubes;
    }

    private Cube CreateCube(Vector3 basePosition, Vector3 scale, int generation)
    {
        Vector3 offset = Random.insideUnitSphere * _spawnRadius;
        Vector3 spawnPosition = basePosition + offset;

        Cube cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

        cube.SetScale(scale);
        cube.Generation = generation;
        cube.SetRandomColor();
        cube.EnablePhysics(Random.insideUnitSphere * _initialForce);

        //SetupCubeData(cube, generation);
        //SetupCubePhysics(cube);
        //SetupCubeVisuals(cube);

        return cube;
    }

    //private void SetupCubeData(GameObject cube, int generation)
    //{
    //    CubeData cubeData = cube.GetComponent<CubeData>() ?? cube.AddComponent<CubeData>();
    //    cubeData.Generation = generation;
    //}

    //private void SetupCubePhysics(GameObject cube)
    //{
    //    Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>() ?? cube.AddComponent<Rigidbody>();
    //    cubeRigidbody.AddForce(Random.insideUnitSphere * _initialForce, ForceMode.Impulse);

    //    cubeRigidbody.useGravity = true;
    //}

    //private void SetupCubeVisuals(GameObject cube)
    //{
    //    Renderer cubeRenderer = cube.GetComponent<Renderer>();

    //    if (cubeRenderer is null)
    //        return;

    //    cubeRenderer.material = new Material(cubeRenderer.material)
    //    {
    //        color = Random.ColorHSV()
    //    };
    //}
}