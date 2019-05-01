using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCollision : MonoBehaviour {
    private float stunDuration;
    private GameObject log;
    private float time = 0.0f;
    private bool hit = false;

	// Use this for initialization
	void Start () {
        log = Resources.Load("LogProjectile") as GameObject;
        stunDuration = log.GetComponentInChildren<LogProjectile>().StunTime();
	}
	
	// Update is called once per frame
	void Update () {
        if (hit == true)
        {
            time += Time.deltaTime;
            if(time >= stunDuration)
            {
                Destroy(transform.parent.gameObject);
                hit = false;
            }
        }
        Debug.Log(time);
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Boundary" || col.gameObject.tag == "Platform")
        {
            hit = true;
        }
        if (col.gameObject.tag == "Player")
        {
            time = 0.0f;
            hit = true;
        }
    }
}

