using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

    [SerializeField] private List<GameObject> p1tips;
    [SerializeField] private List<GameObject> p2tips;

    // Use this for initialization
    void Start () {
		foreach( GameObject tip in p1tips)
        {
            Debug.Log(tip.GetComponent<CanvasRenderer>().GetAlpha());
            tip.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public static void FadeIn(GameObject g)
    {
        g.GetComponent<CanvasRenderer>().SetAlpha(0f);
 
}
