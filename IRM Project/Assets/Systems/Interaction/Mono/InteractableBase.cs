using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace IRM.InteractionSystem
{
    [RequireComponent(typeof(IXRSelectInteractable))]
    public abstract class InteractableBase : MonoBehaviour
    {
        protected XRBaseInteractable _interactable;
        private int _leftControllerLayer;

        protected virtual void Awake()
        {
            _interactable = GetComponentInParent<XRBaseInteractable>();
            _leftControllerLayer = LayerMask.NameToLayer("Left Controller");
        }

        protected virtual void Start()
        {
            _interactable.selectEntered.AddListener(OnSelectEnter);
            _interactable.selectExited.AddListener(OnSelectExit);
            
            _interactable.focusEntered.AddListener(OnFocusEnter);
            _interactable.focusExited.AddListener(OnFocusExit);

        }

        protected virtual void OnDisable()
        {
            _interactable.selectEntered.RemoveListener(OnSelectEnter);
            _interactable.selectExited.RemoveListener(OnSelectExit);
            
            _interactable.focusEntered.RemoveListener(OnFocusEnter);
            _interactable.focusExited.RemoveListener(OnFocusExit);
        }

        public bool IsLeftController(SelectExitEventArgs eventInfo)
        {
            var interactor = eventInfo.interactorObject.transform;
            return interactor.gameObject.layer == _leftControllerLayer;
        }
        
        protected virtual void OnSelectExit(SelectExitEventArgs eventInfo) {}
        protected virtual void OnSelectEnter(SelectEnterEventArgs eventInfo) {}
        
        protected virtual void OnFocusExit(FocusExitEventArgs eventInfo) {}
        protected virtual void OnFocusEnter(FocusEnterEventArgs eventInfo) {}
    }
}