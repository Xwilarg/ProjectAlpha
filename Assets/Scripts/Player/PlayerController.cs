﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform gunEnd; // From where the bullet is shoot

    [SerializeField]
    private GameObject bulletPrefab;

    private Rigidbody rb;
    private float speed = 10f; // Player speed
    private float reloadTimer; // Timer to check when the player can next shoot
    private Shake camShake; // To shake the camera
    private Gun gun; // Player weapon

    private const float shakeForce = 1f; // Force of the screen shake;
    private const float shakeDuration = .1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gun = Gun.handgun;
        reloadTimer = 0f;
        camShake = Camera.main.GetComponent<Shake>();
    }

    private void Update()
    {
        reloadTimer -= Time.deltaTime;
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
        if (reloadTimer < 0f)
        {
            GameObject go = Instantiate(bulletPrefab, gunEnd.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(transform.forward * gun.GetFireForce(), ForceMode.Impulse);
            Destroy(go, 5f);
            camShake.ShakeMe(shakeForce, shakeDuration);
            reloadTimer = gun.GetReloadTime();
        }
    }
}
