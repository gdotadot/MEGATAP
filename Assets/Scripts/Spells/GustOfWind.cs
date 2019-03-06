using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GustOfWind : MonoBehaviour {

    private SpellBase spellBase;
    private bool trig = false;
    private CameraOneRotator cam;
    private CameraTwoRotator cam2;
    [SerializeField] private int windForce = 10;
    private Vector3 direction;
    private void Start()
    {
        
       	cam = GameObject.Find("Player 1").GetComponent<CameraOneRotator>();
      
        switch (cam.GetState())
        {
            case 1:
                transform.eulerAngles = new Vector3(0, 0, 0);
                direction = new Vector3(-1.0f, 0.0f, 0.0f);
                break;
            case 2:
                transform.eulerAngles = new Vector3(0, 270, 0);
                direction = new Vector3(0.0f, 0.0f, -1.0f);
                break;
            case 3:
                transform.eulerAngles = new Vector3(0, 180, 0);
                direction = new Vector3(1.0f, 0.0f, 0.0f);
                break;
            case 4:
                transform.eulerAngles = new Vector3(0, 90, 0);
                direction = new Vector3(0.0f, 0.0f, 1.0f);
                break;
        }
         
    }

    void FixedUpdate()
    {
        // if (player != null)
        // {
        //     if (hit)
        //     {
        //        spellBase.Stun(player, stunDuration);
        //     }
        // }

        if(trig){
                //player.transform.position = playerPos;
          }

          // temp.z = -42.08f;
          // player.transform.position = temp;
          // float previousX = 0;
          // if (player.transform.position.z == 0){
          //   previousX = player.transform.position.x;
          // }
          // if (player.transform.position.z != 0)
          // {
          //   Vector3 newPosition = player.transform.position;
          //   newPosition.x = previousX;
          //   newPosition.z = 0;
          //   player.transform.position = newPosition;
          // }

    }

    void OnTriggerEnter(Collider other)
    {
        // if (other.tag == "Player")
        // {
        //     hit = true;
        //     player = other.gameObject;
        //     this.GetComponent<Renderer>().enabled = false;
        // }
       if(other.gameObject.tag == "Player"){
          //Debug.Log("entered");
          //player = other.gameObject;
          trig = true;
      }
    }

	void OnTriggerStay(Collider other)
	{

     	// Here you add negative forces to object that is within the fan area
     	// Other is the object, that should be pushed away
      if(other.gameObject.tag == "Player")
      {
        //Debug.Log("Object is in trigger");
     	  Vector3 place = transform.position;
        //Debug.Log("place" + place);
     	  Vector3 targetPosition = other.transform.position;
        //Debug.Log("target position" + targetPosition);
     	 // Vector3 direction = targetPosition - place;
      //  Debug.Log("direction" + direction);
     	 // direction.Normalize();
      //  Debug.Log("normal direction" + direction);



     	  other.transform.position += direction * windForce * Time.deltaTime;
      }

	}
	void OnTriggerExit(Collider other)
	{
    if(other.gameObject.tag == "Player"){
      //Debug.Log("Object left the trigger");
      trig = false;
   }

	}


}
