using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //this.gameObject.GetComponent<Renderer>().

    }

    // Update is called once per frame
    void Update() {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerOneStats>().pickupCount >= 3)
        {
            Color currColor = this.gameObject.GetComponent<Renderer>().material.color;
            currColor.a = 0.0f;
            this.gameObject.GetComponent<Renderer>().material.color = currColor;
        } else
        {
            Color currColor = this.gameObject.GetComponent<Renderer>().material.color;
            currColor.a = 1f;
            this.gameObject.GetComponent<Renderer>().material.color = currColor;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
            other.GetComponent<PlayerOneStats>().pickupCount++;
        }
    }
}
