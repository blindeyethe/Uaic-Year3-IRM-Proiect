using UnityEngine;
using IRM.AlbumSystem;
using IRM.PhotoSystem;
using IRM.Utility;

namespace IRM.CameraSystem
{
    internal sealed class PhotoValidator : MonoBehaviour
    {
        [SerializeField] private AlbumData albumData;
        [SerializeField] private RenderTexture cameraRenderTexture;

        public void Validate(PhotoObject capturedObjects)
        {
            foreach (var page in albumData.Pages)
            {
                if (!page.Contains(capturedObjects))
                    continue;

                foreach (var photo in page.Photos)
                {
                    if (!photo.Contains(capturedObjects))
                        continue;

                    SavePhoto(photo);
                    return;
                }
            }
        }

        private void SavePhoto(PhotoData photo)
        {
            var imageName = photo.GetImageName();
            var capturedImage = cameraRenderTexture.SaveImage(imageName);
            
            photo.MarkAsFound(capturedImage);
        }
    }
}