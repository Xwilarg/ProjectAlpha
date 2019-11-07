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
        SetDestination();
    }

    private void SetDestination()
    {
        float dist;
        Transform dest = AIUtilities.GetClosestTransform(transform.position, _humans, out dist);
        if (dist < 2f)
            agent.SetDestination(Vector3.MoveTowards(transform.position, dest.position, -1f));
        else if (dist < 3f)
            agent.SetDestination(transform.position);
        else if (dist > 3.5f)
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, .5f, dest.position, out hitInfo, float.MaxValue, ~(1 << 8)))
            {
                if (hitInfo.collider.CompareTag("PlayerAI"))
                {
                    if (hitInfo.distance < 3f)
                        agent.SetDestination(transform.position);
                }
            }
            agent.SetDestination(dest.position);
        }
    }
}
