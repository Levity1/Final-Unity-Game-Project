using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour {

    Text ammoText;

    // Use this for initialization.
    void Awake () {
        ammoText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
      //  ammoText.text = GetComponent<NewGuns>().currentAmmo.ToString();
	}
}
