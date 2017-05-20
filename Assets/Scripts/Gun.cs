using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{/*
   
    public int damage = 50;
    public int currentAmmo = 2;
    public int maxClip = 10;
    public int totalAmmo = 15;
    public float timeBetweenBullets = 0.15f;
    public float range = 50;
    public float reload = 1f;
    */
    public bool riflelock = false;
    public bool pistollock = false;
    public Text text;
    AMMOTEST test;
    public GameObject guntest;
    private bool reloading = false;
    private bool swap = false;
    public NewGuns[] weapons;
    public int currentWeapon = 0;
    public EnemyHealth enemy;
    public GameObject controller;
    public int swapTime = 2;

    float timer;
    public ParticleSystem gunParticles;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    int shootableMask;

    private AudioSource myAudioSource;
    public AudioClip shootSound;
    public AudioClip enemyHurt;
    public AudioClip reload_rifle;
    //public AudioClip reloadSound;

    private Transform maincamera;
    static Animator anim;

    // Use this for initialization


    void Start()
    {
        text.text = "";
        maincamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        shootableMask = LayerMask.GetMask("Shootable");
        myAudioSource = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        anim = GameObject.FindGameObjectWithTag("Gun").GetComponent<Animator>();
    }

    // Update is called once per frame

    //Shoot and play particle effects on click
    void Update()
    {
        guntest = GameObject.FindGameObjectWithTag("Gun");
        test = guntest.GetComponent<AMMOTEST>();
        timer += Time.deltaTime;
        gunParticles = GetComponentInChildren<ParticleSystem>();

        if (Input.GetButtonDown("Fire1") && reloading != true && timer >= weapons[currentWeapon].timeBetweenBullets && swap != true && test.currentAmmo > 0 && currentWeapon == 0 && pistollock == true)
        {
            if (test.currentAmmo > 0)
            {
                Shoot();
                anim.SetTrigger("shoot_pistol");
            }

            if (test.currentAmmo == 0)
                Reload();
        }
        if (Input.GetButton("Fire1") && reloading != true && timer >= weapons[currentWeapon].timeBetweenBullets && swap != true && test.currentAmmo > 0 && currentWeapon == 1)
        {
            if (test.currentAmmo > 0)
            {
                Shoot();
                anim.SetTrigger("uzi_shoot");
            }
            if (test.currentAmmo == 0)
                Reload();
        }
        if (Input.GetButton("Fire1") && reloading != true && timer >= weapons[currentWeapon].timeBetweenBullets && swap != true && test.currentAmmo > 0 && currentWeapon == 2 && riflelock == true)
        {
            if (test.currentAmmo > 0)
            {
                Shoot();
                anim.SetTrigger("rifle_shoot");
            }
            if (test.currentAmmo == 0)
                Reload();
        }

        if (timer >= weapons[currentWeapon].timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        if (Input.GetButtonDown("Reload") && test.currentAmmo != weapons[currentWeapon].maxClip)
        {
            Reload();
        }

        if (reloading != true && Input.GetButtonDown("Primary Weapon") && pistollock == true && currentWeapon != 0)
        {
            myAudioSource.Stop();
            StartCoroutine(Swap());
            currentWeapon = 0;
        }

        if (reloading != true && Input.GetButtonDown("Secondary Weapon") && currentWeapon != 1)
        {
            myAudioSource.Stop();
            StartCoroutine(Swap());
            currentWeapon = 1;
        }
        if (reloading != true && Input.GetKeyDown(KeyCode.Alpha3) && riflelock == true && currentWeapon != 2)
        {
            myAudioSource.Stop();
            StartCoroutine(Swap());
            currentWeapon = 2;
        }
        Debug.Log(currentWeapon);
        text.text = test.currentAmmo + " / " + test.totalAmmo;
    }

    public void DisableEffects()
    {
        gunLight.enabled = false;
    }

    //check for enemy dmg
    void Shoot()
    {
        timer = 0f;

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        Ray ray = new Ray(maincamera.position, maincamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, weapons[currentWeapon].range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                controller = hit.transform.gameObject;

                EnemyHealth enemyHealth = controller.GetComponent<EnemyHealth>();
                if (enemyHealth.currentHealth > 0/*&&distance <= 4*/)
                {
                    // ... the enemy should take damage.

                    myAudioSource.PlayOneShot(enemyHurt);
                    enemyHealth.TakeDamage(weapons[currentWeapon].damage);
                }
                Debug.Log("We Hit");
            }
        }

        myAudioSource.PlayOneShot(shootSound);
        test.currentAmmo--;
    }
    //Reload the clip when not already reloading
    void Reload()
    {
        anim.SetTrigger("reload");

        StartCoroutine(Reloading());

    }

    private IEnumerator Reloading()
    {   //Refills maximum amount of ammo
        reloading = true;

        yield return new WaitForSeconds(weapons[currentWeapon].reload);


        if (test.totalAmmo > weapons[currentWeapon].maxClip)
        {
            test.totalAmmo += test.currentAmmo;
            test.currentAmmo = weapons[currentWeapon].maxClip;
            test.totalAmmo -= weapons[currentWeapon].maxClip;
        }
        //Makes sure the currentAmmo isn't greater than the maxClip
        if (test.currentAmmo + test.totalAmmo > weapons[currentWeapon].maxClip)
        {
            test.totalAmmo += test.currentAmmo;
            test.currentAmmo = weapons[currentWeapon].maxClip;
            test.totalAmmo -= test.currentAmmo;
        }
        //Refills the rest of the ammo you have
        else if (test.totalAmmo <= weapons[currentWeapon].maxClip)
        {
            test.totalAmmo += test.currentAmmo;
            test.currentAmmo = test.totalAmmo;
            test.totalAmmo = 0;
        }
        reloading = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("riflepickup"))
        {
            Destroy(other.gameObject);
            riflelock = true;
        }
        if (other.gameObject.CompareTag("pistolpickup"))
        {
            Destroy(other.gameObject);
            pistollock = true;
        }
    }

    private IEnumerator Swap()
    {
        swap = true;
        yield return new WaitForSeconds(swapTime);
        swap = false;
    }

    public static void changeAnim()
    {
        anim = GameObject.FindGameObjectWithTag("Gun").GetComponent<Animator>();
    }

}
