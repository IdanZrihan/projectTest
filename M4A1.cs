using System;
using System.Collections;
using UnityEngine;

public class M4A1 : MonoBehaviour
{
    Vector3 origin;
    RaycastHit hit;
    public GameObject fpsCam;
    public float damage = 45f;
    public float maxDistance = 500f;
    public ParticleSystem muzzleFlashl;
    public GameObject impactEffect;

    int bullets = 30;
    int maxAmmo = 120;

    public float fireRate = 15f;
    float nextTimeToFire = 0f;
    bool isReloading = false;

    void Update()
    {
        if (isReloading)
        {
            return;
        }

       
        if (Input.GetButton("Fire1") && bullets > 0 && Time.time >= nextTimeToFire)
        {
            mouseShoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && bullets < 30 && maxAmmo > 0)
        {
            StartCoroutine(Reload());
        }
        else if (bullets == 0)
        {
            StartCoroutine(Reload());
        }
    }

    void mouseShoot()
    {
        nextTimeToFire = Time.time + 1f / fireRate;
        Shoot();
        Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        bullets--;
        print("You have now " + bullets + " bullets");
    }

    void Shoot()
    {
        origin = fpsCam.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        muzzleFlashl.Play();

        if (Physics.Raycast(origin, fpsCam.transform.forward, out hit, maxDistance))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        print("Reloading...");
        
        yield return new WaitForSeconds(3);

        int bulletsToLoad = Mathf.Min(30 - bullets, maxAmmo);
        bullets += bulletsToLoad;
        maxAmmo -= bulletsToLoad;

        print("The current bullets in the magazine: " + bullets);
        print("The remaining ammo: " + maxAmmo);

        isReloading = false;
    }
}
