using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public event Action<Vector2> OnLeftMouseButtonPressed;

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnLeftMouseButtonPressed?.Invoke(Mouse.current.position.value);
        }
    }
}