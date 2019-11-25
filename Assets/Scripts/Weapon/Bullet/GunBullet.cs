using UnityEngine;

public class GunBullet : ABullet
{
    public PlayerStats Stats { set; private get; }

    protected override void OnCollision(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
            collision.collider.GetComponent<Character>()?.LooseHp(1, Stats);
    }
}
