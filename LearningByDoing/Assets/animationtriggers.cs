using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationtriggers : MonoBehaviour
{

    private GameObject avatar;
    private Animator animator;
	// Use this for initialization
	void Start () {
		avatar = GameObject.Find("DefaultAvatar");
	    animator = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update ()
	{

	    float h = Input.GetAxis("Horizontal");
	    float v = Input.GetAxis("Vertical");
        animator.SetFloat("Speed", v);
        animator.SetFloat("Direction", h);

	    if (Input.GetKeyDown(KeyCode.DownArrow))
	    {
	        v = v - 0.1f;
	    }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            h = h + 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            h = h - 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            v = v + 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
    }
}
