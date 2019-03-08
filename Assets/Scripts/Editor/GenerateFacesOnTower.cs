using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GenerateFacesOnTower : MonoBehaviour {

    //Change to Use
    //private static int NumFloors = 7;

    private static int state = 1;
    private static int floor = 1;
    private static bool loop = true;
    private static int index = 0;

    private static List<GameObject> Faces = new List<GameObject>(); 

    [MenuItem("GameObject/Generate Face on Tower", false, 13)]
    private static void Create()
    {
        Debug.Log("Generating Faces on Tower");
        GetAtPath(Faces);
        while(loop == true)
         {
             switch (state)
             {
                 case 1:
                     Instantiate(Faces[index], new Vector3(-39f, (20 * floor) + 1, -42.15f), Quaternion.Euler(0, 0, 0));
                     state++;
                     index++;
                     break;
                 case 2:
                     Instantiate(Faces[index], new Vector3(42.15f, (20 * floor) + 1, -39), Quaternion.Euler(0, -90, 0));
                     state++;
                     index++;
                     break;
                 case 3:
                     Instantiate(Faces[index], new Vector3(39f, (20 * floor) + 1, 42.15f), Quaternion.Euler(0, 180, 0));
                     state++;
                     index++;
                     break;
                 case 4:
                     Instantiate(Faces[index], new Vector3(-42.15f, (20 * floor) + 1, 39f), Quaternion.Euler(0, -270, 0));
                     state = 1;
                     floor++;
                     index++;
                     break;
             }
             if(index >= Faces.Count)
             {
                 loop = false;
             }
         }
        index = 0;
        floor = 1;
        state = 1;
        loop = true;
    }


    public static List<GameObject> GetAtPath(List<GameObject> list)
    {

        ArrayList al = new ArrayList();
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + "Prefabs/Faces");

        foreach (string fileName in fileEntries)
        {
            int assetPathIndex = fileName.IndexOf("Assets");
            string localPath = fileName.Substring(assetPathIndex);

            Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject));

            if (t != null)
                al.Add(t);
        }
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < al.Count; i++)
        {
            result.Add((GameObject)al[i]);
        }
        
        for(int i = 0; i < result.Count; i++)
        {
            //int random = Random.Range(0, result.Count);
            list.Add(result[i]);
        }
        return list;
    }
}
