using UnityEngine;
using IRM.Systems.Utility;

namespace IRM.CameraSystem.UI
{
    internal sealed class UICameraFocusBoxPanel : MonoBehaviour
    {
        [SerializeField] private Camera cameraView;
        [SerializeField] private RectTransform focusBoxPrefab;
        
        private RectTransform _rect;

        private void Awake() =>
            _rect = GetComponent<RectTransform>();

        private void OnEnable()
        {
            PhotoDetection.OnStartObjectDetection += DestroyChildren;
            PhotoDetection.OnObjectDetected += DrawFocusBox;
        }

        private void OnDisable()
        {
            PhotoDetection.OnStartObjectDetection -= DestroyChildren;
            PhotoDetection.OnObjectDetected -= DrawFocusBox;
        }
        
        private void DrawFocusBox(Bounds bounds)
        {
            var corners = new BoundsCorners(bounds);

            var min = new Vector2(float.MaxValue, float.MaxValue);
            var max = new Vector2(float.MinValue, float.MinValue);

            for (int i = 0; i < BoundsCorners.CORNERS_COUNT; i++)
            {
                var viewportPosition = cameraView.WorldToViewportPoint(corners[i]);
                
                min = Vector2.Min(min, viewportPosition);
                max = Vector2.Max(max, viewportPosition);
            }
            
            var canvasSize = _rect.rect.size;
            
            var boxMin = new Vector2
            (
                (min.x - 0.5f) * canvasSize.x,
                (min.y - 0.5f) * canvasSize.y
            );

            var boxMax = new Vector2
            (
                (max.x - 0.5f) * canvasSize.x,
                (max.y - 0.5f) * canvasSize.y
            );

            var boxInstance = Instantiate(focusBoxPrefab, _rect);

            boxInstance.anchoredPosition = (boxMin + boxMax) * 0.5f;
            boxInstance.sizeDelta = boxMax - boxMin;
            boxInstance.localScale = Vector3.one;
        }
        
        private void DestroyChildren()
        {
            for (int i = 0; i < _rect.childCount; i++)
                Destroy(_rect.GetChild(i).gameObject);
        }
    }
}