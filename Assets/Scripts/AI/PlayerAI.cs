using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAI : MonoBehaviour
{
    private PlayerController pc;
    private Transform[] _humans;

    public void Init(Transform[] humans)
    {
        _humans = humans;
    }

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }
}
