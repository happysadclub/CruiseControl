using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Gun_Powerup : MonoBehaviour {

    //General
    public bool GunPowerup = false;
    public KeyCode interactKey = KeyCode.Mouse0;

    //General Shooting
    public float bulletForce = 1000f;
    public int currentClipAmmo = 0;
    public int maxClipAmmo = 17;
    public float fireRate = 0.1f;
    public float reloadTime_seconds = 1f;
    private float fireRateTimer = 0f;

    //Bullet
    public GameObject bullet;
    public Transform bulletSpawn_front_right_transform;
    public Transform bulletSpawn_front_left_transform;
    public Transform bulletSpawn_back_right_transform;
    public Transform bulletSpawn_back_left_transform;

    //Gun Model
    public GameObject gun_model_front_right;
    public GameObject gun_model_front_left;
    public GameObject gun_model_back_right;
    public GameObject gun_model_back_left;

    //Bullet Casing
    public ParticleSystem casing_right_system;
    public ParticleSystem casing_left_system;

    
    //Shoot Control
    private bool frontGuns = true;
    private bool reloading = false;

    //Muzzle Flash
    public ParticleSystem muzzleFlash_front_r;
    public ParticleSystem muzzleFlash_front_l;
    public ParticleSystem muzzleFlash_back_r;
    public ParticleSystem muzzleFlash_back_l;

    //Audio
    public AudioSource source;
    public AudioClip gunShot_clip;
    public AudioClip reload_clip;

    //Boosting
    public float frontGuns_boost = -2000;
    public float backGuns_boost = 40000;
	
	// Update is called once per frame
	void Update () {
        //GunVisibilityControl();
        if (GunPowerup)
        {
            shootController();
            BoostController();
        }
	}

    private void shootController()
    {
        testCarController carControllerScript = GetComponent<testCarController>();

        if (Input.GetKeyUp(interactKey))
        {
            //disable casing particle effect
            casing_right_system.Stop();
            casing_left_system.Stop();
            //disable particle effect
            muzzleFlash_back_l.Stop();
            muzzleFlash_back_r.Stop();
            muzzleFlash_front_l.Stop();
            muzzleFlash_front_r.Stop();

            if (frontGuns)
            {
                StartCoroutine(reloadFront());
            }
            else if (!frontGuns)
            {
                StartCoroutine(reloadBack());
            }
        }

        if (Input.GetKey(interactKey))
        {
            if (frontGuns && !reloading)
            {
                fireRateTimer += Time.deltaTime;
                if (fireRateTimer > fireRate)
                {
                    //print("shooting");
                    GameObject bulletInstance_right = Instantiate(bullet, bulletSpawn_front_right_transform.position, bulletSpawn_front_right_transform.rotation);
                    bulletInstance_right.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce);
                    GameObject bulletInstance_left = Instantiate(bullet, bulletSpawn_front_left_transform.position, bulletSpawn_front_left_transform.rotation);
                    bulletInstance_left.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce);

                    //play sound effect
                    source.clip = gunShot_clip;
                    source.Play();
                    source.pitch += .02f;

                    //muzzle flash
                    muzzleFlash_front_r.Play();
                    muzzleFlash_front_l.Play();

                    //enable casing particle effect
                    casing_right_system.Play();
                    casing_left_system.Play();

                    //increase used ammo
                    currentClipAmmo++;

                    //Camera Shake
                    CameraShaker.Instance.ShakeOnce(1.5f, 4f, 0.1f, 0.05f);

                    fireRateTimer = 0f;
                }
                if (currentClipAmmo >= maxClipAmmo && !reloading)
                {
                    //RELOAD
                    StartCoroutine(reloadFront());
                }
            }
            else if (!frontGuns && !reloading)
            {
                fireRateTimer += Time.deltaTime;
                if (fireRateTimer > fireRate)
                {
                    //print("shooting");
                    GameObject bulletInstance_right = Instantiate(bullet, bulletSpawn_back_right_transform.position, bulletSpawn_back_right_transform.rotation);
                    bulletInstance_right.GetComponent<Rigidbody>().AddForce(-transform.forward * bulletForce);
                    GameObject bulletInstance_left = Instantiate(bullet, bulletSpawn_back_left_transform.position, bulletSpawn_back_left_transform.rotation);
                    bulletInstance_left.GetComponent<Rigidbody>().AddForce(-transform.forward * bulletForce);

                    //play sound effect
                    source.clip = gunShot_clip;
                    source.Play();
                    source.pitch += .02f;

                    //muzzle flash
                    muzzleFlash_back_r.Play();
                    muzzleFlash_back_l.Play();

                    //enable casing particle effect
                    casing_right_system.Play();
                    casing_left_system.Play();

                    //increase used ammo
                    currentClipAmmo++;

                    //Camera Shake
                    CameraShaker.Instance.ShakeOnce(1.5f, 4f, 0.1f, 0.05f);

                    fireRateTimer = 0f;
                }
                if (currentClipAmmo >= maxClipAmmo && !reloading)
                {
                    //RELOAD
                    StartCoroutine(reloadBack());
                }
            }

        }
    }

    IEnumerator reloadFront()
    {
        reloading = true;
        //reset pitch
        source.pitch = 1f;
        //disable casing particle effect
        casing_right_system.Stop();
        casing_left_system.Stop();
        //stop muzzle flash
        muzzleFlash_front_r.Stop();
        muzzleFlash_front_l.Stop();
        //switch gun model
        gun_model_front_right.SetActive(false);
        gun_model_front_left.SetActive(false);
        gun_model_back_right.SetActive(true);
        gun_model_back_left.SetActive(true);
        //play sound effect
        source.clip = reload_clip;
        source.Play();
        yield return new WaitForSeconds(reloadTime_seconds);
        currentClipAmmo = 0;
        frontGuns = false;
        reloading = false;
    }

    IEnumerator reloadBack()
    {
        reloading = true;
        //reset pitch
        source.pitch = 1f;
        //disable casing particle effect
        casing_right_system.Stop();
        casing_left_system.Stop();
        //disable muzzle flash
        muzzleFlash_back_r.Stop();
        muzzleFlash_back_l.Stop();
        //switch gun model
        gun_model_back_right.SetActive(false);
        gun_model_back_left.SetActive(false);
        gun_model_front_right.SetActive(true);
        gun_model_front_left.SetActive(true);
        //play sound effect
        source.clip = reload_clip;
        source.Play();
        yield return new WaitForSeconds(reloadTime_seconds);
        currentClipAmmo = 0;
        frontGuns = true;
        reloading = false;
    }

    private void GunVisibilityControl()
    {
        if (!GunPowerup)
        {
            //disable game objects
            gun_model_front_right.SetActive(false);
            gun_model_front_left.SetActive(false);
            gun_model_back_right.SetActive(false);
            gun_model_back_left.SetActive(false);
        }
    }

    private void BoostController()
    {
        Movement_and_Boost_Powerup movement_script = GetComponent<Movement_and_Boost_Powerup>();
        if(Input.GetKey(interactKey) && !reloading)
        {
            if (frontGuns)
            {
                //turn off boost
                movement_script.boosting = false;
                //turn on slowdown
                movement_script.slowdown = true;

                //switch thrust
                movement_script.thrust = frontGuns_boost;
            }
            else
            {
                //turn off slowdown
                movement_script.slowdown = false;
                //turn on boost
                movement_script.boosting = true;

                //switch thrust
                movement_script.thrust = backGuns_boost;
            }
        }
        else
        {
            //turn off boost
            movement_script.boosting = false;
            movement_script.slowdown = false;

            //switch thrust
            movement_script.thrust = movement_script.forwardAcceleration;
        }
    }
}
