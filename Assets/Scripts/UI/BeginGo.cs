using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BeginGo : MonoBehaviour {

    [SerializeField] private GameObject canvas;
    [SerializeField] private Camera camBot;
    [SerializeField] private Camera camTop;
    [SerializeField] private Camera ZoomCam;
    //[SerializeField] private GameObject map;
    [SerializeField] GameObject playerTwo;
    [SerializeField] GameObject playerOne;
    [SerializeField] private GameObject countdown;
    [SerializeField] private GameObject countdownCanvas;
    [SerializeField] EventSystem es;
    [SerializeField] float CountdownTime = 4;

    private PlaceTrap pt;
    private CastSpell cs;
    private PlayerOneMovement playerMov;
    private GameObject activeCountdown;
    [SerializeField] private PauseMenu pause;

    private Vector3 TargetPosition;
    private bool once = false;

    [SerializeField] private float moveInSpeed = 0.1f;

    // Use this for initialization
    void Start () {
        pt = playerTwo.GetComponent<PlaceTrap>();
        cs = playerTwo.GetComponent<CastSpell>();
        playerMov = playerOne.GetComponent<PlayerOneMovement>(); 

        TargetPosition = new Vector3(camTop.transform.position.x, 21, camTop.transform.position.z + 5);

        canvas.SetActive(false);

        countdown.SetActive(false);
        
        camBot.enabled = false;
        //new Rect(Screen.width - xPos, Screen.height - yPos, width, height);
        camTop.enabled = false;

        es.GetComponent<StandaloneInputModule>().submitButton = "Submit_Menu";
        pt.InputEnabled = false;
        cs.InputEnabled = false;
        playerMov.InputEnabled = false;
        pause.GameIsPaused = true;

    }

    private void Update()
    {
        ZoomCam.transform.position = Vector3.Lerp(ZoomCam.transform.position, TargetPosition , moveInSpeed);
        if (ZoomCam.transform.position.x <= camTop.transform.position.x && ZoomCam.transform.position.y <= 21 + 10 && ZoomCam.transform.position.z <= camTop.transform.position.z + 10 && once == false)
        {
            if (countdownCanvas.transform.childCount == 0)
            {
                activeCountdown = Instantiate(countdown, new Vector3(0, 30, -50), Quaternion.identity);
                activeCountdown.transform.parent = countdownCanvas.transform;
                activeCountdown.transform.localScale = new Vector3(50, 50, 1);
            }
            StartCoroutine(StartDelay());
        }
    }

    private IEnumerator StartDelay()
    {
        once = true;
        countdownCanvas.SetActive(true);
        activeCountdown.SetActive(true);

        yield return new WaitForSeconds(CountdownTime);
        Destroy(activeCountdown);
        countdownCanvas.SetActive(false);

        canvas.SetActive(true);

        camBot.enabled = true;
        camTop.enabled = true;
        ZoomCam.enabled = false;

        pt.InputEnabled = true;
        cs.InputEnabled = true;
        playerMov.InputEnabled = true;
        pause.GameIsPaused = false;
    }
}
