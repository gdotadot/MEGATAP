using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogProjectile : MonoBehaviour {
    private TrapBase trapBase;

    private bool hit = false;
    private GameObject player = null;
    private Renderer[] child;

    // Use this for initialization
    void Start () {
        trapBase = GetComponent<TrapBase>();
        child = this.GetComponentsInChildren<Renderer>();
        hit = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
