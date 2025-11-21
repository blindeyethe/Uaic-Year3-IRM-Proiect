using UnityEngine;
using LoaderObject;
using IRM.PhotoSystem;

#if UNITY_EDITOR
#endif

namespace IRM.AlbumSystem.Loader
{
    internal sealed class PhotosLoaderMono : LoaderMono<PhotosLoaderObjectData, PhotoData>
    {
        [SerializeField] private PhotoData[] photos;
        
        protected override void Awake() =>
            PassData(photos);

        private void GetAllPhotos()
        {
        }
    }
}