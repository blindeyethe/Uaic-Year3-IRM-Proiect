using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace IRM.InteractionSystem
{
    public abstract class InteractorBase : MonoBehaviour
    {
        [SerializeField] protected XRBaseInteractor interactor;
        private int _leftControllerLayer;

        public bool IsLeftController => gameObject.layer == _leftControllerLayer;
        
        protected virtual void Awake() =>
            _leftControllerLayer = LayerMask.NameToLayer("Left Controller");

        private void Start()
        {
            interactor.selectEntered.AddListener(OnSelectEnter);
            interactor.selectExited.AddListener(OnSelectExit);
        }

        private void OnDisable()
        {
            interactor.selectEntered.RemoveListener(OnSelectEnter);
            interactor.selectExited.RemoveListener(OnSelectExit);
        }

        protected virtual void OnSelectEnter(SelectEnterEventArgs eventInfo) {}
        protected virtual void OnSelectExit(SelectExitEventArgs eventInfo) {}
    }
}