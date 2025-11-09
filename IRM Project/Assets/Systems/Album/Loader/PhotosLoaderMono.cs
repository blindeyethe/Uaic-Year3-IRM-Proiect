using UnityEngine;
using LoaderObject;
using IRM.PhotoSystem;

namespace IRM.AlbumSystem.Loader
{
    internal sealed class PhotosLoaderMono : LoaderMono<PhotosLoaderObjectData, PhotoData>
    {
        [SerializeField] private PhotoData[] photos;
        
        protected override void Awake() =>
            PassData(photos);
    }
}