using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace IRM.InteractionSystem
{
    public abstract class InteractableBase : MonoBehaviour
    {
        protected IXRSelectInteractable _interactable;

        protected virtual void Awake() =>
            _interactable = GetComponent<IXRSelectInteractable>();

        private void OnEnable()
        {
            _interactable.selectEntered.AddListener(OnSelectEntered);
            _interactable.selectExited.AddListener(OnSelectExited);
        }

        protected virtual void OnDisable()
        {
            _interactable.selectEntered.RemoveListener(OnSelectEntered);
            _interactable.selectExited.RemoveListener(OnSelectExited);
        }

        protected virtual void OnSelectExited(SelectExitEventArgs eventInfo) {}
        protected virtual void OnSelectEntered(SelectEnterEventArgs eventInfo) {}
    }
}