using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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
        Debug.Log("HandleMouseClick ВЫЗВАН с позицией: " + mousePosition);

        if (_mainCamera == null)
        {
            Debug.LogError("_mainCamera = null! У камеры нет тега MainCamera?");
            return;
        }

        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);


        if (Physics.Raycast(ray, out RaycastHit hit) == false)
        {
            Debug.Log("Луч ни во что не попал");
            return;
        }

        Debug.Log("Попали в объект: " + hit.collider.name);


        GameObject clickedObject = hit.collider.gameObject;

        if (IsTerrain(clickedObject))
            return;

        OnCubeHit?.Invoke(clickedObject, hit.point);

        Debug.Log("Клик есть");
    }

    private bool IsTerrain(GameObject targetObject)
    {
        return targetObject.layer == _terrainLayer;
    }
}