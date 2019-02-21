using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrify : MonoBehaviour {

    private SpellBase spellBase;
    [SerializeField] private int stunDuration;
    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;


    private void Start()
    {
        spellBase = GetComponent<SpellBase>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
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
            this.GetComponent<Renderer>().enabled = false;
        }
    }


}
