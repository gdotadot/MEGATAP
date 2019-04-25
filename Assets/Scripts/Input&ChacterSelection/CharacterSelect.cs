using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {
    private InputManager inputManager;
    [SerializeField] private Image playerOneSelector;
    [SerializeField] private Image playerTwoSelector;
    [SerializeField] private GameObject startText;

    [SerializeField] private float stickSensitivity;
    [SerializeField] private float stickDelay;

    private bool stickMove = true;

    //States can be -1 (left), 0 (middle), and 1 (right)
    private int selectorOneState = 0;
    private int selectorTwoState = 0;

    float quarterDist;

    private void Awake()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

	private void Update () {
        Vector2 playerOnePos = playerOneSelector.transform.position;
        Vector2 playerTwoPos = playerTwoSelector.transform.position;
        quarterDist = Screen.width / 4;

        //Controller 1 movement
        if (Input.GetAxis("Horizontal_Joy_1_Stick") > stickSensitivity && selectorOneState < 1 && stickMove)
        {
            playerOneSelector.transform.position = new Vector2(playerOnePos.x + quarterDist, playerOnePos.y);
            selectorOneState++;
            stickMove = false;
            StartCoroutine(StickDelay());
        }
        if (Input.GetAxis("Horizontal_Joy_1_Stick") < -stickSensitivity && selectorOneState > -1 && stickMove)
        {
            playerOneSelector.transform.position = new Vector2(playerOnePos.x - quarterDist, playerOnePos.y);
            selectorOneState--;
            stickMove = false;
            StartCoroutine(StickDelay());
        }

        //Controller 2 movement
        if (Input.GetAxis("Horizontal_Joy_2_Stick") > stickSensitivity && selectorTwoState < 1 && stickMove)
        {
            playerTwoSelector.transform.position = new Vector2(playerTwoPos.x + quarterDist, playerTwoPos.y);
            selectorTwoState++;
            stickMove = false;
            StartCoroutine(StickDelay());
        }
        if (Input.GetAxis("Horizontal_Joy_2_Stick") < -stickSensitivity && selectorTwoState > -1 && stickMove)
        {
            playerTwoSelector.transform.position = new Vector2(playerTwoPos.x - quarterDist, playerTwoPos.y);
            selectorTwoState--;
            stickMove = false;
            StartCoroutine(StickDelay());
        }

        //Check characters selected are opposite
        if(selectorOneState == -selectorTwoState && selectorOneState != 0)
        {
            startText.SetActive(true);
            if(inputManager.GetButton(InputCommand.Start))
            {
                if(selectorOneState == -1)
                {
                    inputManager.P1IsTop = false;
                }
                else
                {
                    inputManager.P1IsTop = true;
                }

                SceneManager.LoadScene("Tutorial");
            }
        }
        else
        {
            startText.SetActive(false);
        }
    }

    private IEnumerator StickDelay()
    {
        yield return new WaitForSeconds(stickDelay);
        stickMove = true;
    }
}
