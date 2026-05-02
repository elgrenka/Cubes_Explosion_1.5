using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public UnityEvent<Vector2> OnLeftMouseButtonPressed = new UnityEvent<Vector2>();

    private void Start()
    {
        if (OnLeftMouseButtonPressed == null)
            Debug.LogError("На событие OnLeftMouseButtonPressed никто не подписан!");
        else
            Debug.Log("Подписчиков: " + OnLeftMouseButtonPressed.GetPersistentEventCount());
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnLeftMouseButtonPressed?.Invoke(Mouse.current.position.value);
            Debug.Log("Событие вызвано.");
        }
    }
}