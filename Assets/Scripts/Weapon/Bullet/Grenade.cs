using UnityEngine;

public class Grenade : ABullet
{
    private Shake camShake;
    private const float shakeForce = 1f; // Force of the screen shake
    private const float shakeDuration = .1f;

    private void Start()
    {
        camShake = Camera.main.GetComponent<Shake>();
    }

    protected override void OnCollision(Collision collision)
    {
        var enemy = collision.collider.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.LooseHp(1);
        }
        camShake.ShakeMe(shakeForce, shakeDuration);
    }
}
