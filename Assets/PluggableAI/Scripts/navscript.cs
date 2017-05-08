using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class navscript : MonoBehaviour {
  //  public GameObject  target;
    NavMeshAgent agent;
    Animator anim;
    public Text m_MessageText;
    PlayerHealth playerHealth;
    GameObject player;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = GetComponent<PlayerHealth>();
        
    }
	
	// Update is called once per frame
	void Update () {
        float h = agent.velocity.x;
        float v = agent.velocity.z;
        Move(h, v);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

          if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {  
           /*           string test = "www";
                    m_MessageText.text =  test;
                if (target != null)
                {
                    //  target.transform.position= target.transform.position; ;
                    // agent.destination = target.transform.position;
               //     agent.updatePositio{ ntarget.transform.position,true }

                }
                if (hit.collider.CompareTag("Enemy"))
                {
                  //  if (target = null)
                  //  {
                        m_MessageText.text = "wwwwaasfas";

                        target = hit.transform.gameObject;
                    agent.SetDestination(target.transform.position);
                  //  }
                    //agent.destination = transform.position;

                }*/
              ///  else
              //  {
                    agent.destination = hit.point;
             //   }
                anim.SetBool("isRunning", true);

            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            agent.ResetPath();

        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health Pack"))
        {
            other.gameObject.SetActive(false);
			playerHealth.addHealth(25);
        }

        if (other.gameObject.CompareTag("Weapon Pack"))
        {
            other.gameObject.SetActive(false);

        }
    }*/

    void Move(float h, float v)
    {
        bool running = h != 0f || v != 0f;
        anim.SetBool("isRunning", running);
    }
    private void Chase(StateController controller)
    {
        controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.Resume();
    }
}
