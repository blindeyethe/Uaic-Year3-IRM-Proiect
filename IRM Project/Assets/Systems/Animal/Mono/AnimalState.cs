using UnityEngine;

namespace Systems.AnimalSystem
{
    public abstract class AnimalState : MonoBehaviour
    {
        public bool CanChangeInThisState { get; protected set; } = true;
        
        public abstract void OnStart(AnimalStateMachine stateMachine);
        public abstract void OnUpdate(AnimalStateMachine stateMachine);

        public virtual void OnExit(AnimalStateMachine stateMachine) {}
    }
}