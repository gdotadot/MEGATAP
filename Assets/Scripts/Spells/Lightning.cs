using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{

    private SpellBase spellBase;
    private GameObject player = null;
    private bool hit = false;

    [SerializeField] private float stunDuration = 3;

    // Use this for initialization
    void Start()
    {
        spellBase = this.GetComponent<SpellBase>();

        switch (GameObject.Find("Player 1").GetComponent<CameraOneRotator>().GetState())
        {
            case 1:
                break;
            case 2:
                transform.eulerAngles = new Vector3(0, -90, 0);
                break;
            case 3:
                transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case 4:
                transform.eulerAngles = new Vector3(0, 90, 0);
                break;

        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // if colliding, give an amount of slow
            if (hit)
            {
                spellBase.Stun(player, stunDuration);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hit = true;
            player = other.gameObject;
        }
    }
}
