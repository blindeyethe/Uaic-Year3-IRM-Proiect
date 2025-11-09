using UnityEngine;
using UnityEngine.UI;

namespace IRM.AlbumSystem.UI
{
    internal sealed class UIPageContent : MonoBehaviour
    {
        [SerializeField] private Image[] images;
        
        public void Initialize(AlbumPage page)
        {
            // _images[0].sprite = ;

            for (int i = 1; i < images.Length; i++)
            {
                var thumbnail = page.GetThumbnail(i - 1);
                images[i].sprite = thumbnail;
            }
        }
    }
}