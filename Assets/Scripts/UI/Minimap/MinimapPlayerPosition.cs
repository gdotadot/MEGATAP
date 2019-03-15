using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//<Alex>
[RequireComponent(typeof(Image))]
public class MinimapPlayerPosition : MonoBehaviour {
    [SerializeField] private NumberOfFloors numberOfFloors;
    [SerializeField] private Image topPlayerImage;
    [SerializeField] private Image bottomPlayerImage;
    [SerializeField] private TextMeshProUGUI topText;
    [SerializeField] private TextMeshProUGUI bottomText;


    [SerializeField] private CameraTwoRotator camTwo;
    [SerializeField] private CameraOneRotator camOne;

    private float highestMinimapPoint;

    
    void Start () {
        highestMinimapPoint = GetComponent<Image>().rectTransform.sizeDelta.y;
    }
	
	// Update is called once per frame
	void Update () {
        GetTopPlayerPosition();
        GetBottomPlayerPosition();
	}

    private void GetTopPlayerPosition()
    {
        int floor = camTwo.GetFloor();
        float position = 1 - (((float)numberOfFloors.NumFloors - (float)floor) / (float)numberOfFloors.NumFloors);

        topPlayerImage.rectTransform.anchoredPosition = new Vector3(topPlayerImage.rectTransform.anchoredPosition.x,
                                                                 highestMinimapPoint * position - (highestMinimapPoint / numberOfFloors.NumFloors),
                                                                 0);

        topText.text = "TOP: " + floor;
    }

    private void GetBottomPlayerPosition()
    {
        int floor = camOne.GetFloor();
        float position = 1 - (((float)numberOfFloors.NumFloors - (float)floor) / (float)numberOfFloors.NumFloors);
        bottomPlayerImage.rectTransform.anchoredPosition = new Vector3(bottomPlayerImage.rectTransform.anchoredPosition.x,
                                                                 highestMinimapPoint * position - (highestMinimapPoint / numberOfFloors.NumFloors),
                                                                 0);

        bottomText.text = "BOT: " + floor;
    }
}
