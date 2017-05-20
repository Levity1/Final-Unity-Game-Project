using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 5.0f;
    public float jumpSpeed = 20.0f;

    float vRotation = 0;
    public float yRange = 60.0f;

    float vertVelocity = 0;

    CharacterController player;

	// Use this for initialization
	void Start () {
        Screen.lockCursor = true;
        player = GetComponent<CharacterController>();
		Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {

        float rotate = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotate, 0);

        vRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        vRotation = Mathf.Clamp(vRotation, -yRange, yRange);
        Camera.main.transform.localRotation = Quaternion.Euler(vRotation, 0, 0);

        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        vertVelocity += Physics.gravity.y * Time.deltaTime;
        
        if(player.isGrounded && Input.GetButtonDown("Jump") ) {
            vertVelocity = jumpSpeed;
        }

        Vector3 speed = new Vector3(sideSpeed, vertVelocity, forwardSpeed);

        speed = transform.rotation * speed;

        player.Move(speed * Time.deltaTime);
	}
}
