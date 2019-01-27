﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sap : MonoBehaviour {

    // dictate where trap can be placed
    [SerializeField] private bool canPlaceAbove;
    [SerializeField] private bool canPlaceBelow;
    [SerializeField] private bool canPlaceLeft;
    [SerializeField] private bool canPlaceRight;
    [SerializeField] private TrapBase trapBase;

    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;
    // keep track of how many frames of knockback have passed
    private int slowTimer = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    // knockback has a knockback velocity, knockup velocity, and a knockTimer to 
    // force the knockback into an arc shape.
    void FixedUpdate()
    {
        if(player != null)
        {
            // if colliding, give an amount of slow
            if (hit)
            {
                slowTimer = 20;
                hit = false;
            }
            if (slowTimer > 0)
            {
                trapBase.Slow(player, 0.5f, 0.5f);
                slowTimer--;
            }
            else
            {
                player.GetComponent<PlayerOneMovement>().SetJumpHeight(player.GetComponent<PlayerOneMovement>().GetConstantJumpHeight());
                player.GetComponent<PlayerOneMovement>().SetSpeed(player.GetComponent<PlayerOneMovement>().GetConstantSpeed());
            }
        }
        

    }

    void OnTriggerStay(Collider other)
    {
        hit = true;
        player = other.gameObject;
    }
}
