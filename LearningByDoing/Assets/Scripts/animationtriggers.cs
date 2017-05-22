using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animationtriggers : MonoBehaviour
{

    private GameObject avatar;
    private Transform fattyTransform;
    private Animator animator;
    public float fullHealth;
    public float currentHealth;
    private GameObject greenBar;
    private Text healthText;
    private Canvas GUI;
    private int healthStat;
    private bool alive;
    private float speed;
	// Use this for initialization
	void Start () {
		avatar = GameObject.Find("Fatty");
	    animator = GetComponent<Animator>();
	    currentHealth = fullHealth;
        greenBar = GameObject.Find("fill - full health");
	    GUI = GameObject.FindObjectOfType<Canvas>();
	    healthText = GUI.GetComponentInChildren<Text>();
	    healthStat = 100;
	    alive = true;
	    fattyTransform = avatar.GetComponent<Transform>();
	    speed = 0.05f;

	}

    void DamageHealth()
    {
        if (healthStat > 0)
        {
            currentHealth -= 5;
            greenBar.transform.localScale = new Vector3(currentHealth / fullHealth, 1, 1);
            //scales bar in x direction, scaled by percentage
        }
        else
        {
            healthText.text = "DEAD";
            alive = false;
        }
    }

    void Heal()
    {
        if (healthStat < 100)
        {
            currentHealth += 5;
            greenBar.transform.localScale = new Vector3(currentHealth / fullHealth, 1, 1);
            //scales bar in x direction, scaled by percentage
        }
    }



    void updateHealth()
    {
        if ((healthStat > 0) && (alive))
        {
            healthStat = Convert.ToInt32((currentHealth / fullHealth) * 100);
            healthText.text = "Health: " + healthStat + "%";
        }

    }
    // Update is called once per frame
    void Update ()
    {
        updateHealth();

       
        
        float h = Input.GetAxis("Horizontal");
	    float v = Input.GetAxis("Vertical");

        fattyTransform.Translate(new Vector3(h * speed, 0, v * speed));

        animator.SetFloat("Speed", v);
        animator.SetFloat("Direction", h);

	    if (Input.GetKeyDown(KeyCode.S))
	    {
	        v = v - 0.001f;
           
	    }
        if (Input.GetKeyDown(KeyCode.A))
        {
            h = h + 0.001f;

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            h = h - 0.001f;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            v = v + 0.001f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("grab");

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("wave");
        }
        
        if(Input.GetKeyDown(KeyCode.E)){
            animator.SetLayerWeight(1, 1f);
            animator.SetTrigger("eat");
        }
        
        if(Input.GetKeyDown(KeyCode.R)){
            animator.SetTrigger("pick");
        }
   

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "hazard")
        {
            DamageHealth();
        }
        if (col.gameObject.tag == "food")
        {
            //col.gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(col.gameObject);
            animator.SetLayerWeight(1, 1f);
            animator.SetTrigger("eat");
            Heal();
        }

        if (col.gameObject.tag == "hangingfood")
        {
            //col.gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(col.gameObject);
            animator.SetLayerWeight(1, 1f);
            animator.SetTrigger("pick");
            Heal();
        }

    }

    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.tag == "floor")
    //    {
    //        healthText.text = "DEAD";
    //        greenBar.transform.localScale = new Vector3(0, 1, 1);
    //        alive = false;
    //    }
    //}
}
