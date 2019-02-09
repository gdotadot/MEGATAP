using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    [SerializeField] private GameObject playerOne;

	// Use this for initialization
	void Start () {
        this.transform.position = playerOne.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = playerOne.transform.position;
    }
}
