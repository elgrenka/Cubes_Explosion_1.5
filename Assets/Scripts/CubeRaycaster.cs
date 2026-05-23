using System;
using UnityEngine;

public class CubeRaycaster : MonoBehaviour
{
    [SerializeField] private int _terrainLayer = 10;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputReader _inputReader;

    public event Action<Cube, Vector3> OnCubeHit;

    private void OnEnable()
    {
        if (_inputReader is not null)
            _inputReader.OnLeftMouseButtonPressed += HandleMouseClick;
    }

    private void OnDisable()
    {
        if (_inputReader is not null)
            _inputReader.OnLeftMouseButtonPressed -= HandleMouseClick;
    }

    private void HandleMouseClick(Vector2 mousePosition)
    {
        if (_mainCamera is null)
            return;

        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;

        GameObject hitObject = hit.collider.gameObject;

        if (hitObject.TryGetComponent(out Cube cube) == false)
            return;

        OnCubeHit?.Invoke(cube, hit.point);
    }
}