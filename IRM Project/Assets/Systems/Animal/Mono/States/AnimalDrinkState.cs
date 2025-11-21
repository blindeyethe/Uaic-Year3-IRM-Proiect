using IRM.Utility;
using UnityEngine;
using UnityEngine.AI;

namespace Systems.AnimalSystem
{
    public class AnimalDrinkState : AnimalState
    {
        private NavMeshAgent _navAgent; 
        private float _thirstCooldown;
        private float _cooldownTimer;
        private float _drinkTimer;

        private void Awake() => _navAgent = GetComponent<NavMeshAgent>();

        public override void OnStart(AnimalStateMachine stateMachine)
        {
            stateMachine.MeshRenderer.material = stateMachine.Materials[3]; //temp
            
            _thirstCooldown = stateMachine.AnimalData.ThirstCooldown;
            var waterSourcePosition = stateMachine.AnimalDetection.ClosestWaterSourcePosition;

            _navAgent.speed = stateMachine.AnimalData.WalkingSpeed;
            _navAgent.SetDestination(waterSourcePosition);
        }

        public override void OnUpdate(AnimalStateMachine stateMachine)
        {
            if (!_navAgent.HasReachedTarget())
            {
                _drinkTimer = 0;
                return;
            }
            
            _drinkTimer += Time.deltaTime;
            if (_drinkTimer <= stateMachine.AnimalData.DrinkDuration)
                return;
            
            stateMachine.ChangeState<AnimalIdleState>();
        }

        public override void OnExit(AnimalStateMachine stateMachine)
        {
            CanChangeInThisState = false;
            _cooldownTimer = 0f;
        }

        private void Update()
        {
            if (CanChangeInThisState)
                return;
            
            _cooldownTimer += Time.deltaTime;
            if(_cooldownTimer > _thirstCooldown)
                CanChangeInThisState = true;
        }
    }
}