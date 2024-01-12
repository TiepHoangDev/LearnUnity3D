using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    PlayerInputActions inputActions;

    public event EventHandler OnInteraction;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interacs.performed += Interacs_performed;
    }

    private void Interacs_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteraction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetDirection()
    {
        var direction = inputActions.Player.Move.ReadValue<Vector2>();
        var result = new Vector3(direction.x, 0, direction.y);
        return result.normalized;
    }
}
