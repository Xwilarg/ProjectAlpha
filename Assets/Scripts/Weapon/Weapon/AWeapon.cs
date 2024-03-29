﻿using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    protected AWeapon()
    {
        _refReloadTime = GetReloadTime();
        _reloadTime = _refReloadTime;
    }

    public abstract void Init(GameObject bulletPrefab, Transform gunEnd, PlayerStats stats);
    protected abstract float GetReloadTime();
    public abstract void Fire();

    private void Update()
    {
        _reloadTime -= Time.deltaTime;
    }

    public bool CanShoot()
        => _reloadTime < 0f;

    public float GetRemainingReloadTime()
        => _reloadTime;

    protected void Reload()
        => _reloadTime = _refReloadTime;

    private readonly float _refReloadTime;
    private float _reloadTime;
}
