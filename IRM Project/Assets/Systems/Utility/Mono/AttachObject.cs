using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace IRM.Utility
{
    internal sealed class AttachObject : MonoBehaviour
    {
        [SerializeField] private XRBaseInteractable interactable;
        private XRSocketInteractor _socketInteractor;
        
        private void Awake() =>
            _socketInteractor = GetComponent<XRSocketInteractor>();

        private void Start()
        {
            IXRSelectInteractable selectInteractable = interactable;
            _socketInteractor.StartManualInteraction(selectInteractable);
        }
    }
}