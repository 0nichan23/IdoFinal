using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    BasicActions basicActions;
    public UnityEvent OnAttack;
    public UnityEvent OnTurnRight;
    public UnityEvent OnTurnLeft;

    private void Start()
    {
        basicActions = new BasicActions();
        basicActions.Enable();
        basicActions.Actions.Attack.started += InvokeOnAttack;
        basicActions.Actions.TurnRight.started += InvokeOnTurnRight;
        basicActions.Actions.TurnLeft.started += InvokeOnTurnLeft;
    }

    public void InvokeOnAttack(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }
    public void InvokeOnTurnRight(InputAction.CallbackContext obj)
    {
        OnTurnRight?.Invoke();
    }
    public void InvokeOnTurnLeft(InputAction.CallbackContext obj)
    {
        OnTurnLeft?.Invoke();
    }

    public Vector3Int GetMoveVector()
    {
        Vector3Int dir = new Vector3Int(Mathf.FloorToInt(basicActions.Actions.Movement.ReadValue<Vector2>().x), 0, Mathf.FloorToInt(basicActions.Actions.Movement.ReadValue<Vector2>().y));
        return dir;
    }

}
