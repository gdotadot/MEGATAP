using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneStats : MonoBehaviour {

    [SerializeField] public int pickupCount { get; set; }

	// Use this for initialization
	void Start () {
        pickupCount = 0;
        Debug.Log(pickupCount);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
