//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Data/Input/InputUnit.inputactions
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

namespace Input
{
    public partial class @InputUnit : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputUnit()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputUnit"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""fc11a743-adb0-4d61-8030-c10f44ce0985"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9c1765eb-f51e-401e-a8ea-71f75a3d90b2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ViewDelta"",
                    ""type"": ""Value"",
                    ""id"": ""0135f5de-8806-451a-8417-c5d4124d1f5a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""ef2a96a7-5488-44ef-bdc6-3b53ac7e6efc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""54a69d23-8ae3-4d37-aee3-bd0165e33228"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""6764d691-4d10-404d-b9b7-13faf7fbb91c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3c15867d-9cbe-44b8-bd3e-27a9ffe516c0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""183d5d48-a454-4aa2-aa57-3f30a401e008"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bd782c81-f91f-40bd-a97a-12b9e5cb5f59"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""caba577c-c361-4c22-a433-ecc468d394fb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e1caeb75-2742-4b34-aa6c-b3b95c06bb9e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6eb558af-4fcc-4011-892d-6f84701aa210"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2602169-2136-4781-9239-bf6e472198aa"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ViewDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0733154d-6c5a-473c-939d-79e7b7d9e834"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=2)"",
                    ""groups"": """",
                    ""action"": ""ViewDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""790bada6-615c-4357-aa6d-e845249fbd73"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77c6eabb-aa79-460d-b782-92b8a2801390"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d56747b8-b4f6-4dc6-8f77-e44631734dfb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f7b382b-8bdc-4a81-bae6-a1e31325e978"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88d99349-0eea-45bf-abba-1731f3b43e3d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""930cf9f3-4e2a-4ecb-bd8b-9fd8713a5bc0"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
            m_Player_ViewDelta = m_Player.FindAction("ViewDelta", throwIfNotFound: true);
            m_Player_Action = m_Player.FindAction("Action", throwIfNotFound: true);
            m_Player_Quit = m_Player.FindAction("Quit", throwIfNotFound: true);
            m_Player_Restart = m_Player.FindAction("Restart", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Move;
        private readonly InputAction m_Player_ViewDelta;
        private readonly InputAction m_Player_Action;
        private readonly InputAction m_Player_Quit;
        private readonly InputAction m_Player_Restart;
        public struct PlayerActions
        {
            private @InputUnit m_Wrapper;
            public PlayerActions(@InputUnit wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @ViewDelta => m_Wrapper.m_Player_ViewDelta;
            public InputAction @Action => m_Wrapper.m_Player_Action;
            public InputAction @Quit => m_Wrapper.m_Player_Quit;
            public InputAction @Restart => m_Wrapper.m_Player_Restart;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @ViewDelta.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnViewDelta;
                    @ViewDelta.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnViewDelta;
                    @ViewDelta.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnViewDelta;
                    @Action.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                    @Action.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                    @Action.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                    @Quit.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuit;
                    @Quit.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuit;
                    @Quit.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuit;
                    @Restart.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestart;
                    @Restart.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestart;
                    @Restart.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestart;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @ViewDelta.started += instance.OnViewDelta;
                    @ViewDelta.performed += instance.OnViewDelta;
                    @ViewDelta.canceled += instance.OnViewDelta;
                    @Action.started += instance.OnAction;
                    @Action.performed += instance.OnAction;
                    @Action.canceled += instance.OnAction;
                    @Quit.started += instance.OnQuit;
                    @Quit.performed += instance.OnQuit;
                    @Quit.canceled += instance.OnQuit;
                    @Restart.started += instance.OnRestart;
                    @Restart.performed += instance.OnRestart;
                    @Restart.canceled += instance.OnRestart;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        public interface IPlayerActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnViewDelta(InputAction.CallbackContext context);
            void OnAction(InputAction.CallbackContext context);
            void OnQuit(InputAction.CallbackContext context);
            void OnRestart(InputAction.CallbackContext context);
        }
    }
}
