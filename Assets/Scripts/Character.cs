using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int hp;

    private bool isEnemy;
    private PlayerManager pm;

    private void Start()
    {
        isEnemy = GetComponent<EnemyAI>() != null;
        if (isEnemy)
        {
            pm = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
        }
    }

    public void LooseHp(int amount, PlayerStats stats)
    {
        hp -= amount;
        if (hp <= 0)
        {
            if (isEnemy)
            {
                pm.DeleteEnemy(transform);
                stats.EnemyKilled++;
            }
            Destroy(gameObject);
        }
    }

    public int GetHP()
        => hp;
}
