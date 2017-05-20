using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animationtriggers : MonoBehaviour
{

    private GameObject avatar;
    private Animator animator;
    public float fullHealth;
    public float currentHealth;
    private GameObject greenBar;
    private Text healthText;
    private Canvas GUI;
    private int healthStat;
    private bool alive;
    
    private Canvas GUI_kafu;
    private Image healthbar;
    
	// Use this for initialization
	void Start () {
		avatar = GameObject.Find("DefaultAvatar");
	    animator = GetComponent<Animator>();
	    currentHealth = fullHealth;
        greenBar = GameObject.Find("fill - full health");
	    GUI = GameObject.Find("GUI").GetComponent<Canvas>();
	    healthText = GUI.GetComponentInChildren<Text>();
	    healthStat = 100;
	    alive = true;
        
        GUI_kafu = GameObject.Find("GUI_KaFu_canvas").GetComponent<Canvas>();
        healthbar = GameObject.Find("fill_content").GetComponent<Image>();
	}
    
    void getTiredAndIll() {
        float health = healthbar.fillAmount;
        if(0 <= health && health <=1) {
            Debug.Log(health);
            healthbar.fillAmount -= 0.01f; 
        }
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



    void updateHealth() {
        if ((healthStat > 0) && (alive))
        {
            healthStat = Convert.ToInt32((currentHealth / fullHealth) * 100);
            healthText.text = "Health: " + healthStat + "%";
        }

    }
    // Update is called once per frame
    void Update () {
        updateHealth();
       
	    float h = Input.GetAxis("Horizontal");
	    float v = Input.GetAxis("Vertical");
        animator.SetFloat("Speed", v);
        animator.SetFloat("Direction", h);
        
        float v_check = v;

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
        
        if(Input.GetKeyDown("1")) {
            animator.SetLayerWeight(1, 1f);
            animator.SetBool("eating", true);
        } 
        if(Input.GetKeyUp("1")){
            animator.SetBool("eating", false);
        }
        
        if(v_check != v) {
            getTiredAndIll();   
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
            Heal();
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "floor")
        {
            healthText.text = "DEAD";
            greenBar.transform.localScale = new Vector3(0, 1, 1);
            alive = false;
        }
    }
}
