using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoveControllerCursor : MonoBehaviour {
    [Header("Programmers - GameObjects/Scripts -----")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Image controllerCursor;
    private CheckControllers checkControllers;
    private PauseMenu pause;

    [Header("Controller Values -----")]
    [SerializeField] private float cursorDelayHorizontal; //How long the script waits before moving cursor to next grid pos while stick is held down.
    [SerializeField] private float cursorDelayVertical;
    [SerializeField] private float cursorGrid;  //Size of the cursor grid - DIFFERENT from the size of the worldspace/trap grid. Should be fine at 23 but might need to be tweaked if we change UI.
    [Tooltip("Higher # = Lower Sensitivity")] [SerializeField] private float stickSensitivity; //Between 0-1 ; how far the player needs to push the stick for it to move the cursor
                                                                                               //Needed to set it higher because some controller sticks naturally move left/right a little.

    private bool p2Controller;
    private bool cursorHorizontalMove = true;
    private bool cursorVerticalMove = true;

    private float screenWidth, screenHeight;

    void Start () {
        pause = gameManager.GetComponent<PauseMenu>();

        checkControllers = gameManager.GetComponent<CheckControllers>();

        p2Controller = checkControllers.GetControllerTwoState();


        //Screen size
        CanvasScaler scaler = controllerCursor.GetComponentInParent<CanvasScaler>();
        float expectedAspectRatio = scaler.referenceResolution.x / scaler.referenceResolution.y;
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        screenWidth = (aspectRatio / expectedAspectRatio) * (scaler.referenceResolution.x / 2);
        screenHeight = scaler.referenceResolution.y / 2;

        //Set Cursor visible if p2Controller is true
        controllerCursor.enabled = p2Controller;
    }
	
    //Move cursor with grid
	void Update () {
        p2Controller = checkControllers.GetControllerTwoState();
        if (p2Controller && !pause.GameIsPaused)
        {
            Vector3 cursorPos = controllerCursor.GetComponent<RectTransform>().localPosition;
            if (Input.GetAxisRaw("Horizontal_Joy_2") > stickSensitivity && cursorHorizontalMove && cursorPos.x < screenWidth)
            {
                controllerCursor.GetComponent<RectTransform>().localPosition += new Vector3(cursorGrid, 0, 0);
                cursorHorizontalMove = false;
                StartCoroutine(EnableHorizontalCursorMove());
            }
            else if (Input.GetAxisRaw("Horizontal_Joy_2") < -stickSensitivity && cursorHorizontalMove && cursorPos.x > -screenWidth)
            {
                controllerCursor.GetComponent<RectTransform>().localPosition -= new Vector3(cursorGrid, 0, 0);
                cursorHorizontalMove = false;
                StartCoroutine(EnableHorizontalCursorMove());
            }
            else if (Input.GetAxisRaw("Vertical_Joy_2") > stickSensitivity && cursorVerticalMove && cursorPos.y < screenHeight)
            {
                controllerCursor.GetComponent<RectTransform>().localPosition += new Vector3(0, cursorGrid, 0);
                cursorVerticalMove = false;
                StartCoroutine(EnableVerticalCursorMove());
            }
            else if (Input.GetAxisRaw("Vertical_Joy_2") < -stickSensitivity && cursorVerticalMove && cursorPos.y > -screenHeight)
            {
                controllerCursor.GetComponent<RectTransform>().localPosition -= new Vector3(0, cursorGrid, 0);
                cursorVerticalMove = false;
                StartCoroutine(EnableVerticalCursorMove());
            }
        }
    }

    IEnumerator EnableHorizontalCursorMove()
    {
        yield return new WaitForSeconds(cursorDelayHorizontal);
        cursorHorizontalMove = true;
    }

    IEnumerator EnableVerticalCursorMove()
    {
        yield return new WaitForSeconds(cursorDelayVertical);
        cursorVerticalMove = true;
    }
}
