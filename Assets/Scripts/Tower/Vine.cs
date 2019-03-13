using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour {

    [SerializeField] private float spawnTime = 30f;
    [SerializeField] private int numFloors = 7;

    private int floor = 0;
    private int face = 1;
    public int TotalFaceNumber = 1;

    [SerializeField] private GameObject[] vine;

    private GameObject child = null;

    private bool spawned = false;

    private int[,] once;

    private bool growing = false;


    // Use this for initialization
    void Start() {
        StartCoroutine(WaitToSpawn(spawnTime));

        once = new int[4 + 1, numFloors];
        for(int i = 0; i < 5; i++)
        {
            for (int j = 0; j < numFloors; j++)
            {
                once[i, j] = 0;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (spawned == true && floor < numFloors) {
            switch (face)
            {
                case 1:
                    if (once[face, floor] == 0)
                    {
                        child = Instantiate(vine[0], new Vector3(-38f, 3f + (20f * floor), -42f), Quaternion.Euler(0, 90, 0));
                        growing = false;
                        once[face, floor]++;
                    }
                    if(growing == true)
                    {
                        face++;
                        TotalFaceNumber++;
                    }
                    break;
                case 2:
                    if (once[face, floor] == 0)
                    {
                        child = Instantiate(vine[0], new Vector3(42f, 3f + (20f * floor), -38f), Quaternion.Euler(0, 0, 0));
                        growing = false;
                        once[face, floor]++;

                    }
                    if(growing == true)
                    {
                        face++;
                        TotalFaceNumber++;
                    }
                    break;
                case 3:
                    if (once[face, floor] == 0)
                    {
                        child = Instantiate(vine[0], new Vector3(38f, 3f + (20f * floor), 42f), Quaternion.Euler(0, -90, 0));
                        growing = false;
                        once[face, floor]++;
                    }
                    if (growing == true)
                    {
                        face++;
                        TotalFaceNumber++;
                    }
                    break;
                case 4:
                    if (once[face, floor] == 0)
                    {
                        child = Instantiate(vine[0], new Vector3(-42f, 3f + (20f * floor), 38f), Quaternion.Euler(0, -180, 0));
                        growing = false;
                        once[face, floor]++;
                    }
                    if (growing == true)
                    {
                        face = 1;
                        floor++;
                        TotalFaceNumber++;
                    }
                    break;

            }
        }
        if (child != null)
        {
            growing = child.GetComponent<VineAnim>().isDone();
        }
	}

    private IEnumerator WaitToSpawn(float time)
    {
        yield return new WaitForSeconds(time);
        spawned = true;
    }

}
