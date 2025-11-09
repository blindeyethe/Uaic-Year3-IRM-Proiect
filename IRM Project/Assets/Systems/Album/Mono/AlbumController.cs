using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TheBlindEye.Utility;
using IRM.Utility;

namespace IRM.AlbumSystem
{
    internal sealed class AlbumController : MonoBehaviour
    {
        [SerializeField] private AlbumData albumData;
        
        [SerializeField] private Transform leftSide, rightSide;
        [SerializeField] private PageController[] pages;
        
        [SerializeField] private float pageTurnDuration = 0.5f;
        
        private readonly Quaternion[] _pageRotations = new Quaternion[4];
        private int _currentOpenIndex;

        private void Awake()
        {
            for (int i = 0; i < pages.Length; i++)
                _pageRotations[i] = pages[i].transform.localRotation;
        }
        
        public async UniTask OpenAsync(CancellationToken cancellationToken = default)
        {
            var leftCover = leftSide.MoveLocalRotationAsync(Quaternion.identity, pageTurnDuration, cancellationToken);
            var rightCover = rightSide.MoveLocalRotationAsync(Quaternion.Euler(-180, 0, 0), pageTurnDuration, cancellationToken);

            for (int i = 0; i < 4; i++)
                pages[i].transform.MoveLocalRotation(_pageRotations[i], pageTurnDuration, cancellationToken);

            ShowPageContents(0, pages[1], pages[2]);
            pages.SetActive(true);

            await rightCover;
            await leftCover;
        }

        public async UniTask CloseAsync(CancellationToken cancellationToken = default)
        {
            var rotation = Quaternion.Euler(-90, 0, 0);
            
            var leftCover  = leftSide.MoveLocalRotationAsync(rotation, pageTurnDuration, cancellationToken);
            var rightCover =  rightSide.MoveLocalRotationAsync(rotation, pageTurnDuration, cancellationToken);

            for (int i = 0; i < 4; i++)
                pages[i].transform.MoveLocalRotation(rotation, pageTurnDuration, cancellationToken);
            
            await rightCover;
            await leftCover;

            if (cancellationToken.IsCancellationRequested)
                return;
            
            pages.SetActive(false);
            
            pages[1].CleanUp();
            pages[2].CleanUp();
        }
        
        public async UniTask TurnPageAsync(bool swipeRight)
        {
            int openCount = albumData.OpenCount;
            _currentOpenIndex = (_currentOpenIndex + (swipeRight ? 1 : -1)) % openCount;

            var leftPage = pages[swipeRight ? 2 : 0];
            var rightPage = pages[swipeRight ? 3 : 1];
            
            ShowPageContents(_currentOpenIndex, leftPage, rightPage);
            
            var rotateAsync = swipeRight ?
                RotateRightPagesAsync(leftPage, rightPage) :
                RotateLeftPagesAsync(rightPage, leftPage);
            
            await rotateAsync;
        }

        public bool CanSwipeInDirection(bool swipeRight)
        {
            switch (swipeRight)
            {
                case false when _currentOpenIndex == 0:
                case true when _currentOpenIndex == albumData.OpenCount - 1:
                    return false;
            }

            return true;
        }

        private async UniTask RotateRightPagesAsync(PageController pageToRotate, PageController adjacentPage)
        {
            var a = pageToRotate.transform.MoveLocalRotationAsync(_pageRotations[1], pageTurnDuration);
            var b = adjacentPage.transform.MoveLocalRotationAsync(_pageRotations[2], pageTurnDuration);
            var c = pages[1].transform.MoveLocalRotationAsync(_pageRotations[0], pageTurnDuration);

            var swapPage = pages[0];
            swapPage.transform.localRotation = _pageRotations[3];
            
            await a; 
            await b; 
            await c;
            
            swapPage.CleanUp();
            SwapPagesAfterRight();
        }

        private async UniTask RotateLeftPagesAsync(PageController rotatePage, PageController otherPage)
        {
            var a = rotatePage.transform.MoveLocalRotationAsync(_pageRotations[2], pageTurnDuration);
            var b = otherPage.transform.MoveLocalRotationAsync(_pageRotations[1], pageTurnDuration);
            var c = pages[2].transform.MoveLocalRotationAsync(_pageRotations[3], pageTurnDuration);

            var swapPage = pages[3];
            swapPage.transform.localRotation = _pageRotations[0];
            
            await a; 
            await b; 
            await c;

            swapPage.CleanUp();
            SwapPagesAfterLeft();
        }
        
        private void ShowPageContents(int openIndex, PageController leftPage, PageController rightPage)
        {
            var (leftAlbumPage, rightAlbumPage) = albumData.GetPages(openIndex);
            
            leftPage.ShowContent(leftAlbumPage, true);
            rightPage.ShowContent(rightAlbumPage, false);
        }

        private void SwapPagesAfterRight()
        {
            var page0 = pages[0];
            
            pages[0] = pages[1];
            pages[1] = pages[2];
            pages[2] = pages[3];
            pages[3] = page0;
        }

        private void SwapPagesAfterLeft()
        {
            var page2 = pages[2];
            
            pages[2] = pages[1];
            pages[1] = pages[0];
            pages[0] = pages[3];
            pages[3] = page2;
        }
    }
}