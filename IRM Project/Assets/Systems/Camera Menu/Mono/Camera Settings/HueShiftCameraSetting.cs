using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace IRM.CameraMenuSystems.UI
{
    public class HueShiftCameraSetting : CameraSetting
    {
        private ColorAdjustments _hueShift;
        private UIHueShiftPanel _hueShiftPanel;

        private void Awake() =>
            _hueShiftPanel = worldCanvas.GetComponentInChildren<UIHueShiftPanel>();

        public override void Initialize(Volume volume)
        {
            volume.profile.TryGet(out _hueShift);
            _hueShiftPanel.Initialize(_hueShift);
        }

        public override void OnSelected() =>
            _hueShiftPanel.OnSelect();

        public override void OnExited() =>
            _hueShiftPanel.OnDeselect();
    }
}