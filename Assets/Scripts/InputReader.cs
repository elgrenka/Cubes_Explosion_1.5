using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public UnityEvent<Vector2> OnLeftMouseButtonPressed = new UnityEvent<Vector2>();

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnLeftMouseButtonPressed?.Invoke(Mouse.current.position.ReadValue());
        }
    }
}