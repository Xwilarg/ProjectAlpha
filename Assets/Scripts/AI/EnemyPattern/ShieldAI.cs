using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(NavMeshAgent))]
public class ShieldAI : MonoBehaviour
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
        float distance;
        var closest = AIUtilities.GetClosestTransform(transform.position, ai.Humans, out distance).position;
        if (distance < 10f)
            agent.SetDestination(transform.position);
        else
            agent.SetDestination(closest);
    }
}
