using IRM.Utility;
using UnityEngine;
using UnityEngine.AI;

namespace Systems.AnimalSystem
{
    public class AnimalRoamState : AnimalState
    {
        [SerializeField] private AnimationClip walkingAnimation;
        private NavMeshAgent _navAgent;
        private AnimalBounds _animalBounds;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _animalBounds = GetComponentInParent<AnimalBounds>();
        }

        public override void OnStart(AnimalStateMachine stateMachine)
        {
            stateMachine.MeshRenderer.material = stateMachine.Materials[1]; //temp
            
            //stateMachine.Animator.Play(walkingAnimation.name);
            
            var randomPosition = _animalBounds.GetRandomPosition();
            
            _navAgent.speed = stateMachine.AnimalData.WalkingSpeed;
            _navAgent.SetDestination(randomPosition);
        }

        public override void OnUpdate(AnimalStateMachine stateMachine)
        {
            if (!_navAgent.HasReachedTarget())
                return;
            
            /*
            if (stateMachine.ChangeState<AnimalDrinkState>())
                return;
            
            if (stateMachine.CanPerformAction) stateMachine.ChangeState<AnimalActionState>();
            */
            stateMachine.ChangeState<AnimalIdleState>();
        }
    }
}