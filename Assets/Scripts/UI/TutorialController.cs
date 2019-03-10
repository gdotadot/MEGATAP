using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

    // two serparate lists for player tips
    [SerializeField] private List<GameObject> p1tips;
    [SerializeField] private List<GameObject> p2tips;

    // Use this for initialization
    void Start () {
        // set all of them to invisible, for both lists
		foreach( GameObject tip in p1tips)
        {
            tip.GetComponent<CanvasRenderer>().SetAlpha(0f);
            tip.transform.GetChild(0).gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
        foreach (GameObject tip in p2tips)
        {
            tip.GetComponent<CanvasRenderer>().SetAlpha(0f);
            tip.transform.GetChild(0).gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        // TOP PLAYER
        // spawn a tip ever 10 seconds
        if((int)Time.timeSinceLevelLoad % 10 == 0 && (int)Time.timeSinceLevelLoad/10 < p1tips.Count)
        {
            GameObject tip = p1tips[(int)Time.timeSinceLevelLoad/10];
            tip.GetComponent<CanvasRenderer>().SetAlpha(1f);
            tip.transform.GetChild(0).gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        }
        // remove the previous tip every 10 seconds
        if ((int)Time.timeSinceLevelLoad % 10 == 0 && (int)Time.timeSinceLevelLoad / 10 > 0 && (int)Time.timeSinceLevelLoad / 10 < p1tips.Count + 2)
        {
            GameObject tip = p1tips[((int)Time.timeSinceLevelLoad / 10) -1];
            tip.GetComponent<CanvasRenderer>().SetAlpha(0f);
            tip.transform.GetChild(0).gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }

        // BOTTOM PLAYER
        // spawn a tip ever 10 seconds
        if ((int)Time.timeSinceLevelLoad % 10 == 0 && (int)Time.timeSinceLevelLoad / 10 < p2tips.Count)
        {
            GameObject tip = p2tips[(int)Time.timeSinceLevelLoad / 10];
            tip.GetComponent<CanvasRenderer>().SetAlpha(1f);
            tip.transform.GetChild(0).gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        }
        // remove the previous tip every 10 seconds
        if ((int)Time.timeSinceLevelLoad % 10 == 0 && (int)Time.timeSinceLevelLoad / 10 > 0 && (int)Time.timeSinceLevelLoad / 10 < p2tips.Count+2)
        {
            GameObject tip = p2tips[((int)Time.timeSinceLevelLoad / 10) - 1];
            tip.GetComponent<CanvasRenderer>().SetAlpha(0f);
            tip.transform.GetChild(0).gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
    }
 
}
