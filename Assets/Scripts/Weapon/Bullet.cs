using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            var enemy = collision.collider.GetComponent<EnemyAI>();
            if (enemy != null)
                enemy.LooseHp(1);
        }
        Destroy(gameObject);
    }
}
