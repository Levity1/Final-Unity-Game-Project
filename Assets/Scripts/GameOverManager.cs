using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public float restartDelay = 5f;

	Animator anim;
	float restartTimer;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	
	void Update ()
	{
		// If the player has run out of health...
		if(playerHealth.currentHealth <= 0)
		{
			// ... tell the animator the game is over.
			anim.SetTrigger ("GameOver");

			// .. increment a timer to count up to restarting.
			restartTimer += Time.deltaTime;

			// .. if it reaches the restart delay...
			if(restartTimer >= restartDelay)
			{
				// .. then reload the currently loaded level.
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
}
