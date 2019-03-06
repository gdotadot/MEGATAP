using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrify : MonoBehaviour {

    private SpellBase spellBase;
    [SerializeField] private float stunDuration;
    //Change boy's material when hit and turn back
    [SerializeField] private Material normalBody;
    [SerializeField] private Material normalHat;
    [SerializeField] private Material normalHatEyes;
    [SerializeField] private Material normalPoncho;
    [SerializeField] private Material turnStone;

    private Renderer[] child;

    // let the FixedUpdate method know that there was a collision
    private bool hit = false;
    // the player (or whatever collided with this trap)
    private GameObject player = null;



    private void Start()
    {
        spellBase = GetComponent<SpellBase>();
        WaitToDie(this.gameObject);

    }

    void FixedUpdate()
    {
        if (player != null)
        {
            if (hit)
            {
                child = player.GetComponentsInChildren<Renderer>();
                TurnIntoStone();
                spellBase.Stun(player, stunDuration);
                StartCoroutine(Wait());
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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Boundary"))
        {
                Destroy(this);
        }
    }

    private void TurnIntoStone()
    {
        foreach (Renderer r in child)
        {
            r.material = turnStone;
        }
    }

    private void Revert()
    {
        child[1].material = normalBody;
        child[2].material = normalHat;
        child[3].material = normalHatEyes;
        child[4].material = normalPoncho;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(stunDuration - 0.2f);
        Revert();
    }

    private IEnumerator WaitToDie(GameObject obj)
    {
        yield return new WaitForSeconds(stunDuration * 2);
        Destroy(obj);
    }
}
