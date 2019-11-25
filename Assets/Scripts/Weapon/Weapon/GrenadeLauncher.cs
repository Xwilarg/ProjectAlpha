using UnityEngine;

public class GrenadeLauncher : AWeapon
{
    private float _reloadTime = 2f;
    private float _fireForce = 7f;
    private GameObject _bulletPrefab;
    private Transform _gunEnd; // From where the bullet is shoot
    private PlayerStats _stats;

    public override void Fire()
    {
        if (!CanShoot())
            return;
        GameObject go = Instantiate(_bulletPrefab, _gunEnd.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * _fireForce, ForceMode.Impulse);
        go.GetComponent<Grenade>().Stats = _stats;
        Destroy(go, 5f);
        Reload();
    }

    protected override float GetReloadTime()
        => _reloadTime;

    public override void Init(GameObject bulletPrefab, Transform gunEnd, PlayerStats stats)
    {
        _bulletPrefab = bulletPrefab;
        _gunEnd = gunEnd;
        _stats = stats;
    }
}
