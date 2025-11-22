using IRM.InteractionSystem;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace IRM.CameraSystem
{
    internal sealed class CameraInteractable : InteractableBase
    {
        [SerializeField] private GameObject cameraSettings;

        protected override void OnSelectEntered(SelectEnterEventArgs eventInfo) =>
           cameraSettings.SetActive(true);
        
        protected override void OnSelectExited(SelectExitEventArgs eventInfo) =>
            cameraSettings.SetActive(false);
    }
}