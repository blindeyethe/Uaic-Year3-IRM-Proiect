using UnityEngine;

namespace IRM.Utility
{
    internal sealed class RotateHandInteractable : MonoBehaviour
    {
        [SerializeField] private Vector3 rotation;

        public Vector3 Rotation => rotation;
    }
}