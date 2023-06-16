//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Tilemap/Player/BasicActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @BasicActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @BasicActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BasicActions"",
    ""maps"": [
        {
            ""name"": ""Actions"",
            ""id"": ""cff62028-d880-4dbf-a662-799011280087"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""0255ab93-8dbf-4b03-9037-3650d314c30a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""4fda58f6-f95f-4046-b130-fd5c4ba4a5fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TeamPanel"",
                    ""type"": ""Button"",
                    ""id"": ""5353cd44-de7b-4ba7-86da-8aad4ca64834"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TurnRight"",
                    ""type"": ""Button"",
                    ""id"": ""26c88619-34a7-49a0-930e-93b8dcabfecb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TurnLeft"",
                    ""type"": ""Button"",
                    ""id"": ""66f7ba45-ee3a-4a98-bb2a-e7a834568cdc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchAttacks"",
                    ""type"": ""Button"",
                    ""id"": ""f179e5f4-c2d0-493f-a452-3bb7b82324c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ddc8e389-2df3-4bee-a529-a3ae003c2057"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""896ad5cc-a660-4b92-929a-9536c836d65d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""38d0a903-7060-4c6c-b010-5ba9095caa8f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""febfd365-db8c-4980-bd2f-bc2aeb887ee6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b9de8438-6228-4098-b5b7-4b7d31a75845"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a3f39c59-b4e7-45ff-a62d-07f62c779c66"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33011200-01ed-49a7-925c-388468b363dc"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TeamPanel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12b86d08-11fb-47d1-849e-4a7cdf69cb66"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e90ee0f-c137-412a-9990-039c1ae4afd6"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1317bca3-697b-4aa0-88c6-f4d4a63c63d3"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchAttacks"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Actions
        m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
        m_Actions_Movement = m_Actions.FindAction("Movement", throwIfNotFound: true);
        m_Actions_Attack = m_Actions.FindAction("Attack", throwIfNotFound: true);
        m_Actions_TeamPanel = m_Actions.FindAction("TeamPanel", throwIfNotFound: true);
        m_Actions_TurnRight = m_Actions.FindAction("TurnRight", throwIfNotFound: true);
        m_Actions_TurnLeft = m_Actions.FindAction("TurnLeft", throwIfNotFound: true);
        m_Actions_SwitchAttacks = m_Actions.FindAction("SwitchAttacks", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Actions
    private readonly InputActionMap m_Actions;
    private IActionsActions m_ActionsActionsCallbackInterface;
    private readonly InputAction m_Actions_Movement;
    private readonly InputAction m_Actions_Attack;
    private readonly InputAction m_Actions_TeamPanel;
    private readonly InputAction m_Actions_TurnRight;
    private readonly InputAction m_Actions_TurnLeft;
    private readonly InputAction m_Actions_SwitchAttacks;
    public struct ActionsActions
    {
        private @BasicActions m_Wrapper;
        public ActionsActions(@BasicActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Actions_Movement;
        public InputAction @Attack => m_Wrapper.m_Actions_Attack;
        public InputAction @TeamPanel => m_Wrapper.m_Actions_TeamPanel;
        public InputAction @TurnRight => m_Wrapper.m_Actions_TurnRight;
        public InputAction @TurnLeft => m_Wrapper.m_Actions_TurnLeft;
        public InputAction @SwitchAttacks => m_Wrapper.m_Actions_SwitchAttacks;
        public InputActionMap Get() { return m_Wrapper.m_Actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
        public void SetCallbacks(IActionsActions instance)
        {
            if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMovement;
                @Attack.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnAttack;
                @TeamPanel.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTeamPanel;
                @TeamPanel.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTeamPanel;
                @TeamPanel.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTeamPanel;
                @TurnRight.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTurnRight;
                @TurnRight.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTurnRight;
                @TurnRight.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTurnRight;
                @TurnLeft.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTurnLeft;
                @SwitchAttacks.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchAttacks;
                @SwitchAttacks.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchAttacks;
                @SwitchAttacks.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchAttacks;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @TeamPanel.started += instance.OnTeamPanel;
                @TeamPanel.performed += instance.OnTeamPanel;
                @TeamPanel.canceled += instance.OnTeamPanel;
                @TurnRight.started += instance.OnTurnRight;
                @TurnRight.performed += instance.OnTurnRight;
                @TurnRight.canceled += instance.OnTurnRight;
                @TurnLeft.started += instance.OnTurnLeft;
                @TurnLeft.performed += instance.OnTurnLeft;
                @TurnLeft.canceled += instance.OnTurnLeft;
                @SwitchAttacks.started += instance.OnSwitchAttacks;
                @SwitchAttacks.performed += instance.OnSwitchAttacks;
                @SwitchAttacks.canceled += instance.OnSwitchAttacks;
            }
        }
    }
    public ActionsActions @Actions => new ActionsActions(this);
    public interface IActionsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnTeamPanel(InputAction.CallbackContext context);
        void OnTurnRight(InputAction.CallbackContext context);
        void OnTurnLeft(InputAction.CallbackContext context);
        void OnSwitchAttacks(InputAction.CallbackContext context);
    }
}
