using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private AIType type;

    public Transform[] Humans { set; get; }

    public enum AIType
    {
        Rush
    }

    private void Start()
    {
        switch (type)
        {
            case AIType.Rush:
                gameObject.AddComponent<RushAI>();
                break;

            default:
                throw new ArgumentException("Invalid AI type " + type.ToString());
        }
    }
}
