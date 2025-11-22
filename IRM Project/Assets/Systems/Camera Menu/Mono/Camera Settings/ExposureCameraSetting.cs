
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace IRM.CameraMenuSystems.UI
{
    public class ExposureCameraSetting : CameraSetting
    {
        private ColorAdjustments _exposure;
        private UIExposurePanel _exposurePanel;

        private void Awake() =>
            _exposurePanel = worldCanvas.GetComponentInChildren<UIExposurePanel>();

        public override void Initialize(Volume volume)
        {
            volume.profile.TryGet(out _exposure);
            _exposurePanel.Initialize(_exposure);
        }

        public override void OnSelected() =>
            _exposurePanel.OnSelect();

        public override void OnExited() =>
            _exposurePanel.OnDeselect();
    }
}