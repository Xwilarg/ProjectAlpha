using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private WeaponType weaponType;

    [SerializeField]
    private Transform gunEnd;

    [SerializeField]
    private GameObject bulletPrefab;

    private Rigidbody rb;
    private float speed = 10f; // Player speed
    private AWeapon gun; // Player weapon

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        switch (weaponType)
        {
            case WeaponType.SMG:
                gun = gameObject.AddComponent<SMG>();
                break;

            default:
                throw new ArgumentException("Invalid weapon type " + weaponType.ToString());
        }
        gun.Init(bulletPrefab, gunEnd);
    }

    public void SetVelocity(Vector3 vel)
    {
        rb.velocity = vel * speed;
    }

    public void SetRotation(Quaternion rot)
    {
        transform.rotation = rot;
    }

    public void Shoot()
    {
        gun.Fire();
    }

    public enum WeaponType
    {
        SMG
    }
}
