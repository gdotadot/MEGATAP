using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CheckControllers : MonoBehaviour {
    private string[] joysticks;

    private bool controllerOne;
    private bool controllerTwo;

    private Canvas canvas;
    private SetEventTriggerVars[] eventVars;
    private SetPointerClickEvents[] clickEvents;

    private Button[] trapButtons;
    private Button[] spellButtons;

    string scene;

    private void Awake()
    {
        joysticks = Input.GetJoystickNames();
        scene = SceneManager.GetActiveScene().name;
        if (scene == "Tower1")
        {
            GameObject canv = GameObject.Find("Canvas");
            canvas = canv.GetComponent<Canvas>();

            GameObject tower = GameObject.Find("Tower");
            eventVars = tower.GetComponentsInChildren<SetEventTriggerVars>();
            clickEvents = tower.GetComponentsInChildren<SetPointerClickEvents>();
        }

        CheckConnected();
        Debug.Log("Player 1 controller: " + controllerOne);
        Debug.Log("Player 2 controller: " + controllerTwo);


    }

    private void Update()
    {
        CheckConnected();
    }

    private void CheckConnected()
    {
        joysticks = Input.GetJoystickNames();
        if (joysticks.Length > 0)
        {
            for (int i = 0; i < joysticks.Length; i++)
            {
                if (!string.IsNullOrEmpty(joysticks[i]))
                {
                    //Debug.Log("Controller " + i + " is connected using: " + joysticks[i]);
                    if (i == 0)
                    {
                        controllerOne = true;
                    }
                    if (i == 1)
                    {
                        controllerTwo = true;
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;


                        if(canvas != null && eventVars != null)
                        {
                            canvas.GetComponent<GraphicRaycaster>().enabled = false;
                            foreach (SetEventTriggerVars v in eventVars)
                            {
                                v.enabled = false;
                            }
                            foreach (SetPointerClickEvents p in clickEvents)
                            {
                                p.enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    //Debug.Log("Controller " + i + " is disconnected.");
                    if (i == 0)
                    {
                        controllerOne = false;
                    }
                    if (i == 1)
                    {
                        controllerTwo = false;
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;

                        if(canvas != null && eventVars != null)
                        {
                            canvas.GetComponent<GraphicRaycaster>().enabled = true;
                            foreach (SetEventTriggerVars v in eventVars)
                            {
                                v.enabled = true;
                            }
                            foreach (SetPointerClickEvents p in clickEvents)
                            {
                                p.enabled = true;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            controllerOne = false;
            controllerTwo = false;
        }
    }

    public bool GetControllerTwoState()
    {
        return controllerTwo;
    }

    public bool GetControllerOneState()
    {
        return controllerOne;
    }

    //Get InputAxis based on whether player1 is using controller or keyboard
    public float GetInputAxis()
    {
        if (controllerOne)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal_Joy_1")) > 0.4f)
            {
                return Input.GetAxis("Horizontal_Joy_1");
            }
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal_Keyboard")) > 0)
            {
                return Input.GetAxisRaw("Horizontal_Keyboard");
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return Input.GetAxisRaw("Horizontal_Keyboard");
        }
    }
}
