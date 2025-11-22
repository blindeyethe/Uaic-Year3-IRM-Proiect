using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace IRM.CameraMenuSystems.UI
{
    public class WhiteBalanceCameraSetting : CameraSetting
    {
        private WhiteBalance _whiteBalance;
        private UIWhiteBalancePanel _whiteBalancePanel;

        private void Awake() =>
            _whiteBalancePanel = worldCanvas.GetComponentInChildren<UIWhiteBalancePanel>();
        
        public override void Initialize(Volume volume)
        {
            volume.profile.TryGet(out _whiteBalance);
            _whiteBalancePanel.Initialize(_whiteBalance);
        }

        public override void OnSelected() =>
            _whiteBalancePanel.OnSelect();

        public override void OnExited() =>
            _whiteBalancePanel.OnDeselect();
    }
}