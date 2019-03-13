using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAnim : MonoBehaviour {
    private Animator anim;
    private bool done = false;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();     
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Growing", true);
	}
    
    public Animator GetAnim()
    {
        return anim;
    }

    private void Growth()
    {
        done = true;
    }

    public bool isDone()
    {
        return done;
    }
    
}
