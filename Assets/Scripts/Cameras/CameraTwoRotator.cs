﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//<alexc> This class rotates and moves the Player 2 (right side camera) on a given input.
public class CameraTwoRotator : MonoBehaviour {

    [SerializeField] private Camera playerTwoCam;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Image gridUI;
    [SerializeField] private GameManager gm;
    //Change these static variables iff tower is scaled
    private static int camPosHorizontal = 150;
    private static int camPosVertical = 20;
    private static int camRotationX = 5;
    private static int numFloors = 7;
    

    private Vector3[] basePositions = new [] { new Vector3(0,                   camPosVertical, -camPosHorizontal),
                                               new Vector3(camPosHorizontal,    camPosVertical, 0),
                                               new Vector3(0,                   camPosVertical, camPosHorizontal),
                                               new Vector3(-camPosHorizontal,   camPosVertical, 0)};

    private Quaternion[] baseRotations = new[] { Quaternion.Euler(camRotationX,  0,   0),
                                                 Quaternion.Euler(camRotationX, -90,  0),
                                                 Quaternion.Euler(camRotationX, -180, 0),
                                                 Quaternion.Euler(camRotationX, -270, 0)};

    private IEnumerator camTween;

    private int currentPos, floor;

    private bool moveEnabled = true;
    private PauseMenu pause;
    private void Start()
    {
        pause = gm.GetComponent<PauseMenu>();
        Vector3 startPos = basePositions[0] + new Vector3(0, 20, 0);
        playerTwoCam.transform.position = startPos;
        playerTwoCam.transform.rotation = baseRotations[0];

        currentPos = 1;
        floor = 2;

        moveEnabled = true;
    }

    //Rotate camera around tower when arrow keys are pressed
    private void Update()
    { 
        if (moveEnabled)
        {
            if (Input.GetButton("Submit_Joy_2") && !pause.GameIsPaused)
            {
                moveEnabled = false;

                if (currentPos == basePositions.Length)
                {
                    if (floor < numFloors)
                    {
                        moveEnabled = false;
                        floor++;
                        StartMove(basePositions[0], baseRotations[0], 1);
                    }
                }
                else
                {
                    StartMove(basePositions[currentPos], baseRotations[currentPos], currentPos + 1);
                }
            }
        }
    }

    //Initialize camera movement variables and start movement coroutine
    private void StartMove(Vector3 goalPos, Quaternion goalRot, int goal)
    {
        currentPos = goal;

        if (camTween != null)
        {
            StopCoroutine(camTween);
        }
        camTween = TweenToPosition(goalPos, goalRot, moveSpeed);
        StartCoroutine(camTween);
        MoveGrid();
    }

    //Camera movement coroutine
    private IEnumerator TweenToPosition(Vector3 targetPos, Quaternion targetRot, float time)
    {
        Vector3 currentPos = playerTwoCam.transform.position;
        Quaternion currentRot = playerTwoCam.transform.rotation;

        targetPos.y *= floor;

        for (float t = 0; t < time; t += Time.deltaTime)
        {
            playerTwoCam.transform.position = Vector3.Lerp(currentPos, targetPos, t/time);
            playerTwoCam.transform.rotation = Quaternion.Slerp(currentRot, targetRot, t/time);
            yield return null;
        }

        playerTwoCam.transform.position = targetPos;
        playerTwoCam.transform.rotation = targetRot;

        moveEnabled = true;
        camTween = null;
    }

    //Rotate and move worldspace grid UI with camera
    private void MoveGrid()
    {
        gridUI.transform.Rotate(0, 90, 0);
        switch (currentPos)
        {
            case 1:
                //Move up 20 when it hits face 1 again
                gridUI.transform.position = new Vector3(0, gridUI.transform.position.y + 20, -40.1f);
                break;
            case 2:
                gridUI.transform.position = new Vector3(40.1f, gridUI.transform.position.y, 0);
                break;
            case 3:
                gridUI.transform.position = new Vector3(0, gridUI.transform.position.y, 40.1f);
                break;
            case 4:
                gridUI.transform.position = new Vector3(-40.1f, gridUI.transform.position.y, 0);
                break;
        }
    }

    public int GetState()
    {
        return currentPos;
    }
    
    public int GetFloor()
    {
        return floor;
    }
}
