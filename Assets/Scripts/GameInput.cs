using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
    }

    public Vector3 GetDirection()
    {
        var direction = inputActions.Player.Move.ReadValue<Vector2>();
        var result = new Vector3(direction.x, 0, direction.y);
        return result.normalized;
    }
}
