using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

public class PlacePickups : MonoBehaviour {
    //Change to Use
    private static int NumFloors = 5;
    private static int NumPickUpsFloor = 2;

    private static int state = 1;
    private static int floor = 1;
    private static bool loop = true;
    private static int index = 0;

    private static List<GameObject> Faces = new List<GameObject>();

    [MenuItem("GameObject/Generate Pickups", false, 15)]
    // Use this for initialization
    void Create()
    {
        GameObject item = GameObject.Find("PickUp");
        List<GameObject> faces = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(faces);

    }

}
