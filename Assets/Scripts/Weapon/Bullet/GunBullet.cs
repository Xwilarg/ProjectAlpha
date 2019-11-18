using UnityEngine;

public class GunBullet : ABullet
{
    protected override void OnCollision(Collision collision)
    {
        var enemy = collision.collider.GetComponent<EnemyAI>();
        if (enemy != null)
            enemy.LooseHp(1);
    }
}
