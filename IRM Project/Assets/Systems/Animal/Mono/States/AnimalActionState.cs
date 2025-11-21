using UnityEngine;

namespace Systems.AnimalSystem
{
    public class AnimalActionState : AnimalState
    {
        [SerializeField] private AnimationClip[] actionClips;
        private float _clipLength;
        private float _actionTimer;
        
        public override void OnStart(AnimalStateMachine stateMachine)
        {
            stateMachine.MeshRenderer.material = stateMachine.Materials[2]; //temp
            
            var randomIndex = Random.Range(0, actionClips.Length);
            
            _clipLength = actionClips[randomIndex].length;
            _actionTimer = 0f;
            
            stateMachine.Animator.Play(actionClips[randomIndex].name);
        }

        public override void OnUpdate(AnimalStateMachine stateMachine)
        {
            _actionTimer += Time.deltaTime;
            if (_actionTimer <= _clipLength)
                return;
            
            if(Random.value > 0.5f) stateMachine.ChangeState<AnimalRoamState>();
            else stateMachine.ChangeState<AnimalIdleState>();
        }
    }
}