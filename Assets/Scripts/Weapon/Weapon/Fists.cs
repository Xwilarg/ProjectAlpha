using UnityEngine;

public class Fists : AWeapon
{
    private float _reloadTime = .1f;
    private float _fistRange = 1f;
    private Transform _gunEnd;
    private PlayerStats _stats;

    public override void Fire()
    {
        if (!CanShoot())
            return;
        RaycastHit hitInfo;
        Debug.DrawRay(_gunEnd.position, _gunEnd.position - transform.position, Color.red);
        if (Physics.Raycast(_gunEnd.position, _gunEnd.position - transform.position, out hitInfo, _fistRange))
        {
            if (hitInfo.collider.CompareTag("Enemy"))
                hitInfo.collider.GetComponent<Character>()?.LooseHp(5, _stats);
        }
        Reload();
    }

    protected override float GetReloadTime()
        => _reloadTime;

    public override void Init(GameObject bulletPrefab, Transform gunEnd, PlayerStats stats)
    {
        _gunEnd = gunEnd;
        _stats = stats;
    }
}
