using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    ActionMap actionMap;

    public UnityEvent OnJumpDown;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        actionMap = new ActionMap();
        actionMap.Enable();
        actionMap.BasicActions.Jump.started += InvokeOnJumpDown;

    }

    public void InvokeOnJumpDown(InputAction.CallbackContext obj)
    {
        OnJumpDown?.Invoke();
    }

    public Vector2 GetMoveVector()
    {
        return actionMap.BasicActions.Movement.ReadValue<Vector2>();
    }

}
