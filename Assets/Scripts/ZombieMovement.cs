using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour {
    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
    Animator anim;

    public Transform head = null;
    public Vector3 lookAtTargetPosition;
    private Vector3 lookAtPosition;
    public bool looking = true;
    private float lookAtWeight = 0.0f;

    public float rotationSpeed = 10f;


    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void OnAnimatorMove()
    {
        // Update position based on animation movement using navigation surface height
        //		Vector3 position = anim.rootPosition;
        //		position.y = nav.nextPosition.y;
        //		transform.position = position;
        print("in Anim move");
        RotateTowards(player);
    }


    void Update()
    {
        // If the enemy and the player have health left...
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            // ... set the destination of the nav mesh agent to the player.
            MoveTowards(player);
            RotateTowards(player);
            anim.SetBool("isRun", true);
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
  //          anim.SetBool("Idle", true);
       
        }
    }

    private void MoveTowards(Transform target)
    {
        nav.SetDestination(target.position);

    }

    private void RotateTowards(Transform target)
    {
        //		Vector3 direction = (target.position - transform.position).normalized;
        //		Quaternion lookRotation = Quaternion.LookRotation(direction);
        //		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);


        var targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
