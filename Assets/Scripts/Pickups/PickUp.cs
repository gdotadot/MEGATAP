using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    [SerializeField] private float speedUpDuration = 7f;
    [SerializeField] private float speedUpMultiplier = 3f;
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
            Color currColor = this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color;
            currColor.a = 0.0f;
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = currColor;
        } else
        {
            // make it no longer transparent
            Color currColor = this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color;
            currColor.a = 1f;
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = currColor;
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && active == true)
        {
            active = false;
            other.GetComponent<PlayerOneStats>().pickupCount++;
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;

            // initiate speed up
            if (GameObject.FindWithTag("Player").GetComponent<PlayerOneStats>().pickupCount >= 3)
            {
                other.GetComponent<PlayerOneMovement>().SetSpedUp(true);
                StartCoroutine(other.GetComponent<PlayerOneMovement>().SpeedBoost(other, speedUpMultiplier, speedUpDuration));
            }
        }
    }


}
