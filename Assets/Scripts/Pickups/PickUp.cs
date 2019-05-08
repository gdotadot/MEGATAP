using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour {

    [SerializeField] public float speedUpDuration = 7f;
    [SerializeField] public float speedUpMultiplier = 2f;
    [SerializeField] private AudioClip pickUpSFX1;
    [SerializeField] private AudioClip pickUpSFX2;
    [SerializeField] private AudioClip pickUpSFX3;

    private Image[] pickupImages;
    [SerializeField] private Sprite pickupFull;

    private bool active = true; // make sure only picks up once

    private AudioSource audioSource;
    // active correlates to whether or not the pickup is faded out or not

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
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
        GameObject pickupParent = GameObject.Find("PickupsUI");
        pickupImages = pickupParent.GetComponentsInChildren<Image>();
        if (other.tag == "Player" && other.GetComponent<PlayerOneStats>().pickupCount < 3 && active == true)
        {
            active = false;
            other.GetComponent<PlayerOneStats>().pickupCount++;
            

            if (other.gameObject.GetComponent<PlayerOneStats>().pickupCount == 1)
            {
                audioSource.PlayOneShot(pickUpSFX1);
                pickupImages[0].sprite = pickupFull;
                pickupImages[0].rectTransform.sizeDelta = new Vector2(75, 60);
            } else if (other.gameObject.GetComponent<PlayerOneStats>().pickupCount == 2)
            {
                audioSource.PlayOneShot(pickUpSFX2);
                pickupImages[1].sprite = pickupFull;
                pickupImages[1].rectTransform.sizeDelta = new Vector2(75, 60);
            } else if (other.gameObject.GetComponent<PlayerOneStats>().pickupCount == 3)
            {
                audioSource.PlayOneShot(pickUpSFX3);
                pickupImages[2].sprite = pickupFull;
                pickupImages[2].rectTransform.sizeDelta = new Vector2(75, 60);
            }

            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
    }


}
