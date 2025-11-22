using UnityEngine;
using UnityEngine.Rendering;

namespace IRM.CameraMenuSystems.UI
{
    public abstract class CameraSetting : MonoBehaviour
    {
        [SerializeField] protected GameObject worldCanvas;
        public abstract void Initialize(Volume volume);

        public abstract void OnSelected();
        public abstract void OnExited();
    }
}