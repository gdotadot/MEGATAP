using UnityEditor;
using UnityEngine;

//<alex>
public class GenerateFloors {
    //Change these to generate a different # of floors.
    private static int floorHeight = 20;
    public static int NumFloors = 7;


    [MenuItem("GameObject/Instantiate Floors", false, 11)]
    private static void Create()
    {
        GameObject previouslyInstantiated = GameObject.Find("ProceduralFloorsPrefab");
        if (previouslyInstantiated == null)
        {
            GameObject tower = GameObject.Find("Tower");
            tower.GetComponent<NumberOfFloors>().NumFloors = NumFloors;
            for (int floor = 0; floor < NumFloors; floor++)
            {
                GameObject newFloor = PrefabUtility.InstantiatePrefab(Resources.Load("ProceduralFloorsPrefab")) as GameObject;
                newFloor.transform.position = new Vector3(0, floorHeight * floor, 0);
                newFloor.transform.SetParent(tower.transform);

                if(floor == NumFloors - 1)
                {
                    GameObject winTrigger = PrefabUtility.InstantiatePrefab(Resources.Load("WinTrigger")) as GameObject;
                    winTrigger.transform.position = new Vector3(winTrigger.transform.position.x, floorHeight * floor + 10, winTrigger.transform.position.z);
                    winTrigger.transform.SetParent(tower.transform);
                }
            }
        }
        else
        {
            Debug.Log("Delete previously instantiated floors & previously instantiated win trigger from Tower parent game object first.");
        }
    }
}
