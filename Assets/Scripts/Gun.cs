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
    //public AudioClip reloadSound;

    private Transform maincamera;
	Animator anim;

    // Use this for initialization


    void Start()
    {
        text.text = "";
        maincamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        shootableMask = LayerMask.GetMask("Shootable");
        myAudioSource = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
		anim = GameObject.FindGameObjectWithTag ("Gun").GetComponent<Animator>();
    }

    // Update is called once per frame

    //Shoot and play particle effects on click
    void Update()
    {
        guntest = GameObject.FindGameObjectWithTag("Gun");
        test = guntest.GetComponent<AMMOTEST>();
        timer += Time.deltaTime;
        gunParticles = GetComponentInChildren<ParticleSystem>();

        if (Input.GetButtonDown("Fire1") && reloading != true && timer >= weapons[currentWeapon].timeBetweenBullets && swap != true && test.currentAmmo > 0)
        {
            if (test.currentAmmo > 0)
                Shoot();
            else if (test.totalAmmo > 0)
                Reload();
        }
        if (Input.GetButton("Fire1") && reloading != true && timer >= weapons[currentWeapon].timeBetweenBullets && swap != true && test.currentAmmo > 0 &&currentWeapon == 1 )
        {
            if (test.currentAmmo > 0)
                Shoot();
            else if (test.totalAmmo > 0)
                Reload();
        }
        if (Input.GetButton("Fire1") && reloading != true && timer >= weapons[currentWeapon].timeBetweenBullets && swap != true && test.currentAmmo > 0 &&  currentWeapon == 2)
        {
            if (test.currentAmmo > 0)
                Shoot();
            else if (test.totalAmmo > 0)
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

        if (Input.GetButtonDown("Primary Weapon"))
        {
            StartCoroutine(Swap());
            currentWeapon = 0;
        }

        if (Input.GetButtonDown("Secondary Weapon")) {
            StartCoroutine(Swap());
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(Swap());
            currentWeapon = 2;
        }
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
		anim.SetTrigger ("shoot");

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
                if (enemyHealth != null/*&&distance <= 4*/)
                {
                    // ... the enemy should take damage.
                    myAudioSource.PlayOneShot(enemyHurt);
                    enemyHealth.TakeDamage(weapons[currentWeapon].damage );
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
        if (reloading)
            return;
        StartCoroutine(Reloading());

    }

    private IEnumerator Reloading()
    {   //Refills maximum amount of ammo
        reloading = true;
		anim.SetTrigger ("reload");
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



    private IEnumerator Swap()
    {
        swap = true;
        yield return new WaitForSeconds(swapTime);
        swap = false;
    }

}
