using UnityEngine;
using LoaderObject;
using IRM.Utility;
using IRM.PhotoSystem;

namespace IRM.AlbumSystem.Loader
{
    [CreateAssetMenu(fileName = "Photo LoaderObjectData", menuName = "Album System/Photos LoaderObjectData")]
    internal sealed class PhotosLoaderObjectData : LoaderObjectData<PhotoData, bool>
    {
        protected override string FileName { get; } = "Photos Data";
        
        public override void SaveData(PhotoData data) =>
            SetValue(data.name, data.WasFound);

        public override void LoadData(PhotoData data)
        {
            var wasFound = GetValue(data.name);
            Debug.Log(data.name + " " + wasFound);
            if (!wasFound)
                return;
            
            var imageName = data.GetImageName();
            var image = UnityHelpers.LoadSprite(imageName);
                
            data.MarkAsFound(image);
        }
    }
}