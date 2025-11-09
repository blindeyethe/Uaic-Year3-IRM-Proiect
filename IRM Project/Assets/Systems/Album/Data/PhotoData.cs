using UnityEngine;

namespace IRM.PhotoSystem
{
    [CreateAssetMenu(fileName = "Photo Data", menuName = "Album System/Photo Data")]
    public sealed class PhotoData : ScriptableObject
    {
        [SerializeField] private Sprite previewImage;
        [SerializeField] private PhotoObject photoObjects;
        
        [SerializeField] private bool wasFound;

        private Sprite _capturedImage;
        
        public Sprite DisplayImage => wasFound ? _capturedImage : previewImage;
        public PhotoObject PhotoObjects => photoObjects;
        public bool WasFound => wasFound;
        
        public void MarkAsFound(Sprite capturedImage)
        {
            wasFound = true;
            _capturedImage = capturedImage;
        }
        
        public bool Contains(PhotoObject objects) =>
            photoObjects == objects;
    }
}