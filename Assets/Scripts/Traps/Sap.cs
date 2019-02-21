﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sap : MonoBehaviour {

    private TrapBase trapBase;

    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // be sure to only slow the player once, not every frame
    private bool slowTriggered = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;
    // keep track of how many frames of knockback have passed
    private int slowTimer = 0;

    [Tooltip("Defines how much slower the player will go.")][SerializeField] private float slowSeverity = 0.1f;
    [Tooltip("Defines how much lower the jump will go." )][SerializeField] private float jumpReduceSeverity = 0.5f;
    [Tooltip("Defines how long the slow will last after being activated. (In number of frames)")] [SerializeField] private int slowDuration = 60;

    private void Start()
    {
        trapBase = GetComponent<TrapBase>();
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
                slowTimer = slowDuration;
                hit = false;
            }
            if (slowTimer > 0 && slowTriggered == false)
            {
                trapBase.Slow(player, slowSeverity, jumpReduceSeverity);
                slowTriggered = true;
            }
            else if(slowTriggered)
            {
                player.GetComponent<PlayerOneMovement>().SetJumpHeight(player.GetComponent<PlayerOneMovement>().GetConstantJumpHeight());
                player.GetComponent<PlayerOneMovement>().SetSpeed(player.GetComponent<PlayerOneMovement>().GetConstantSpeed());
                slowTriggered = false;
            }
        }
        
        // tick timer down if there is any
        if (slowTimer > 0)
        {
            slowTimer--;
        }

    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            hit = true;
            player = other.gameObject;
        }
    }
}
