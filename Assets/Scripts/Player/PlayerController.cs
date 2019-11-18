using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private WeaponType weaponType;

    [SerializeField]
    private Transform gunEnd;

    [SerializeField]
    private GameObject bulletPrefab, grenadePrefab;

    private Rigidbody rb;
    private float speed = 10f; // Player speed
    private AWeapon weapon; // Player weapon
    private User user;

    public void SetUser(User value)
    {
        user = value;
        switch (user.GetGameplayClass())
        {
            case User.GameplayClass.Rifleman:
            case User.GameplayClass.Conjuror:
            case User.GameplayClass.Brawler:
                weaponType = WeaponType.SMG;
                break;

            case User.GameplayClass.Grenadier:
                weaponType = WeaponType.GrenadeLauncher;
                break;

            default:
                throw new ArgumentException("Invalid gameplay class type " + user.GetGameplayClass().ToString());
        }
        GetComponent<PlayerAI>()?.SetGameplayClass(user.GetGameplayClass());
    }

    public User GetUser()
        => user;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        switch (weaponType)
        {
            case WeaponType.SMG:
                weapon = gameObject.AddComponent<SMG>();
                weapon.Init(bulletPrefab, gunEnd);
                break;

            case WeaponType.GrenadeLauncher:
                weapon = gameObject.AddComponent<GrenadeLauncher>();
                weapon.Init(grenadePrefab, gunEnd);
                break;

            default:
                throw new ArgumentException("Invalid weapon type " + weaponType.ToString());
        }
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
        SMG,
        GrenadeLauncher
    }
}
