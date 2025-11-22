using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace IRM.CameraMenuSystems.UI
{
    public class ContrastCameraSetting : CameraSetting
    {
        private ColorAdjustments _contrast;
        private UIContrastPanel _contrastPanel;

        private void Awake() =>
            _contrastPanel = worldCanvas.GetComponentInChildren<UIContrastPanel>();

        public override void Initialize(Volume volume)
        {
            volume.profile.TryGet(out _contrast);
            _contrastPanel.Initialize(_contrast);
        }

        public override void OnSelected() =>
            _contrastPanel.OnSelect();

        public override void OnExited() =>
            _contrastPanel.OnDeselect();
    }
}