using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using IRM.InteractionSystem;

namespace IRM.Utility
{
    internal sealed class SocketInteractable : InteractableBase
    {
        [SerializeField] private XRSocketInteractor socketInteractor;

        private void Start() =>
            AttachToSocket();

        protected override void OnSelectExited(SelectExitEventArgs eventInfo)
        {
            var interactor = eventInfo.interactorObject as XRBaseInteractor;
            if (interactor != socketInteractor)
                AttachToSocket();
        }
        
        private void AttachToSocket()
        {
            if (!socketInteractor.hasSelection)
                socketInteractor.StartManualInteraction(_interactable);
        }
    }
}