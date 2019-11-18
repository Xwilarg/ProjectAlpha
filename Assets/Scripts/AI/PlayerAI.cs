using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerController))]
public class PlayerAI : MonoBehaviour
{
    private PlayerController pc;
    private Transform[] _humans;
    private NavMeshAgent agent;
    private PlayerManager pm;
    private User.GameplayClass gameplayClass;

    public void SetGameplayClass(User.GameplayClass value)
        => gameplayClass = value;

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
            if (Physics.SphereCast(transform.position, .5f, new Vector3(transform.position.x - dest.position.x, 0f, transform.position.z - dest.position.z), out hitInfo, float.MaxValue, ~(1 << 8)))
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
        int finalLayer;
        if (gameplayClass == User.GameplayClass.Grenadier)
            finalLayer = ~(1 << gameObject.layer | 1 << 9); // Grenadier can shoot over shields
        else
            finalLayer = ~(1 << gameObject.layer);
        Transform t = AIUtilities.GetClosestTransformInSight(transform.position, pm.GetEnemies(), out _, ~(1 << gameObject.layer));
        if (t != null)
        {
            float distance = Vector3.Distance(transform.position, t.position);
            if (gameplayClass == User.GameplayClass.Grenadier && distance > 10f) // Grenadiers can't shoot too far
                return;
            var angle = Mathf.Atan2(t.position.z - transform.position.z, t.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            pc.SetRotation(Quaternion.Euler(0f, -angle, 0f));
            pc.Shoot();
        }
    }
}
