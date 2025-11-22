using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace IRM.CameraMenuSystems.UI
{
    public class SaturationCameraSetting : CameraSetting
    {
        private ColorAdjustments _saturation;
        private UISaturationPanel _saturationPanel;

        private void Awake() =>
            _saturationPanel = worldCanvas.GetComponentInChildren<UISaturationPanel>();

        public override void Initialize(Volume volume)
        {
            volume.profile.TryGet(out _saturation);
            _saturationPanel.Initialize(_saturation);
        }

        public override void OnSelected() =>
            _saturationPanel.OnSelect();

        public override void OnExited() =>
            _saturationPanel.OnDeselect();
    }
}