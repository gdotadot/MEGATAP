using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallandChain : MonoBehaviour {

    private SpellBase spellBase;

    [SerializeField] private float slowRun = 0.5f;
    [SerializeField] private float reduceJump = 0.5f;
    [SerializeField] private float spellDuration = 5f;

    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;

    // SFX
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip clip;

    private void Start()
    {
        spellBase = GetComponent<SpellBase>();
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    // knockback has a knockback velocity, knockup velocity, and a knockTimer to
    // force the knockback into an arc shape.
    void FixedUpdate()
    {
        if (player != null)
        {
            // if colliding, give an amount of stun
            if (hit)
            {
                spellBase.Slow(player, slowRun, reduceJump, spellDuration);
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
            audioSource.PlayOneShot(clip);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Boundary"))
        {
            Destroy(this);
        }
    }
}
