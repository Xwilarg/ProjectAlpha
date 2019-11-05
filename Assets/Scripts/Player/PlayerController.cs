using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private User user;

    private Rigidbody rb;

    private const float speed = 10f;

    private PlayerInput input;

    public void SetUser(User value)
    {
        if (user.GetControllerId() == -1)
            input.defaultControlScheme = "KeyboardMouse";
        else
            input.defaultControlScheme = "Gamepad";
        user = value;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        rb.velocity = user.GetMovement() * speed;
    }
}
