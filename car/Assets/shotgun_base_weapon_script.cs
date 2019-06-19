using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class shotgun_base_weapon_script : MonoBehaviour
{
    public GameObject projectile_object;
    public ParticleSystem projectile_particle_system;
    public Transform projectile_spawnpoint;
    public float projectile_speed = 150f;
    public int projectile_count = 5;
    public float spread_factor = 0.01f;
    public float fire_rate = 0.5f;
    public float charged_fire_rate = 0.05f;
    public float charge_rate = 0.1f;
    public int max_charge = 8;
    List<Quaternion> projectile_list;
    List<Quaternion> charged_projectile_list;

    private float next_fire = 0.0f;
    private float next_charge = 0.0f;
    private int charged_num = 1;
    private bool charged = false;

    // Start is called before the first frame update
    void Awake()
    {
        //instantiate projectiles
        projectile_list = new List<Quaternion>(projectile_count);
        for (int i = 0; i < projectile_count; i++)
        {
            projectile_list.Add(Quaternion.Euler(Vector3.zero));
        }

        //instantiate charged projectiles
        charged_projectile_list = new List<Quaternion>(projectile_count * 2);
        for (int i = 0; i < projectile_count * 2; i++)
        {
            charged_projectile_list.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if the input is clicked and you're not on cooldown
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > next_fire && !charged)
        {
            //fire projectiles
            fire_projectiles();

            //spawn particles
            projectile_particle_system.Play();

            //camera shake
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 0.5f);

            //wait for shot based on fire rate
            next_fire = Time.time + fire_rate;
        }

        //if key is released and shotgun has charge
        if (Input.GetKeyUp(KeyCode.Mouse0) && charged)
        {
            StartCoroutine(fire_charged_projectiles());
        }

        //if the input is held down and you're not on cooldown
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > next_fire)
        {
            //start charging up shots
            if (Time.time > next_charge && charged_num <= max_charge)
            {
                //set charged bool
                charged = true;
                
                //add a charge
                charged_num += 1;
                print(charged_num);

                //wait for shot based on fire rate
                next_charge = Time.time + charge_rate;
            }
        }
    }

    //handles firing all shotgun projectiles
    void fire_projectiles()
    {
        if (charged)
        {
            int i = 0;
            foreach (Quaternion quat in charged_projectile_list)
            {
                charged_projectile_list[i] = Random.rotation;
                GameObject projectile = Instantiate(projectile_object, projectile_spawnpoint.position, projectile_spawnpoint.rotation);
                projectile.transform.rotation = Quaternion.RotateTowards(projectile.transform.rotation, charged_projectile_list[i], spread_factor);
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.right * projectile_speed);
            }
        }
        else
        {
            int i = 0;
            foreach (Quaternion quat in projectile_list)
            {
                projectile_list[i] = Random.rotation;
                GameObject projectile = Instantiate(projectile_object, projectile_spawnpoint.position, projectile_spawnpoint.rotation);
                projectile.transform.rotation = Quaternion.RotateTowards(projectile.transform.rotation, projectile_list[i], spread_factor);
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.right * projectile_speed);
            }
        }
    }

    IEnumerator fire_charged_projectiles()
    {
        //set charged spread
        spread_factor = spread_factor * 2f;
        
        //fire all the charged shots
        for (int z = 0; z < charged_num; z++)
        {
            //perform shotgun blast
            fire_projectiles();
            print("SHOT");
            yield return new WaitForSeconds(charged_fire_rate);
        }

        //reset charge bool
        charged = false;

        //reset shotgun charge amount
        charged_num = 1;

        //reset spread
        spread_factor = spread_factor / 2f;
    }
}
