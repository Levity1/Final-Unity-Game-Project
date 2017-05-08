using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewGuns : ScriptableObject
{

    public string weaponName = "Weapon Name Here";

    public int damage = 10;
    public float range = 50;
    public int currentAmmo = 2;
    public int maxClip = 10;
    public int totalAmmo = 15;
    public float timeBetweenBullets = 0.15f;
    public float reload = 1f;
    public int swapTime = 2;

}
