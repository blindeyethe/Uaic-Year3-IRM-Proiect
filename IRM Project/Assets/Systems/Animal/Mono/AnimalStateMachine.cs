using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.AnimalSystem
{
    public class AnimalStateMachine : MonoBehaviour
    {
        public MeshRenderer MeshRenderer { get; private set; } //temp
        public Material[] Materials; // temp
        public bool CanPerformAction => Random.value < AnimalData.PerformActionChance;
        public Animator Animator { get; private set; }
        public AnimalDetection AnimalDetection { get; private set; }
        [field:SerializeField] public AnimalData AnimalData { get; private set; }
        
        private AnimalState[] _states;
        private int _currentIndex;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            AnimalDetection = GetComponent<AnimalDetection>();
            MeshRenderer = GetComponent<MeshRenderer>();
        }
        
        private void Start()
        {
            _states = GetComponents<AnimalState>();
            ChangeState<AnimalRoamState>();
        }

        public bool ChangeState<T>()
            where T : AnimalState
        {
            var currentState = _states[_currentIndex];
            if (currentState is T)
                return false;

            int stateIndex = -1;
            for (int i = 0; i < _states.Length; i++)
            {
                if (_states[i] is not T)
                    continue;
                
                stateIndex = i;
                break;
            }

            if (stateIndex == -1)
                return false;
            
            var nextState = _states[stateIndex];
            if (!nextState.CanChangeInThisState)
                return false;
            
            currentState.OnExit(this);
            nextState.OnStart(this);

            _currentIndex = stateIndex;
            return true;
        }

        private void Update() => _states[_currentIndex].OnUpdate(this);
    }
}