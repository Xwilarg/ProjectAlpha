using UnityEngine;

public class Grenade : ABullet
{
    public PlayerStats Stats { set; private get; }
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
        foreach (var collider in Physics.OverlapSphere(explosionPos, grenadeRadius, 1 << 10)) // Detect enemies on grenade range
        {
            if (!Physics.Linecast(collider.transform.position, explosionPos, 1 << 9 | 1 << 16)) // Is the enemy protected by a shield?
                collider.GetComponent<Character>()?.LooseHp(5, Stats);
        }
        camShake.ShakeMe(shakeForce, shakeDuration);
    }
}
