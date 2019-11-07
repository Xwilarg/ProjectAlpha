using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(NavMeshAgent))]
public class RushAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyAI ai;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ai = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        agent.SetDestination(AIUtilities.GetClosestTransform(transform.position, ai.Humans, out _).position);
    }
}
