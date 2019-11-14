using UnityEngine;

public sealed class SMG : AWeapon
{
    private float _reloadTime = .1f;
    private float _fireForce = 50f;
    private GameObject _bulletPrefab;
    private Transform _gunEnd; // From where the bullet is shoot
    private Shake camShake; // To shake the camera
    private const float shakeForce = .1f; // Force of the screen shake;
    private const float shakeDuration = .1f;

    private void Start()
    {
        camShake = Camera.main.GetComponent<Shake>();
    }

    public override void Init(GameObject bulletPrefab, Transform gunEnd)
    {
        _bulletPrefab = bulletPrefab;
        _gunEnd = gunEnd;
    }

    protected override float GetReloadTime()
        => _reloadTime;

    public override void Fire()
    {
        if (!CanShoot())
            return;
        GameObject go = Instantiate(_bulletPrefab, _gunEnd.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * _fireForce, ForceMode.Impulse);
        Destroy(go, 5f);
        camShake.ShakeMe(shakeForce, shakeDuration);
        Reload();
    }

    private SMG() : base()
    { }
}
