using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackdamage : MonoBehaviour {
    public StateController controller;
    public PlayerHealth player;
    RaycastHit hit;
    private float nextDamageEvent;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        player.TakeDamage(0);


        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.black);
        if ((Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.attackRange)
            && hit.collider.CompareTag("Player"))) {
            if (Time.time >= nextDamageEvent)
            {
                nextDamageEvent = Time.time + controller.enemyStats.attackRate;

                player.TakeDamage(controller.enemyStats.attackDamage);
            }
            else
            {
                //nextDamageEvent = Time.time + controller.enemyStats.attackRate;
                player.TakeDamage(0);

            }
        }

    }
}
