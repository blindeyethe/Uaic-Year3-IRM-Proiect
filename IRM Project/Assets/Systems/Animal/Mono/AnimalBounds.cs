using UnityEngine;

namespace Systems.AnimalSystem
{
    public class AnimalBounds : MonoBehaviour
    {
        [SerializeField] private LayerMask _terrainMask;
        
        private BoxCollider _collider;
        private Bounds _bounds;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _bounds = _collider.bounds;
        }

        public Vector3 GetRandomPosition()
        {
            var randomX = Random.Range(_bounds.min.x, _bounds.max.x);
            var randomZ = Random.Range(_bounds.min.z, _bounds.max.z);
            
            var rayPosition = new Vector3(randomX, _bounds.max.y, randomZ);
            Physics.Raycast(rayPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, _terrainMask);
            
            return hit.point;
        }
        
        public Vector3 GetFleePosition(Vector3 animalPosition, Vector3 playerPosition, float fleeDistance)
        {
            var fleeDirection = (animalPosition - playerPosition).normalized;
            var rayPosition = animalPosition + fleeDirection * fleeDistance;
            
            rayPosition.y = _bounds.max.y;
            Physics.Raycast(rayPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, _terrainMask);
            
            return hit.point;
        }
    }
}