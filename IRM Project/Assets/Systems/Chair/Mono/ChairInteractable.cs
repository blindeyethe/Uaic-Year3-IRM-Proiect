using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.InputSystem;
using IRM.PlayerSystem;
using TheBlindEye.Utility;
using UnityEngine.XR.Interaction.Toolkit;

namespace IRM.ChairSystem
{
    internal sealed class ChairInteractable : XRBaseInteractable
    {
        [SerializeField] private Transform sitPivot;
        [SerializeField] private Transform standPivot;

        private bool _isSitting;

        private void Start()
        {
            var playerManager = PlayerManager.Instance;
            
            playerManager.SubscribeOnActivate(OnActivate);
            playerManager.SubscribeOnMove(OnMove);
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            var playerManager = PlayerManager.Instance;

            playerManager.UnsubscribeOnActivate(OnActivate);
            playerManager.UnsubscribeOnMove(OnMove);
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            if (!_isSitting)
                return;
            
            MovePlayer(standPivot)
                .Forget();
        }

        private void OnActivate(InputAction.CallbackContext _)
        {
            if (!isHovered || _isSitting)
                return;

            MovePlayer(sitPivot)
                .Forget();
        }

        private async UniTaskVoid MovePlayer(Transform pivot)
        {
            var manager = PlayerManager.Instance;
            _isSitting = !_isSitting;
            
            var moveAsync = manager.transform.MovePositionAndRotationAsync(pivot, 0.5f);

            if (!_isSitting)
                await moveAsync;
            
            manager.ToggleMovement();
            manager.ToggleGravity();

            if (_isSitting)
                await moveAsync;
        }
    }
}