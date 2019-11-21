using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private AIType type;

    public Transform[] Humans { set; get; }

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
    }
}
