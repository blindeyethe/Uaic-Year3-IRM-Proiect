using UnityEngine;

namespace Systems.AnimalSystem
{
    public class AnimalIdleState : AnimalState
    {
        [SerializeField] private AnimationClip idleAnimation;
        private float _idleTimer;
        
        public override void OnStart(AnimalStateMachine stateMachine)
        {
            stateMachine.MeshRenderer.material = stateMachine.Materials[0]; //temp
            
            _idleTimer = 0;
            //stateMachine.Animator.Play(idleAnimation.name);
        }

        public override void OnUpdate(AnimalStateMachine stateMachine)
        {
            _idleTimer += Time.deltaTime;
            if (_idleTimer <= stateMachine.AnimalData.IdleDuration)
                return;

            if (stateMachine.ChangeState<AnimalDrinkState>())
                return;
            
            if (stateMachine.CanPerformAction) stateMachine.ChangeState<AnimalActionState>();
            stateMachine.ChangeState<AnimalRoamState>();
        }
    }
}