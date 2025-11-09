using IRM.PhotoSystem;
using UnityEngine;

namespace IRM.CameraSystem
{
    internal sealed class DetectionObject : MonoBehaviour
    {
        [SerializeField] private PhotoObject photoObject;
        
        public PhotoObject PhotoObject => photoObject;
    }
}