using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneLose : MonoBehaviour {
    private bool lose;
    private CameraOneRotator cam;
    [SerializeField] MoveVines vines;



    private void Start () {
        lose = false;
        cam = GetComponent<CameraOneRotator>();
	}

	private void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        //check collision with Rising walls that are tagged with "rise"
        if (other.tag == "Vine" && cam.GetFloor() == vines.GetVineFloor())
        {
            lose = true;
            GameOver();
        }
    }

    public bool GameOver()
    {
        return lose;
    }
}
