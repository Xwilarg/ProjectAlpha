using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private AIType type;

    public Transform[] Humans { set; get; }
    private int hp;
    private PlayerManager pm;

    public enum AIType
    {
        Rush,
        Shield
    }

    private void Start()
    {
        switch (type)
        {
            case AIType.Rush:
                gameObject.AddComponent<RushAI>();
                break;

            case AIType.Shield:
                gameObject.AddComponent<ShieldAI>();
                break;

            default:
                throw new ArgumentException("Invalid AI type " + type.ToString());
        }
        hp = 5;
        pm = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
    }

    public void LooseHp(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            pm.DeleteEnemy(transform);
            Destroy(gameObject);
        }
    }
}
