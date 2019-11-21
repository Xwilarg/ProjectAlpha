using UnityEngine;

public class GunBullet : ABullet
{
    protected override void OnCollision(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
            collision.collider.GetComponent<Character>()?.LooseHp(1);
    }
}
