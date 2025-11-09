using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace TheBlindEye.Utility
{
    public static class UniTaskExtensions
    {
        public static async UniTask MovePositionAsync(this Transform target, 
            Vector3 endPosition, float duration)
        {
            var startPosition = target.position;
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                var position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                target.position = position;
                
                elapsedTime += Time.deltaTime;
                await UniTask.NextFrame();
            }

            target.position = endPosition;
        }
        
        public static async UniTask MoveRotationAsync(this Transform target, 
            Quaternion endRotation, float duration)
        {
            var startRotation = target.rotation;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                var rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration); 
                target.rotation = rotation;
                    
                elapsedTime += Time.deltaTime;
                await UniTask.NextFrame();
            }
            
            target.rotation = endRotation;
        }
        
        public static async UniTask MoveLocalRotationAsync(this Transform target, 
            Quaternion endRotation, float duration, CancellationToken cancellationToken = default)
        {
            var startRotation = target.localRotation;
            float elapsedTime = 0;

            while (elapsedTime < duration && !cancellationToken.IsCancellationRequested)
            {
                var rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration); 
                target.localRotation = rotation;
                    
                elapsedTime += Time.deltaTime;
                await UniTask.NextFrame();
            }
            
            if (!cancellationToken.IsCancellationRequested)
                target.localRotation = endRotation;
        }
        
        public static async UniTask MovePositionAndRotationAsync(this Transform target, 
            Vector3 endPosition, Quaternion endRotation, float duration)
        {
            var positionTask = target.MovePositionAsync(endPosition, duration);
            var rotationTask = target.MoveRotationAsync(endRotation, duration);

            await positionTask;
            await rotationTask;
        }
        
        public static async UniTask MovePositionAndRotationAsync(this Transform target, 
            Transform endTransform, float duration)
        {
            await target.MovePositionAndRotationAsync(endTransform.position, endTransform.rotation, duration);
        }
        
        public static async UniTask MovePositionAsync(this Rigidbody target, 
            Vector3 endPosition, float duration)
        {
            var startPosition = target.position;
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                var position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                target.MovePosition(position);
                
                elapsedTime += Time.fixedDeltaTime;
                await UniTask.WaitForFixedUpdate();
            }

            target.position = endPosition;
        }
        
        public static async UniTask MoveRotationAsync(this Rigidbody target, 
            Quaternion endRotation, float duration)
        {
            var startRotation = target.rotation;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                var rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration); 
                target.MoveRotation(rotation); 
                    
                elapsedTime += Time.fixedDeltaTime;
                await UniTask.WaitForFixedUpdate();
            }
            
            target.rotation = endRotation;
        }

        public static async UniTask MovePositionAndRotationAsync(this Rigidbody target, 
            Vector3 endPosition, Quaternion endRotation, float duration)
        {
            var positionTask = target.MovePositionAsync(endPosition, duration);
            var rotationTask = target.MoveRotationAsync(endRotation, duration);

            await positionTask;
            await rotationTask;
        }
        
        public static async UniTask MovePositionAndRotationAsync(this Rigidbody target, 
            Transform endTransform, float duration)
        {
            await target.MovePositionAndRotationAsync(endTransform.position, endTransform.rotation, duration);
        }

        public static void MoveLocalRotation(this Transform target,
            Quaternion endRotation, float duration, CancellationToken cancellationToken = default)
        {
            target.MoveLocalRotationAsync(endRotation, duration, cancellationToken)
                .Forget();
        }

        public static async UniTask LerpAsync(this CanvasGroup target, float endAlpha, float duration,
            CancellationToken cancellationToken = default)
        {
            float startAlpha = target.alpha;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;
                
                var alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                target.alpha = alpha;
                
                elapsedTime += Time.unscaledDeltaTime;
                await UniTask.NextFrame();
            }
    
            target.alpha = endAlpha;
        }
    }
}