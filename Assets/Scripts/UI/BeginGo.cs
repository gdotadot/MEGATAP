using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BeginGo : MonoBehaviour {
    [HideInInspector] public bool GameIsPaused = false;

    [SerializeField] private GameObject canvas;
    [SerializeField] private Camera camBot;
    [SerializeField] private Camera camTop;
    [SerializeField] private GameObject map;
    [SerializeField] GameObject playerTwo;
    [SerializeField] GameObject playerOne;
    [SerializeField] private int time;
    [SerializeField] private GameObject countdown;
    [SerializeField] private GameObject countdownCanvas;
    [SerializeField] EventSystem es;

    private PlaceTrap pt;
    private CastSpell cs;
    private PlayerOneMovement playerMov;

    // Use this for initialization
    void Start () {
        pt = playerTwo.GetComponent<PlaceTrap>();
        cs = playerTwo.GetComponent<CastSpell>();
        playerMov = playerOne.GetComponent<PlayerOneMovement>();

        canvas.SetActive(false);
        map.SetActive(false);
        
        camBot.enabled = false;
        //new Rect(Screen.width - xPos, Screen.height - yPos, width, height);
        camTop.transform.position = new Vector3(0, 100f, -200f);
        camTop.rect = new Rect(1, 1, 0, 0);

        es.GetComponent<StandaloneInputModule>().submitButton = "Submit_Menu";
        pt.InputEnabled = false;
        cs.InputEnabled = false;
        playerMov.InputEnabled = false;
        GameIsPaused = true;

        StartCoroutine(StartDelay());

    }

    private IEnumerator StartDelay()
    {

        yield return new WaitForSeconds(3);
        countdown.SetActive(false);
        countdownCanvas.SetActive(false);

        canvas.SetActive(true);
        map.SetActive(true);

        camBot.enabled = true;
        camTop.rect = new Rect(1, 0.505f, 0, 0.5f);
        camTop.transform.position = new Vector3(0, 30f, -80.2f);

        pt.InputEnabled = true;
        cs.InputEnabled = true;
        playerMov.InputEnabled = true;
        GameIsPaused = false;
    }
}
