using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    //public UnityEvent<Vector2> OnLeftMouseButtonPressed = new UnityEvent<Vector2>();
    public event Action<Vector2> OnClick;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Клик есть.");
            OnClick?.Invoke(Mouse.current.position.value);
        }
    }
}