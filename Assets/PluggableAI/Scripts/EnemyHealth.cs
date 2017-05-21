using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public GameObject enemy;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }


    void Update()
    {
        if (isDead)
        {
            StartSinking();
        }
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        //enemyAudio.Play();
        currentHealth -= amount;
        anim.SetTrigger("Hurt");
        //hitParticles.transform.position = hitPoint;
        //hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        enemyAudio.PlayOneShot(deathClip);
        isDead = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //capsuleCollider.isTrigger = true;
        anim.SetBool("isRun", false);
        anim.SetTrigger("isDead");
       // enemyAudio.PlayOneShot(deathClip);
		ScoreManager.score += scoreValue;
    }


    public void StartSinking()
    {
        
        // GetComponent<Rigidbody>().isKinematic = true;
   //     isSinking = true;
        if (startingHealth == 101)
        {
            enemy.SetActive(false);
        }
        else { 
        //ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
        }
    }

}
