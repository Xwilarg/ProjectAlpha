using UnityEngine;

public class Grenade : ABullet
{
    protected override void OnCollision(Collision collision)
    {
        var enemy = collision.collider.GetComponent<EnemyAI>();
        if (enemy != null)
            enemy.LooseHp(1);
    }
}
