using System.IO;
using UnityEngine;

namespace IRM.Utility
{
    public static class UnityHelpers
    {
        public static Sprite LoadSprite(string imageName)
        {
            string imagePath = Path.Combine(Application.persistentDataPath, imageName);
            var imageBytes = File.ReadAllBytes(imagePath);

            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            return texture.LoadImage(imageBytes) ? texture.ToSprite() : null;
        }
    }
}