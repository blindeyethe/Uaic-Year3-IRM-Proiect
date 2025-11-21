using IRM.Utility;
using UnityEngine;
using UnityEngine.AI;

namespace Systems.AnimalSystem
{
    public class AnimalFleeState : AnimalState
    {
        [SerializeField] private AnimationClip walkingAnimation;
        private NavMeshAgent _navAgent;
        private AnimalBounds _animalBounds;
        private Transform _transform;
        
        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _animalBounds = GetComponentInParent<AnimalBounds>();
            _transform = transform;
        }
        public override void OnStart(AnimalStateMachine stateMachine)
        {
            stateMachine.MeshRenderer.material = stateMachine.Materials[5];
            
            var randomPosition = _animalBounds.GetFleePosition(_transform.position, 
                stateMachine.AnimalDetection.PreviousPlayerPosition, stateMachine.AnimalData.FleeDistance);
            
            _navAgent.speed = stateMachine.AnimalData.RunningSpeed;
            _navAgent.SetDestination(randomPosition);
        }

        public override void OnUpdate(AnimalStateMachine stateMachine)
        {
            if (!_navAgent.HasReachedTarget())
                return;
           
            stateMachine.ChangeState<AnimalAlertState>();
        }
    }
}