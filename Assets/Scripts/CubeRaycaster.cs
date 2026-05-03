using System;
using UnityEngine;

public class CubeRaycaster : MonoBehaviour
{
    [SerializeField] private int _terrainLayer = 10;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputReader _inputReader;

    public event Action<Cube, Vector3> OnCubeHit;

    private void Awake()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;

        if (_inputReader == null)
            _inputReader = FindAnyObjectByType<InputReader>();
    }

    private void OnEnable()
    {
        if (_inputReader != null)
            _inputReader.OnLeftMouseButtonPressed += HandleMouseClick;
    }

    private void OnDisable()
    {
        if (_inputReader != null)
            _inputReader.OnLeftMouseButtonPressed -= HandleMouseClick;
    }

    private void HandleMouseClick(Vector2 mousePosition)
    {
        if (_mainCamera == null)
            return;

        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);

        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;

        GameObject hitObject = hit.collider.gameObject;

        if (hitObject.layer == _terrainLayer)
            return;

        if (hitObject.TryGetComponent(out Cube cube) == false)
            return;

        OnCubeHit?.Invoke(cube, hit.point);
    }
}