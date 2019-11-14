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
    private AWeapon weapon; // Player weapon
    private User user;

    public void SetUser(User value)
        => user = value;

    public User GetUser()
        => user;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        switch (weaponType)
        {
            case WeaponType.SMG:
                weapon = gameObject.AddComponent<SMG>();
                break;

            default:
                throw new ArgumentException("Invalid weapon type " + weaponType.ToString());
        }
        weapon.Init(bulletPrefab, gunEnd);
        user.SetWeapon(weapon);
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
        weapon.Fire();
    }

    public enum WeaponType
    {
        SMG
    }
}
