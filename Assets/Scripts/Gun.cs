using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool reloading = false;
    private bool swap = false;
    public NewGuns[] weapons;
    public int currentWeapon = 0;
    public EnemyHealth enemy;
    public GameObject controller;


    float timer;
    ParticleSystem gunParticles;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    int shootableMask;

    private AudioSource myAudioSource;
    public AudioClip shootSound;
    //public AudioClip reloadSound;

    private Transform maincamera;

    // Use this for initialization


    void Start()
    {
        maincamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        myAudioSource = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    // Update is called once per frame

    //Shoot and play particle effects on click
    void Update()
    {

        timer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && reloading != true && timer >= weapons[currentWeapon].timeBetweenBullets && weapons[currentWeapon].currentAmmo > 0 && swap != true)
        {
            if (weapons[currentWeapon].currentAmmo > 0)
                Shoot();
            else if (weapons[currentWeapon].totalAmmo > 0)
                Reload();
        }

        if (timer >= weapons[currentWeapon].timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        if (Input.GetButtonDown("Reload") && weapons[currentWeapon].currentAmmo != weapons[currentWeapon].maxClip)
        {
            Reload();
        }
        if (Input.GetButtonDown("Primary Weapon"))
        {
            if(currentWeapon == 0)
            {
                return;
            }else
            {
                SwapWeapon();
                currentWeapon = 0;
            }
           
        }
        if (Input.GetButtonDown("Secondary Weapon"))
        {
            if (currentWeapon == 1)
            {
                return;
            }else
            {
                SwapWeapon();
                currentWeapon = 1;
            }
        }
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
                if (enemyHealth != null/*&&distance <= 4*/)
                {
                    // ... the enemy should take damage.
                    enemyHealth.TakeDamage(weapons[currentWeapon].damage );
                }
                Debug.Log("We Hit");
            }
        }

        myAudioSource.PlayOneShot(shootSound);
        weapons[currentWeapon].currentAmmo--;
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
        yield return new WaitForSeconds(weapons[currentWeapon].reload);

        if (weapons[currentWeapon].totalAmmo > weapons[currentWeapon].maxClip)
        {
            weapons[currentWeapon].totalAmmo += weapons[currentWeapon].currentAmmo;
            weapons[currentWeapon].currentAmmo = weapons[currentWeapon].maxClip;
            weapons[currentWeapon].totalAmmo -= weapons[currentWeapon].maxClip;
        }
        //Makes sure the currentAmmo isn't greater than the maxClip
        if (weapons[currentWeapon].currentAmmo + weapons[currentWeapon].totalAmmo > weapons[currentWeapon].maxClip)
        {
            weapons[currentWeapon].totalAmmo += weapons[currentWeapon].currentAmmo;
            weapons[currentWeapon].currentAmmo = weapons[currentWeapon].maxClip;
            weapons[currentWeapon].totalAmmo -= weapons[currentWeapon].currentAmmo;
        }
        //Refills the rest of the ammo you have
        else if (weapons[currentWeapon].totalAmmo <= weapons[currentWeapon].maxClip)
        {
            weapons[currentWeapon].totalAmmo += weapons[currentWeapon].currentAmmo;
            weapons[currentWeapon].currentAmmo = weapons[currentWeapon].totalAmmo;
            weapons[currentWeapon].totalAmmo = 0;
        }
        reloading = false;
    }

    void SwapWeapon()
    {
        StartCoroutine(Swap());
    }

    private IEnumerator Swap()
    {
        swap = true;
        yield return new WaitForSeconds(weapons[currentWeapon].swapTime);
        swap = false;
    }
}
