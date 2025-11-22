using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace IRM.CameraMenuSystems.UI
{
    public class DepthOfFieldCameraSetting : CameraSetting
    {
        private DepthOfField _depthOfField;
        private UIDepthOfFieldPanel _depthOfFieldPanel;
        
        private void Awake() =>
            _depthOfFieldPanel = worldCanvas.GetComponentInChildren<UIDepthOfFieldPanel>();

        public override void Initialize(Volume volume)
        {
            volume.profile.TryGet(out _depthOfField);
            _depthOfFieldPanel.Initialize(_depthOfField);
        }
        
        public override void OnSelected() =>
            _depthOfFieldPanel.OnSelect();

        public override void OnExited() =>
            _depthOfFieldPanel.OnDeselect();
    }
}