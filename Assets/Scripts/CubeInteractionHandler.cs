using System.Collections.Generic;
using UnityEngine;

public class CubeInteractionHandler : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private CubeExplosion _cubeExplosion;
    [SerializeField] private CubeRaycaster _cubeRaycaster;

    private const float ChildSplitChanceMultiplier = 0.5f;

    private void OnEnable()
    {
        if (_cubeRaycaster is not null)
            _cubeRaycaster.OnCubeHit += HandleCubeClick;
    }

    private void OnDisable()
    {
        if (_cubeRaycaster is not null)
            _cubeRaycaster.OnCubeHit -= HandleCubeClick;
    }

    private void HandleCubeClick(Cube clickedCube, Vector3 hitPoint)
    {
        if (clickedCube is null)
            return;

        bool shouldSplit = Random.value <= clickedCube.SplitChance;
        Vector3 position = clickedCube.transform.position;

        if (shouldSplit)
        {
            Vector3 scale = clickedCube.transform.localScale;

            List<Cube> spawnedCubes = _cubeFactory.SpawnSplitCubes(
                position,
                scale * ChildSplitChanceMultiplier,
                clickedCube.SplitChance
            );

            _cubeExplosion.ApplyExplosion(spawnedCubes, position);
        }
        else
        {
            _cubeExplosion.ExplodeFromCube(clickedCube);
        }

        Destroy(clickedCube.gameObject);
    }
}