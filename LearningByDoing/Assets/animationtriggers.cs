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
	void Update () {
	    if (Input.GetKeyDown(KeyCode.W))
	    {
	        animator.SetTrigger("forward");
	    }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("grab");
        }

    }
}
