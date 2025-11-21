using UnityEngine;

namespace Systems.AnimalSystem
{
    public class AnimalDetection : MonoBehaviour
    {
        private static LayerMask _playerLayerMask;
            
        [SerializeField] private AnimalData animalData;
        [SerializeField] private LayerMask _detectionMask;
        public Vector3 ClosestWaterSourcePosition { get; private set; }
        public Vector3 PreviousPlayerPosition { get; private set; }
        
        private float _closestWaterDistanceSqr = Mathf.Infinity;
        private bool _hasAlerted;
        
        private Transform _transform;
        private AnimalStateMachine _animalStateMachine;
        private readonly Collider[] _results = new Collider[5];

        private void Awake()
        {
            _animalStateMachine = GetComponent<AnimalStateMachine>();
            _transform = transform;
            
            _playerLayerMask = LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            var hits = Physics.OverlapSphereNonAlloc(_transform.position, animalData.TotalDetectionRange, _results, _detectionMask);
            if (hits == 0)
            {
                _hasAlerted = false;
                return;
            }

            var wasPlayerDetected = false;
            for (int i = 0; i < hits; i++)
            {
                var resultPosition = _results[i].transform.position;
                var distanceSqr = (_transform.position - resultPosition).sqrMagnitude;
                
                var layer = _results[i].gameObject.layer;
                if (layer == _playerLayerMask && distanceSqr < animalData.PlayerDetectionRangeSqr)
                {
                    wasPlayerDetected = true;
                    PlayerDetectionLogic(distanceSqr, resultPosition);
                    
                    continue;
                }
                
                if (distanceSqr >= _closestWaterDistanceSqr)
                    continue;

                ClosestWaterSourcePosition = resultPosition;
                _closestWaterDistanceSqr = distanceSqr;
            }

            if (!wasPlayerDetected)
                _hasAlerted = false;
        }

        private void PlayerDetectionLogic(float distanceSqr, Vector3 playerPosition)
        {
            var playerMovedDistanceSqr = (playerPosition - PreviousPlayerPosition).sqrMagnitude;
            PreviousPlayerPosition = playerPosition;
            
            if (distanceSqr < animalData.PlayerFleeRangeSqr || playerMovedDistanceSqr > animalData.PlayerUndetectedThreshold && _hasAlerted)
            {
                _animalStateMachine.ChangeState<AnimalFleeState>();
                return;
            }

            if (playerMovedDistanceSqr <= animalData.PlayerUndetectedThreshold && _hasAlerted)
                return;
                    
            _animalStateMachine.ChangeState<AnimalAlertState>();
            _hasAlerted = true;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, animalData.TotalDetectionRange);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, animalData.PlayerDetectionRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, animalData.PlayerFleeRange);
        }
    }
}