using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public GameObject controller;
    private float nextDamageEvent;
    public EnemyHealth enemy;
    public GameObject player;
    public PlayerHealth regen;
    RaycastHit hit;
    Animator anim;
	SphereCollider range;
    public int strength = 0;

	public float timeBetweenSwings = 1.0f;

	float timer;

	// Use this for initialization
	void Start () {
		//enemy = LayerMask.GetMask ("Enemy");
		range = GetComponent<SphereCollider> ();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
        enemy = null;

        if (Input.GetMouseButtonDown(1)&& timer >= timeBetweenSwings && Time.timeScale != 0) 
		{
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    controller = hit.transform.gameObject;
                    var distance = Vector3.Distance(controller.transform.position, player.transform.position);

                    EnemyHealth enemyHealth = controller.GetComponent<EnemyHealth>();
                    if (enemyHealth != null/*&&distance <= 4*/)
                    {
                        // ... the enemy should take damage.
                        enemyHealth.TakeDamage(50+strength);
                    }
                    if(enemyHealth.currentHealth <= 0)
                    {
                        strength += 10;
                        regen.TakeDamage(-30);
                    }
                    //    targetscript.damager(dmggiven * times.deltaTime);
                }
                else
                {
                    enemy = null;
                }
            }
        /*if ((Physics.Raycast(controller.transform.position, hit, 100)
                //Physics.SphereCast(controller.transform.position,4, controller.transform.forward, out hit, 3)
       ))
        {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(50);
            }

        }*/
			Swing ();
		}
	}

	void Swing ()
    {
      
        //if (Time.time >= nextDamageEvent)
        // {
        // nextDamageEvent = Time.time + controller.enemyStats.attackRate;

        //       enemy.TakeDamage(controller.enemyStats.attackDamage);
        // }
        // else
        // {
        //nextDamageEvent = Time.time + controller.enemyStats.attackRate;
        //   enemy.TakeDamage(0);

        //  }

        timer = 0f;

		anim.SetTrigger ("attack");

	}

}
