using UnityEngine;

namespace Systems.AnimalSystem
{
    [CreateAssetMenu(fileName = "Animal Data", menuName = "Animal System/Animal Data")]
    public class AnimalData : ScriptableObject
    {
        [Header("Movement")]
        [field:SerializeField] public float WalkingSpeed { get; private set; }
        [field:SerializeField] public float RunningSpeed { get; private set; }
        [field:SerializeField] public float FleeDistance { get; private set; }
        
        [Header("Durations")]
        [field:SerializeField] public float IdleDuration { get; private set; }
        [field:SerializeField] public float AlertDuration { get; private set; }
        
        [Header("Probabilities")]
        [field:SerializeField, Range(0, 1)] public float PerformActionChance { get; private set; }
        
        [Header("Drinking")]
        [field:SerializeField] public float ThirstCooldown { get; private set; }
        [field:SerializeField] public float DrinkDuration { get; private set; }
        
        [Header("Detection")]
        [field:SerializeField] public float TotalDetectionRange { get; private set; }
        [field:SerializeField] public float PlayerDetectionRange { get; private set; }
        [field:SerializeField] public float PlayerFleeRange { get; private set; }
        [field:SerializeField, Range(0, 1)] public float PlayerUndetectedThreshold { get; private set; }

        public float PlayerDetectionRangeSqr =>  PlayerDetectionRange * PlayerDetectionRange;
        public float PlayerFleeRangeSqr =>  PlayerFleeRange * PlayerFleeRange;
    }
}