using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class smg_base_weapon_script : MonoBehaviour
{
    public KeyCode interact_key = KeyCode.Mouse0;
    public GameObject projectile;
    public Transform projectile_spawn_transform_L;
    public Transform projectile_spawn_transform_R;
    public ParticleSystem muzzle_flash_R;
    public ParticleSystem muzzle_flash_L;
    public float fire_rate = 0.1f;
    public float projectile_speed = 150f;

    private float next_fire = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interact_key))
        {
            //muzzle flash
            muzzle_flash_R.Play();
            muzzle_flash_L.Play();
        }

        if (Input.GetKey(interact_key) && Time.time > next_fire)
        {
            //print("shooting");
            GameObject bulletInstance_right = Instantiate(projectile, projectile_spawn_transform_R.position, projectile_spawn_transform_R.rotation);
            bulletInstance_right.GetComponent<Rigidbody>().AddForce(transform.forward * projectile_speed);
            GameObject bulletInstance_left = Instantiate(projectile, projectile_spawn_transform_L.position, projectile_spawn_transform_L.rotation);
            bulletInstance_left.GetComponent<Rigidbody>().AddForce(transform.forward * projectile_speed);

            //play sound effect

            
            //Camera Shake
            CameraShaker.Instance.ShakeOnce(1.5f, 4f, 0.1f, 0.05f);

            //wait for shot based on fire rate
            next_fire = Time.time + fire_rate;
        }

        if (Input.GetKeyUp(interact_key))
        {
            //stop muzzle flash
            muzzle_flash_R.Stop();
            muzzle_flash_L.Stop();
        }
    }
}
