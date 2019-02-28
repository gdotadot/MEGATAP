using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float stunDuration;

    private TrapBase trapBase;
    //private CameraTwoRotator cam;

    private bool hit = false;
	private GameObject player = null;
    private Renderer[] child;

	// Use this for initialization
	void Start () {
		trapBase = GetComponent<TrapBase>();
		Destroy(gameObject, 5.0f);
        child = this.GetComponentsInChildren<Renderer>();
        hit = false;

        //cam = GameObject.Find("Player 2 Camera").GetComponent<CameraTwoRotator>();

        //switch (cam.GetState())
        //{
        //    case 1:
        //        transform.eulerAngles = new Vector3(0, 0, 0);
        //        break;
        //    case 2:
        //        transform.eulerAngles = new Vector3(0, 270, 0);
        //        break;
        //    case 3:
        //        transform.eulerAngles = new Vector3(0, 180, 0);
        //        break;
        //    case 4:
        //        transform.eulerAngles = new Vector3(0, 90, 0);
        //        break;
        //}
    }

    void update()
    {
        
    }

	void FixedUpdate(){
		if (player != null)
		{
			if (hit)
			{
				trapBase.Stun(player, stunDuration, this.gameObject);
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
            if (hit == true)
            {
                Unrender();
                StartCoroutine(Death(stunDuration));
            }
            else if(hit == false)
            {
                Destroy(this.gameObject);
            }
        }
	}

    private void Unrender()
    {
        foreach(Renderer r in child)
        {
            r.enabled = false;
        }
    }

    private IEnumerator Death(float stunDuration)
    {
        yield return new WaitForSeconds(stunDuration + 2f);

        Destroy(this.gameObject);
    }
}
