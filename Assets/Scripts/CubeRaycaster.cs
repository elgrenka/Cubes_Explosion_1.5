using UnityEngine;
using UnityEngine.InputSystem;

public class CubeRaycaster : MonoBehaviour
{
    [SerializeField] private int _terrainLayer = 10;
    [SerializeField] private Camera _camera;
    [SerializeField] private InputReader _input;

    public event System.Action<Cube, Vector3> OnCubeHit;

    //[System.Serializable]
    //public class CubeHitEvent : UnityEvent<GameObject, Vector3> { }
    //public CubeHitEvent OnCubeHit = new CubeHitEvent();

    private void Awake()
    {
        Debug.Log("CubeRaycaster.Awake()");

        if (_camera == null)
            _camera = Camera.main;

        if (_input == null)
            _input = FindAnyObjectByType<InputReader>();

        if (_input != null)
        {
            _input.OnClick += HandleClick;
        }
        //else
        //{
        //    Debug.Log("CubeRaycaster: InputReader найден, подписываемся");
        //    _input.OnLeftMouseButtonPressed += HandleMouseClick;
        //}

        //_inputReader.OnLeftMouseButtonPressed += HandleMouseClick;
    }

    private void OnDestroy()
    {
        if (_input != null)
            _input.OnClick -= HandleClick;
    }


    private void HandleClick(Vector2 screenPosition)
    {
        //Debug.Log("CubeRaycaster.HandleMouseClick вызван, позиция: " + screenPosition);


        //if (_camera == null)
        //{
        //    Debug.LogError("CubeRaycaster: камера не назначена и Camera.main не найдена. " +
        //        "Луч не может быть построен.");
        //    return;
        //}

        Ray ray = _camera.ScreenPointToRay(screenPosition);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);


        if (Physics.Raycast(ray, out RaycastHit hit) == false)
        {
            Debug.Log("Луч ни во что не попал");
            return;
        }

        if (hit.collider.gameObject.layer == _terrainLayer)
            return;

        if (hit.collider.TryGetComponent(out Cube cube))
        {
            OnCubeHit?.Invoke(cube, hit.point);
        }

        //GameObject clickedObject = hit.collider.gameObject;

        //if (IsTerrain(clickedObject))
        //    return;

        //if (clickedObject.TryGetComponent<CubeData>(out _) == false)
        //    return;


        Debug.Log("OnCubeHit вызван, подписчиков: " + (OnCubeHit != null ? "есть" : "нет"));
    }

    //private bool IsTerrain(GameObject targetObject)
    //{
    //    return targetObject.layer == _terrainLayer;
    //}
}