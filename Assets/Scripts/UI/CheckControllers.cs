using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckControllers : MonoBehaviour {
    private string[] joysticks;

    private bool controllerOne;
    private bool controllerTwo;

    private void Awake()
    {
        joysticks = Input.GetJoystickNames();
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
