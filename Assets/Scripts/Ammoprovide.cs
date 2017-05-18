using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammoprovide : MonoBehaviour {

   public int holder;
    // Use this for initialization
    void Awake()
    {



    }
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * Time.deltaTime*40);
    }

}
