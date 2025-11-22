using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using IRM.InteractionSystem;
using IRM.Utility;
using TheBlindEye.Utility;

namespace IRM
{
    public class HandController : InteractorBase
    {
        private static readonly int IsGrabbing = Animator.StringToHash("IsGrabbing");

        private Transform _transform;
        private Animator _animator;
        
        private Quaternion _initialRotation;

        protected override void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
        }

        protected override void OnSelectEnter(SelectEnterEventArgs eventInfo)
        {
            var interactableTransform = eventInfo.interactableObject.transform;
            if (interactableTransform.TryGetComponent(out RotateHandInteractable rotateInteractable))
            {
                _initialRotation = _transform.localRotation;
                var rotation = rotateInteractable.Rotation * (IsLeftController ? -1 : 1);
                
                _transform.MoveLocalRotation(Quaternion.Euler(rotation), 0.5f);
            }
                
            _animator.SetBool(IsGrabbing, true);
        }

        protected override void OnSelectExit(SelectExitEventArgs eventInfo)
        {
            var interactableTransform = eventInfo.interactableObject.transform;
            if (interactableTransform.TryGetComponent(out RotateHandInteractable _))
                _transform.MoveLocalRotation(_initialRotation, 0.5f);
            
            _animator.SetBool(IsGrabbing, false);
        }
    }
}