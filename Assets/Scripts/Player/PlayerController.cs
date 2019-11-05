using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private User user;
    private Rigidbody rb;
    private PlayerInput input;
    private const float speed = 10f;
    private Vector2 mov;

    public void SetUser(User value)
    {
        user = value;
        input = GetComponent<PlayerInput>();
        if (user.GetControllerId() == -1)
            input.defaultControlScheme = "KeyboardMouse";
        else
            input.defaultControlScheme = "Gamepad";
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mov = Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(mov.x, rb.velocity.y, mov.y) * speed;
    }

    public void OnMove(InputValue value)
    {
        mov = value.Get<Vector2>();
    }
}
