using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour {
    private TrapBase trapBase;
    [SerializeField] private float stunDuration;
    [SerializeField] private float stunAnimSpeed = 1f;

    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;
    // Player's animator for animation
    private Animator anim = null;

    // SFX
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip clip;

    private void Start()
    {
        trapBase = GetComponent<TrapBase>();
        audioSource = GetComponent<AudioSource>();
    }
    // Stun has player object, stun time in seconds, trap itself
    // player has normal y velocity but is stopped in all other velocities and cannot move controls
    void FixedUpdate()
    {
        if (player != null)
        {
            if (hit)
            {
                trapBase.Stun(player, stunDuration, this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            hit = true;
            player = other.gameObject;
            anim = player.GetComponent<PlayerOneMovement>().GetAnim();
            anim.SetFloat("StunAnimSpeed", stunAnimSpeed);
            anim.Play("FaceplantStart", 0);
            audioSource.PlayOneShot(clip);
        }
    }
}
