using IRM.InteractionSystem;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace IRM.CameraSystem
{
    internal sealed class CameraInteractable : InteractableBase
    {
        [SerializeField] private GameObject cameraSettings;

        private PhotoDetection _photoDetection;
        private PhotoValidator _photoValidator;

        protected override void Awake()
        {
            base.Awake();
            _photoDetection = GetComponent<PhotoDetection>();
            _photoValidator = GetComponent<PhotoValidator>();
        }

        protected override void OnSelectEnter(SelectEnterEventArgs eventInfo) =>
           cameraSettings.SetActive(true);
        
        protected override void OnSelectExit(SelectExitEventArgs eventInfo) =>
            cameraSettings.SetActive(false);

        protected override void OnFocusEnter(FocusEnterEventArgs eventInfo)
        {
            var detectedObjects = _photoDetection.Detect();
            _photoValidator.Validate(detectedObjects);
        }
    }
}