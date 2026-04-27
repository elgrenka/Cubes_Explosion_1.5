using UnityEngine;
using UnityEngine.Events;

public class CubeRaycaster : MonoBehaviour
{
    [SerializeField] private int _terrainLayer = 10;
    [SerializeField] private Camera _mainCamera;

    [System.Serializable]
    public class CubeHitEvent : UnityEvent<GameObject, Vector3> { }
    public CubeHitEvent OnCubeHit = new CubeHitEvent();

    private void Awake()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    public void HandleMouseClick(Vector2 mousePosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;

        GameObject clickedObject = hit.collider.gameObject;

        if (IsTerrain(clickedObject))
            return;

        OnCubeHit?.Invoke(clickedObject, hit.point);
    }

    private bool IsTerrain(GameObject targetObject)
    {
        return targetObject.layer == _terrainLayer;
    }
}