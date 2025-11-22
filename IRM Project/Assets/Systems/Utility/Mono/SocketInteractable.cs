using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using IRM.InteractionSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace IRM.Utility
{
    internal sealed class SocketInteractable : InteractableBase
    {
        [SerializeField] private XRSocketInteractor socketInteractor;

        protected override void Start()
        {
            base.Start();
            AttachToSocket();
        }

        protected override void OnSelectExit(SelectExitEventArgs eventInfo)
        {
            var interactor = eventInfo.interactorObject as XRBaseInteractor;
            if (interactor != socketInteractor)
                AttachToSocket();
        }
        
        private void AttachToSocket()
        {
            if (!socketInteractor.hasSelection)
                socketInteractor.StartManualInteraction((IXRSelectInteractable)_interactable);
        }
    }
}