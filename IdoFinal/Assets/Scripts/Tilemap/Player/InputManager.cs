using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    BasicActions basicActions;


    public UnityEvent OnAttackPanelOpen;

    private void Start()
    {
        basicActions = new BasicActions();
        basicActions.Enable();
        basicActions.Actions.AttackPanel.started += InvokeOnAttackPanelOpen;
    }

    public void InvokeOnAttackPanelOpen(InputAction.CallbackContext obj)
    {
        OnAttackPanelOpen?.Invoke();
    }

    public Vector3Int GetMoveVector()
    {
        Vector3Int dir = new Vector3Int(Mathf.FloorToInt(basicActions.Actions.Movement.ReadValue<Vector2>().x), 0, Mathf.FloorToInt(basicActions.Actions.Movement.ReadValue<Vector2>().y));
        return dir;
    }

}
