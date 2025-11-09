using UnityEngine;
using TheBlindEye.Utility;
using IRM.PhotoSystem;
using IRM.AlbumSystem.UI;

namespace IRM.AlbumSystem
{
    [System.Serializable]
    internal class AlbumPage
    {
        [SerializeField] private PhotoData[] photos;
        [SerializeField] private UIPageContent pageContentPrefab;
        
        /// <summary>
        /// What PhotoObjects are contained in this page for quick lookup.
        /// </summary>
        [SerializeField, ReadOnly] private PhotoObject pagePhotoObjects;
        
        public PhotoData[] Photos => photos;
        public UIPageContent PageContentPrefab => pageContentPrefab;
        
        public bool Contains(PhotoObject photoObjects) =>
            pagePhotoObjects == photoObjects;
        
        public Sprite GetThumbnail(int photoIndex) =>
            photos[photoIndex].DisplayImage;

        #if UNITY_EDITOR
            internal void Editor_CalculatePagePhotoObjects()
            {
                pagePhotoObjects = PhotoObject.None;
                
                foreach (var photoData in photos)
                    pagePhotoObjects |= photoData.PhotoObjects;
            }
        #endif
    }
}