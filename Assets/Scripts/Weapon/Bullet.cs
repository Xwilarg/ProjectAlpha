using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
            collision.collider.GetComponent<EnemyAI>().LooseHp(1);
        Destroy(gameObject);
    }
}
