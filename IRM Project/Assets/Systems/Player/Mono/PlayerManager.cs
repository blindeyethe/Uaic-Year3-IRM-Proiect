using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace IRM.PlayerSystem
{
    internal sealed class PlayerManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference leftActivateInput;
        [SerializeField] private InputActionReference rightActivateInput;
        
        [SerializeField] private InputActionReference leftMoveInput;

        [SerializeField] private InputActionReference xInput, yInput;
        [SerializeField] private InputActionReference aInput, bInput;

        public static PlayerManager Instance { get; private set; }
        
        private Transform _transform;
        
        private DynamicMoveProvider _moveProvider;
        private GravityProvider _gravityProvider;

        private void Awake()
        {
            Instance = this;
            
            _transform = transform;
            _moveProvider = GetComponentInChildren<DynamicMoveProvider>();
            _gravityProvider = GetComponentInChildren<GravityProvider>();
        }
        
        public void SubscribeOnActivate(Action<InputAction.CallbackContext> onPerformed)
        {
            leftActivateInput.action.performed += onPerformed;
            rightActivateInput.action.performed += onPerformed;
        }
        
        public void UnsubscribeOnActivate(Action<InputAction.CallbackContext> onPerformed)
        {
            leftActivateInput.action.performed -= onPerformed;
            rightActivateInput.action.performed -= onPerformed;
        }
        
        public void SubscribeOnMove(Action<InputAction.CallbackContext> onPerformed) =>
            leftMoveInput.action.performed += onPerformed;
        
        public void UnsubscribeOnMove(Action<InputAction.CallbackContext> onPerformed) =>
            leftMoveInput.action.performed -= onPerformed;

        public void SubscribeOnDownButton(Action<InputAction.CallbackContext> onPerformed)
        {
            xInput.action.performed += onPerformed;
            aInput.action.performed += onPerformed;
        }
        
        public void UnsubscribeOnDownButton(Action<InputAction.CallbackContext> onPerformed)
        {
            xInput.action.performed -= onPerformed;
            aInput.action.performed -= onPerformed;
        }
        
        public void SubscribeOnUpButton(Action<InputAction.CallbackContext> onPerformed)
        {
            yInput.action.performed += onPerformed;
            bInput.action.performed += onPerformed;
        }
        
        public void UnsubscribeOnUpButton(Action<InputAction.CallbackContext> onPerformed)
        {
            yInput.action.performed -= onPerformed;
            bInput.action.performed -= onPerformed;
        }
        
        public void Teleport(Vector3 position, Quaternion rotation)
        {
            _transform.position = position;
            _transform.rotation = rotation;
        }

        public void ToggleMovement() =>
            _moveProvider.enabled = !_moveProvider.enabled;
        
        public void ToggleGravity() =>
            _gravityProvider.enabled = !_gravityProvider.enabled;
    }
}