using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerHuman : MonoBehaviour
{
    private PlayerController pc;
    private User user; // For inputs

    public void SetUser(User value)
        => user = value;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
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
