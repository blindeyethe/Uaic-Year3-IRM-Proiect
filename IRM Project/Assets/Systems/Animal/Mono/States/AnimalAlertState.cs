using System;
using UnityEngine;
using UnityEngine.AI;

namespace Systems.AnimalSystem
{
    public class AnimalAlertState : AnimalState
    {
        private NavMeshAgent _navAgent;
        private float _alertTimer;

        private void Awake() => _navAgent = GetComponent<NavMeshAgent>();

        public override void OnStart(AnimalStateMachine stateMachine)
        {
            stateMachine.MeshRenderer.material = stateMachine.Materials[4]; //temp
            
            _navAgent.ResetPath();
            _alertTimer = 0;
        }

        public override void OnUpdate(AnimalStateMachine stateMachine)
        {
            _alertTimer += Time.deltaTime;
            if (_alertTimer <= stateMachine.AnimalData.AlertDuration)
                return;
            
            if (stateMachine.CanPerformAction) stateMachine.ChangeState<AnimalActionState>();
            else stateMachine.ChangeState<AnimalIdleState>();
        }
    }
}