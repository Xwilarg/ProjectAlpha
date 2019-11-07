using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision _)
    {
        Destroy(gameObject);
    }
}
