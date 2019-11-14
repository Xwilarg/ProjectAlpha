using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerController))]
public class ShieldAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyAI ai;
    private PlayerController pc;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ai = GetComponent<EnemyAI>();
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        float distance;
        var closest = AIUtilities.GetClosestTransform(transform.position, ai.Humans, out distance).position;
        if (distance < 10f)
        {
            agent.SetDestination(transform.position);
            var angle = Mathf.Atan2(closest.z - transform.position.z, closest.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            pc.SetRotation(Quaternion.Euler(0f, -angle, 0f));
        }
        else
            agent.SetDestination(closest);
    }
}
