using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeWeapons : MonoBehaviour {
    public GameObject[] weapons;
	// Use this for initialization
	void Start () {
        SelectWeapon(1);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Primary Weapon")) SelectWeapon(0);
        if (Input.GetButtonDown("Secondary Weapon")) SelectWeapon(1);

    }
    void SelectWeapon(int index)
    {
        foreach (GameObject weapon in weapons) { weapon.SetActive(false); }
        weapons[index].SetActive(true);
    }
}
