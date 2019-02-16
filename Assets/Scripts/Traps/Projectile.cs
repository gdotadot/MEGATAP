using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float stunDuration;

    private TrapBase trapBase;


	private bool hit = false;
	private GameObject player = null;
    private Renderer[] child;

	// Use this for initialization
	void Start () {
		trapBase = GetComponent<TrapBase>();
		Destroy(gameObject, 5.0f);
        child = GetComponentsInChildren<Renderer>();
    }

	void FixedUpdate(){
		if (player != null)
		{
			if (hit)
			{
				trapBase.Stun(player, stunDuration, this.gameObject);
                hit = false;
			}
			else
			{
				hit = false;
			}
		}
	}

	// Update is called once per frame
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player"){
			player = col.gameObject;
			hit = true;
            Unrender();

        }
		else if(col.gameObject.tag == "Boundary" || col.gameObject.tag == "Platform"){
            Unrender();
        }
	}

    private void Unrender()
    {
        foreach(Renderer r in child)
        {
            r.enabled = false;
        }
    }
}
