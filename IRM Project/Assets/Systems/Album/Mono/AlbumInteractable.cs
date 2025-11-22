using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using IRM.InteractionSystem;
using IRM.PlayerSystem;

namespace IRM.AlbumSystem
{
    internal sealed class AlbumInteractable : InteractableBase
    {
        [SerializeField] private XRSocketInteractor socketInteractor;
        
        private AlbumController _controller;
        private CancellationTokenSource _cancellationToken;

        private bool _isOpen, _canTurnPage = true;
        
        protected override void Awake()
        {
            base.Awake();
            _controller = GetComponent<AlbumController>();
        }

        protected override void Start()
        {
            base.Start();
            PlayerManager.Instance.SubscribeOnUpButton(OnUpPerformed);
            PlayerManager.Instance.SubscribeOnDownButton(OnDownPerformed);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            PlayerManager.Instance.UnsubscribeOnUpButton(OnUpPerformed);
            PlayerManager.Instance.UnsubscribeOnDownButton(OnDownPerformed);
            CancelAlbumAction();
        }

        private void TurnPage(bool swipeRight)
        {
            if (!_canTurnPage || !_interactable.isSelected)
                return;
            
            if (!_controller.CanSwipeInDirection(swipeRight))
            {
                OpenAlbumAsync(false).Forget();
                return;
            }

            if (!_isOpen)
            {
                OpenAlbumAsync(true).Forget();
                return;
            }
            
            TurnPageAsync(swipeRight)
                .Forget();
        }

        private async UniTaskVoid TurnPageAsync(bool swipeRight)
        {
            _canTurnPage = false;

            await _controller.TurnPageAsync(swipeRight);
            _canTurnPage = true;
        }

        protected override void OnSelectEnter(SelectEnterEventArgs eventInfo)
        {
            var interactor = eventInfo.interactorObject as XRBaseInteractor;
            bool openAlbum = interactor != socketInteractor;

            OpenAlbumAsync(openAlbum)
                .Forget();
        }

        private async UniTaskVoid OpenAlbumAsync(bool openAlbum)
        {
            CancelAlbumAction();
            _cancellationToken = new CancellationTokenSource();
            
            var albumAction = openAlbum ? 
                _controller.OpenAsync(_cancellationToken.Token) : 
                _controller.CloseAsync(_cancellationToken.Token);

            _canTurnPage = false;
            await albumAction;
            
            _canTurnPage = true;
            _isOpen = openAlbum;
        }

        private void OnUpPerformed(InputAction.CallbackContext _) =>
            TurnPage(false);

        private void OnDownPerformed(InputAction.CallbackContext _) =>
            TurnPage(true);
        
        private void CancelAlbumAction()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
        }
    }
}