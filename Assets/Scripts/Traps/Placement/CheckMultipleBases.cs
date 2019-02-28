using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMultipleBases : MonoBehaviour {

    private CheckValidLocations[] checkLocations;
	public bool Valid { get; private set; }
    public bool Placed;

	void Start () {
        checkLocations = GetComponentsInChildren<CheckValidLocations>();	
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Platform" && !Placed)
        {
            bool valid = true;
            foreach (CheckValidLocations check in checkLocations)
            {
                if (!check.Valid)
                {
                    valid = false;
                }
            }

            Valid = valid;
        }
    }
}
