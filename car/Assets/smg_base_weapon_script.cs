using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class smg_base_weapon_script : MonoBehaviour
{

    //Main SMG Components

    public KeyCode interact_key = KeyCode.Mouse0;

    [Header("Components")]
    public GameObject projectile;
    public Transform projectile_spawn_transform_L;
    public Transform projectile_spawn_transform_R;
    public ParticleSystem muzzle_flash_R;
    public ParticleSystem muzzle_flash_L;

    [Header("Parameters")]
    public float projectile_speed = 2000f;
    public float fire_rate = 0.07f;

    //Private SMG Components

    private float next_fire = 0.0f;

    // Update is called once per frame
    void Update()
    {
        check_for_input();
    }

    //checks for player input
    private void check_for_input()
    {
        //if input is pressed
        if (Input.GetKeyDown(interact_key))
        {
            //start muzzle flash fx
            muzzle_fx_on();
        }

        //if input is held AND it's time to fire another shot
        if (Input.GetKey(interact_key) && Time.time > next_fire)
        {
            //fire projectile
            fire_projectile();

            //wait for shot based on fire rate
            next_fire = Time.time + fire_rate;
        }

        //if input is released
        if (Input.GetKeyUp(interact_key))
        {
            //stop muzzle flash fx
            muzzle_fx_off();
        }
    }

    //starts muzzle flash fx
    private void muzzle_fx_on()
    {
        muzzle_flash_R.Play();
        muzzle_flash_L.Play();
    }

    //stops muzzle flash fx
    private void muzzle_fx_off()
    {
        muzzle_flash_R.Stop();
        muzzle_flash_L.Stop();
    }

    //fires projectile along with sound and camera fx
    private void fire_projectile()
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
