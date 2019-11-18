using UnityEngine;

public abstract class ABullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("PlayerHuman") && !collision.collider.CompareTag("PlayerAI"))
        {
            OnCollision(collision);
        }
        Destroy(gameObject);
    }

    protected abstract void OnCollision(Collision collision);
}
