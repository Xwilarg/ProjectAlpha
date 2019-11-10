using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerController))]
public class PlayerAI : MonoBehaviour
{
    private PlayerController pc;
    private Transform[] _humans;
    private NavMeshAgent agent;
    private PlayerManager pm;

    public void Init(Transform[] humans)
    {
        _humans = humans;
    }

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        pm = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        SetDestination();
        ShootEnemy();
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
    private void ShootEnemy()
    {
        Transform t = AIUtilities.GetClosestTransformInSight(transform.position, pm.GetEnemies(), out _, ~(1 << gameObject.layer));
        if (t != null)
        {
            var angle = Mathf.Atan2(t.position.z, t.position.x) * Mathf.Rad2Deg - 90f;
            pc.SetRotation(Quaternion.Euler(0f, -angle, 0f));
            pc.Shoot();
        }
    }
}
