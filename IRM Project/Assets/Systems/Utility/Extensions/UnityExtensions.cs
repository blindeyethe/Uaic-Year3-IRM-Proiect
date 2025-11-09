using System.IO;
using UnityEngine;

namespace IRM.Utility
{
    public static class UnityExtensions
    {
        public static void SetActive<T>(this T[] gameObjects, bool isActive)
            where T : MonoBehaviour
        {
            foreach (var gameObject in gameObjects)
                gameObject.gameObject.SetActive(isActive);
        }
        
        public static void CalculateFrustumBounds(this Camera camera, out Vector3 boundCenter, out Vector3 halfExtents)
        {
            float near = camera.nearClipPlane;
            float far = camera.farClipPlane;

            float heightFar = 2f * Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * far;
            float widthFar = heightFar * camera.aspect;

            var cameraTransform = camera.transform;

            boundCenter = cameraTransform.position + cameraTransform.forward * ((near + far) / 2);
            halfExtents = new Vector3(widthFar / 2, heightFar / 2, (far - near) / 2);
        }

        public static Sprite SaveImage(this RenderTexture renderTexture, string fileName)
        {
            var currentTexture = RenderTexture.active;
            RenderTexture.active = renderTexture;

            int width = renderTexture.width;
            int height = renderTexture.height;
            
            var texture = new Texture2D(width, height, TextureFormat.RGB24, false);

            texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            texture.Apply();

            RenderTexture.active = currentTexture;
            var imageBytes = texture.EncodeToPNG();
            
            string imagePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllBytes(imagePath, imageBytes);
            
            return texture.ToSprite();
        }

        public static Sprite ToSprite(this Texture2D texture)
        {
            return Sprite.Create
            (
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
        }
    }
}