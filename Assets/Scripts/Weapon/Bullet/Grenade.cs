using UnityEngine;

public class Grenade : ABullet
{
    private Shake camShake;
    private const float shakeForce = 1f; // Force of the screen shake
    private const float shakeDuration = .1f;
    private const float grenadeRadius = 2f;

    private void Start()
    {
        camShake = Camera.main.GetComponent<Shake>();
    }

    protected override void OnCollision(Collision collision)
    {
        Vector3 explosionPos = collision.GetContact(0).point;
        foreach (var collider in Physics.OverlapSphere(explosionPos, grenadeRadius, 1))
        {
            var enemy = collider.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                if (Physics.Linecast(transform.position, explosionPos, ~(1 << 9))) // Is the enemy protected by a shield?
                    enemy.LooseHp(5);
            }
        }
        camShake.ShakeMe(shakeForce, shakeDuration);
    }
}
