using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public GameObject player;
    public float flashSpeed = 5f;
    public int startingHealth = 100;
    public float time = 5;
    public int currentHealth;
    public Slider Healthbar;
    public Text messagetext;
    public Slider healthSlider;
    public Image damageImage;
    //public AudioClip deathClip;
    NavMeshAgent agent;
    Animator anim;
    //AudioSource playerAudio;
 //   PlayerMovement playerMovement;
    bool isDead;
    bool damaged;

    // Use this for initialization
    void Start()
    {
    //    anim = GetComponent<Animator>();
        //playerAudio = GetComponent <AudioSource> ();
     //   playerMovement = GetComponent<PlayerMovement>();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        if (isDead)
        {
            messagetext.text = "You Died, restarting game";

            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }

        }

    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;
       

            Healthbar.value -= amount;
      
        healthSlider.value = currentHealth;

        //playerAudio.Play ();

        if (currentHealth <= 0 && !isDead)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Death();
        }
    }

    void Death()
    {
      //  messagetext.text = "You Died, restarting game";

        //agent.enabled = false;
        isDead = true;
        // player.SetActive(false);
        SceneManager.LoadScene(0);
        if (Time.time >= 11)
        {
                 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        //playerShooting.DisableEffects ();

        //anim.SetTrigger ("Die");

        //playerAudio.clip = deathClip;
        //playerAudio.Play ();

        //playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }


    public void RestartLevel ()
	{
		SceneManager.LoadScene (0);
	}


}
