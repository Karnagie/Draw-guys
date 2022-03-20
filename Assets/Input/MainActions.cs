// GENERATED AUTOMATICALLY FROM 'Assets/Input/MainActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Input
{
    public class @MainActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MainActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""5a034068-9c00-476d-82f6-dd0b79fc94b4"",
            ""actions"": [
                {
                    ""name"": ""MousePressing"",
                    ""type"": ""Button"",
                    ""id"": ""617db39c-b877-47e2-a0e8-8d32a0b42db5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e21d17e6-f493-43a6-aa0d-f2cd5bf75e5a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePressing"",
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
            m_Player_MousePressing = m_Player.FindAction("MousePressing", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_MousePressing;
        public struct PlayerActions
        {
            private @MainActions m_Wrapper;
            public PlayerActions(@MainActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @MousePressing => m_Wrapper.m_Player_MousePressing;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @MousePressing.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePressing;
                    @MousePressing.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePressing;
                    @MousePressing.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePressing;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MousePressing.started += instance.OnMousePressing;
                    @MousePressing.performed += instance.OnMousePressing;
                    @MousePressing.canceled += instance.OnMousePressing;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        public interface IPlayerActions
        {
            void OnMousePressing(InputAction.CallbackContext context);
        }
    }
}
