using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private User user;

    private Rigidbody rb;

    private const float speed = 10f;

    public void SetUser(User value)
        => user = value;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = user.GetMovement() * speed;
    }
}
