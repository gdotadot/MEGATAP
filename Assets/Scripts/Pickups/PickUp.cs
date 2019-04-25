using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    private float speedUpDuration = 5f;
    private float speedUpMultiplier = 3f;
    // active correlates to whether or not the pickup is faded out or not
    private bool active = true;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update() {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerOneStats>().pickupCount == 3)
        {
            // turn object transparent
            Color currColor = this.gameObject.GetComponent<Renderer>().material.color;
            currColor.a = 0.0f;
            this.gameObject.GetComponent<Renderer>().material.color = currColor;
        } else
        {
            // make it no longer transparent
            Color currColor = this.gameObject.GetComponent<Renderer>().material.color;
            currColor.a = 1f;
            this.gameObject.GetComponent<Renderer>().material.color = currColor;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && active == true)
        {
            Debug.Log(active);
            active = false;
            Debug.Log(other.GetComponent<PlayerOneStats>().pickupCount);
            other.GetComponent<PlayerOneStats>().pickupCount++;
            Debug.Log(other.GetComponent<PlayerOneStats>().pickupCount);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log(other.GetComponent<PlayerOneStats>().pickupCount);

            // initiate speed up
            if (GameObject.FindWithTag("Player").GetComponent<PlayerOneStats>().pickupCount >= 3)
            {
                StartCoroutine(SpeedBoost(other));
            }
        }
    }

    IEnumerator SpeedBoost(Collider other)
    {
        Debug.Log("Speed boost activated");
        Debug.Log(other.GetComponent<PlayerOneMovement>().GetSpeed());
        other.GetComponent<PlayerOneMovement>().SetSpedUp(true);
        other.GetComponent<PlayerOneMovement>().SetSpeed(other.GetComponent<PlayerOneMovement>().GetSpeed() * speedUpMultiplier);
        Debug.Log(other.GetComponent<PlayerOneMovement>().GetSpeed());
        yield return new WaitForSeconds(speedUpDuration);
        Debug.Log(other.GetComponent<PlayerOneMovement>().GetSpeed());
        other.GetComponent<PlayerOneMovement>().SetSpedUp(false);
        other.GetComponent<PlayerOneMovement>().SetSpeed(other.GetComponent<PlayerOneMovement>().GetConstantSpeed());
        
        Debug.Log(other.GetComponent<PlayerOneMovement>().GetSpeed());
        Debug.Log("Speed boost ended");
        other.GetComponent<PlayerOneStats>().pickupCount = 0;
    }
}
