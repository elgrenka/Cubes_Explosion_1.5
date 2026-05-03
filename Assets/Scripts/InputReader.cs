using System;
using UnityEngine;
//using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    //public UnityEvent<Vector2> OnLeftMouseButtonPressed = new UnityEvent<Vector2>();
    public event Action<Vector2> OnLeftMouseButtonPressed;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Клик есть.");
            OnLeftMouseButtonPressed?.Invoke(Mouse.current.position.value);
        }
    }
}