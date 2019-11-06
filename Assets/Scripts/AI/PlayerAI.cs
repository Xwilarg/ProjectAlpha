using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerController))]
public class PlayerAI : MonoBehaviour
{
    private PlayerController pc;
    private Transform[] _humans;
    private NavMeshAgent agent;

    public void Init(Transform[] humans)
    {
        _humans = humans;
    }

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float dist;
        Transform dest = AIUtilities.GetClosestTransform(transform.position, _humans, out dist);
        if (dist < 3f)
            agent.SetDestination(transform.position);
        else
            agent.SetDestination(dest.position);
    }
}
