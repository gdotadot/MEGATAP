using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour {

    [SerializeField] private float spawnTime = 30f;
    [SerializeField] private int numFloors = 7;

    private int floor = 1;
    private int face = 1;

    [SerializeField] private Animator anim;

    private Animator childAnim;

    private bool spawned = false;

    private int[,] once;

    private GameObject child;

    // Use this for initialization
    void Start() {
        StartCoroutine(WaitToSpawn(spawnTime));

        anim = GetComponent<Animator>();

        once = new int[4 + 1, numFloors + 1];
        for(int i = 0; i < 5; i++)
        {
            for (int j = 0; j < numFloors + 1; j++)
            {
                once[i, j] = 0;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (spawned == true && floor <= numFloors) {
            switch (face)
            {
                case 1:
                    if (once[face, floor] == 0)
                    {
                        child = Instantiate(this.gameObject, new Vector3(-38f, 3f * floor, -42f), Quaternion.Euler(0, 90, 0));
                        childAnim = child.GetComponent<Vine>().GetAnim();
                        childAnim.Play("Vine Attack", 0);
                        once[face, floor]++;
                    }
                    break;
                case 2:
                    if (once[face, floor] == 0)
                    {
                        child = Instantiate(this.gameObject, new Vector3(42f, 3f, -38f), Quaternion.Euler(0, 0, 0));
                        childAnim = child.GetComponent<Vine>().GetAnim();
                        childAnim.SetBool("Growing", true);
                        once[face, floor]++;
                    }
                    break;
                case 3:
                    if (once[face, floor] == 0)
                    {
                        child = Instantiate(this.gameObject, new Vector3(38f, 3f, 42f), Quaternion.Euler(0, -90, 0));
                        childAnim = child.GetComponent<Vine>().GetAnim();
                        childAnim.SetBool("Growing", true);
                        once[face, floor]++;
                    }
                    break;
                case 4:
                    if (once[face, floor] == 0)
                    {
                        child = Instantiate(this.gameObject, new Vector3(-42f, 3f, 38f), Quaternion.Euler(0, 270, 0));
                        childAnim = child.GetComponent<Vine>().GetAnim();
                        childAnim.SetBool("Growing", true);
                        once[face, floor]++;
                    }
                    break;

            }
        }
        Debug.Log("face: " + face);
        Debug.Log("floor: " + floor);
        Debug.Log("array: [" + face +", " + floor + "] = " + once[face,floor]);
	}

    private IEnumerator WaitToSpawn(float time)
    {
        yield return new WaitForSeconds(time);
        spawned = true;
    }

    private void Growth(){
        face++;
        if(face > 4)
        {
            face = 1;
            floor++;
        }
    }

    //Gettters

    private Animator GetAnim()
    {
        return anim;
    }
}
