using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeWeapons : MonoBehaviour
{
    int buffer;
    Gun gun;
    PlayerHealth playerhealth;
    public GameObject player;
    public GameObject[] weapons;
    AMMOTEST ammo1;
    AMMOTEST ammo2;
    AMMOTEST ammo3;

    private AudioSource myAudioSource;
    public AudioClip healthPickUp;
    public AudioClip ammoPickUp;

    public GameObject guntest;
    // Use this for initialization
    void Start()
    {
        playerhealth = player.GetComponent<PlayerHealth>();
        gun = guntest.GetComponent<Gun>();
        ammo1 = weapons[0].GetComponent<AMMOTEST>();
        ammo2 = weapons[1].GetComponent<AMMOTEST>();
        ammo3 = weapons[2].GetComponent<AMMOTEST>();
        buffer = 1;
        SelectWeapon(1);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(weapons);
        if (Input.GetButtonDown("Primary Weapon")  && gun.pistollock == true && buffer != 0) SelectWeapon(0);
        if (Input.GetButtonDown("Secondary Weapon") && buffer != 1) SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) && gun.riflelock == true && buffer !=2) SelectWeapon(2);

    }
    public void SelectWeapon(int index)
    {
        buffer = index;
//		GameObject.FindGameObjectWithTag ("Gun").GetComponent<Animator> ().SetTrigger ("gun_swap");
        foreach (GameObject weapon in weapons) { weapon.SetActive(false); }
        weapons[index].SetActive(true);
        Gun.changeAnim();
    }
    public void addAmmo()
    {
        ammo1.totalAmmo += 20;
        ammo2.totalAmmo += 40;
        ammo3.totalAmmo += 30;

        if (ammo1.totalAmmo > 100) ammo1.totalAmmo = 100;
        if (ammo1.totalAmmo > 200) ammo1.totalAmmo = 200;
        if (ammo1.totalAmmo > 150) ammo1.totalAmmo = 150;

        //   SelectWeapon(1);
        //  gun.AddAmmo();
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("ammo"))
        {
            //   myAudioSource.PlayOneShot(ammoPickUp);
            AudioSource.PlayClipAtPoint(ammoPickUp, transform.position);
            addAmmo();
            Destroy(target.gameObject);
        }
        if (target.CompareTag("health"))
        {
            AudioSource.PlayClipAtPoint(healthPickUp, transform.position);
            playerhealth.TakeDamage(-30);
            Destroy(target.gameObject);
        }
    }
}
