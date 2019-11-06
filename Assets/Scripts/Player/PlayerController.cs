using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform gunEnd;

    [SerializeField]
    private GameObject bulletPrefab;

    private User user;
    private Rigidbody rb;
    private const float speed = 10f;
    private float reloadTimer;

    private Gun gun;

    public void SetUser(User value)
        => user = value;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gun = Gun.handgun;
        reloadTimer = 0f;
    }

    private void Update()
    {
        reloadTimer -= Time.deltaTime;
        if (reloadTimer < 0f && user.GetKey("fireMain"))
        {
            Shoot();
            reloadTimer = gun.GetReloadTime();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = user.GetMovement() * speed;
    }

    private void Shoot()
    {
        GameObject go = Instantiate(bulletPrefab, gunEnd.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * gun.GetFireForce(), ForceMode.Impulse);
        Destroy(go, 5f);
    }
}
