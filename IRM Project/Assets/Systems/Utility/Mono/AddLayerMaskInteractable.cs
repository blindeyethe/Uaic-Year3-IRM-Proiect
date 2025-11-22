using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using IRM.InteractionSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace IRM.Utility
{
    internal sealed class AddLayerMaskInteractable : InteractableBase
    {
        [SerializeField] private XRBaseInteractor triggerInteractor;
        [SerializeField] private XRBaseInteractor modifyInteractor;
        
        [SerializeField] private InteractionLayerMask toAdd;
        
        protected override void OnSelectEnter(SelectEnterEventArgs eventInfo)
        {
            if ((XRBaseInteractor)eventInfo.interactorObject == triggerInteractor)
                modifyInteractor.interactionLayers |= toAdd;
        }

        protected override void OnSelectExit(SelectExitEventArgs eventInfo)
        {
            if ((XRBaseInteractor)eventInfo.interactorObject == triggerInteractor)
                modifyInteractor.interactionLayers &= ~toAdd;
        }
    }
}