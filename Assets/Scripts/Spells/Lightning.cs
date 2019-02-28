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
    }

    // Update is called once per frame
    void Update()
    {

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
