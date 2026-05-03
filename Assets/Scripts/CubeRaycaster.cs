using UnityEngine;
//using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CubeRaycaster : MonoBehaviour
{
    [SerializeField] private int _terrainLayer = 10;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputReader _inputReader;

    public event System.Action<GameObject, Vector3> OnCubeHit;

    //[System.Serializable]
    //public class CubeHitEvent : UnityEvent<GameObject, Vector3> { }
    //public CubeHitEvent OnCubeHit = new CubeHitEvent();

    private void Awake()
    {
        Debug.Log("CubeRaycaster.Awake()");

        if (_mainCamera == null)
            _mainCamera = Camera.main;

        if (_inputReader == null)
            _inputReader = FindAnyObjectByType<InputReader>();

        if (_inputReader == null)
        {
            Debug.LogError("CubeRaycaster: InputReader не найден на сцене!");
            return;
        }
        else
        {
            Debug.Log("CubeRaycaster: InputReader найден, подписываемся");
            _inputReader.OnLeftMouseButtonPressed += HandleMouseClick;
        }

        //_inputReader.OnLeftMouseButtonPressed += HandleMouseClick;
    }

    private void OnDestroy()
    {
        if (_inputReader != null)
            _inputReader.OnLeftMouseButtonPressed -= HandleMouseClick;
    }


    private void HandleMouseClick(Vector2 mousePosition)
    {
        Debug.Log("CubeRaycaster.HandleMouseClick вызван, позиция: " + mousePosition);


        if (_mainCamera == null)
        {
            Debug.LogError("CubeRaycaster: камера не назначена и Camera.main не найдена. " +
                "Луч не может быть построен.");
            return;
        }

        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);


        if (Physics.Raycast(ray, out RaycastHit hit) == false)
        {
            Debug.Log("Луч ни во что не попал");
            return;
        }

        GameObject clickedObject = hit.collider.gameObject;

        if (IsTerrain(clickedObject))
            return;

        if (clickedObject.TryGetComponent<CubeData>(out _) == false)
            return;

        OnCubeHit?.Invoke(clickedObject, hit.point);

        Debug.Log("OnCubeHit вызван, подписчиков: " + (OnCubeHit != null ? "есть" : "нет"));
    }

    private bool IsTerrain(GameObject targetObject)
    {
        return targetObject.layer == _terrainLayer;
    }
}