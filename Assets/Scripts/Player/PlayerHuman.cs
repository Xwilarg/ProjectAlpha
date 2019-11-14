using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerHuman : MonoBehaviour
{
    private PlayerController pc;
    private User user; // For inputs

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        user = pc.GetUser();
    }

    private void Update()
    {
        pc.SetVelocity(user.GetMovement());
        pc.SetRotation(user.GetRotation(transform.position));
        if (user.GetKey("fireMain"))
        {
            pc.Shoot();
        }
    }
}
