using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    private TrapBase trapBase;

    private SkinnedMeshRenderer[] meshRenderers;
    private float key;
    [SerializeField] private float animationSpeed;

    // custom to this trap
    [SerializeField] private int knockBackValue = 75;
    [SerializeField] private int knockUpValue = 25;
    [SerializeField] private float stunDuration = 1f;

    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;
    // keep track of how many frames of knockback have passed
    private int knockTimer = 0;
    //Player's animator for knockback animation
    private Animator anim = null;

    // Use this for initialization
    void Start () {
        trapBase = GetComponent<TrapBase>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        key = meshRenderers[0].GetBlendShapeWeight(0);

        InvokeRepeating("Scale", 0f, 0.001f);
	}
	
	// Update is called once per frame
    // knockback has a knockback velocity, knockup velocity, and a knockTimer to 
    // force the knockback into an arc shape.
	void FixedUpdate () {
        if (player != null)
        {
            if (hit && knockTimer < 7 && knockTimer >= 5)
            {
                trapBase.KnockBack(player, knockBackValue, 0);               
                knockTimer++;
            }
            else if (hit && knockTimer < 7)
            {
                trapBase.KnockBack(player, 0, knockUpValue);
                trapBase.Stun(player.gameObject, stunDuration);
                knockTimer++;
            }
            else
            {
                hit = false;
                anim.SetBool("Knockback", hit);
                knockTimer = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hit = true;
            player = other.gameObject;
            trapBase.UpdatePlayerVelocities(other.gameObject);
            anim = player.GetComponent<PlayerOneMovement>().GetAnim();
            anim.Play("Knockback", 0);
            anim.SetBool("Knockback", hit);
        }
    }

    private void Scale()
    {
        if(key >= 100)
        {
            CancelInvoke("Scale");
        }

        key -= animationSpeed;
        foreach(SkinnedMeshRenderer mr in meshRenderers)
        {
            mr.SetBlendShapeWeight(0, key);
        }
        
    }
}
