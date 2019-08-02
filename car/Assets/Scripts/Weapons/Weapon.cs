using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Weapon : MonoBehaviour
{
    public string currentWeapon;
    public KeyCode interact_key = KeyCode.Mouse0;

    [Header("Components")]
    public GameObject projectile;
    public GameObject[] projectileTypes;
    public Transform projectile_spawn_transform_L;
    public Transform projectile_spawn_transform_R;
    public ParticleSystem muzzle_flash_R;
    public ParticleSystem muzzle_flash_L;

    [Header("Parameters")]
    public float projectile_speed = 2000f;
    public float fireRate = 0.07f;

    //Private SMG Components

    private float nextFire = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(currentWeapon == "bow")
        {
            projectile = projectileTypes[0];
        }
        else if(currentWeapon == "smg"){
            projectile = projectileTypes[1];
        }
        else if(currentWeapon == "shotgun")
        {
            projectile = projectileTypes[2];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if input is pressed
        if (Input.GetKeyDown(interact_key))
        {
            //start muzzle flash fx
            MuzzleFXOn();
        }

        //if input is held AND it's time to fire another shot
        if (Input.GetKey(interact_key) && Time.time > nextFire)
        {
            //fire projectile
            Fire();

            //wait for shot based on fire rate
            nextFire = Time.time + fireRate;
        }

        //if input is released
        if (Input.GetKeyUp(interact_key))
        {
            //stop muzzle flash fx
            MuzzleFXOff();
        }


    }
    private void MuzzleFXOn()
    {
        muzzle_flash_R.Play();
        muzzle_flash_L.Play();
    }

    //stops muzzle flash fx
    private void MuzzleFXOff()
    {
        muzzle_flash_R.Stop();
        muzzle_flash_L.Stop();
    }

    //fires projectile along with sound and camera fx
    private void Fire()
    {
        //print("shooting");
        GameObject bulletInstance_right = Instantiate(projectile, projectile_spawn_transform_R.position, projectile_spawn_transform_R.rotation);
        bulletInstance_right.GetComponent<Rigidbody>().AddForce(projectile_spawn_transform_R.forward * projectile_speed);
        GameObject bulletInstance_left = Instantiate(projectile, projectile_spawn_transform_L.position, projectile_spawn_transform_L.rotation);
        bulletInstance_left.GetComponent<Rigidbody>().AddForce(projectile_spawn_transform_L.forward * projectile_speed);

        //play sound effect


        //Camera Shake
        CameraShaker.Instance.ShakeOnce(1.5f, 4f, 0.1f, 0.05f);
    }
}
