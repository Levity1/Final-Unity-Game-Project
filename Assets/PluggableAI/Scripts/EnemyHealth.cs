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

        //hitParticles.transform.position = hitPoint;
        //hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        //capsuleCollider.isTrigger = true;

        anim.SetTrigger("die");
        enemyAudio.PlayOneShot(deathClip);
    }


    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
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
