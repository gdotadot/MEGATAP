using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogProjectile : MonoBehaviour {
    private TrapBase trapBase;

    // custom to this trap
    [SerializeField] private int knockBackValue = 75;
    [SerializeField] private int knockUpValue = 25;
    [SerializeField] private float stunDuration = 0.75f;

    [SerializeField] private float animationSpeed;

    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;
    // keep track of how many frames of knockback have passed
    private int knockTimer = 0;
    //Player's animator for knockback animation
    private Animator anim = null;

    //child to get position
    private Transform child;

    // Use this for initialization
    void Start () {
        trapBase = GetComponent<TrapBase>();
        hit = false;
        
    }

    // Update is called once per frame
    private void Update()
    {
        child = GetComponentInChildren<Transform>();
        this.transform.position = child.position;
    }
    void FixedUpdate () {
        if (player != null)
        {
            if (hit)
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
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            hit = true;
            anim = player.GetComponent<PlayerOneMovement>().GetAnim();
            if (player.GetComponent<PlayerOneMovement>().IsCrouched() == false)
            {
                anim.Play("Knockback", 0);
            }
        }
        else if (col.gameObject.tag == "Boundary" || col.gameObject.tag == "Platform")
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(stunDuration);
        Destroy(this.gameObject);
    }
}
