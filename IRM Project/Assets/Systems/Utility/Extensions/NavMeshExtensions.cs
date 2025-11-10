using UnityEngine.AI;

namespace IRM.Utility
{
    public static class NavMeshExtensions
    {
        public static bool HasReachedTarget(this NavMeshAgent agent)
        {
            if (agent.pathPending)
                return false;

            if(agent.remainingDistance > agent.stoppingDistance)
                return false;
            
            return !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
        }
    }
}