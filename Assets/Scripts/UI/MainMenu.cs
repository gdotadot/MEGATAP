using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {
	[SerializeField] private GameObject popup;
    [SerializeField] private EventSystem es;

    [SerializeField] private GameObject[] menuButtons;

    [SerializeField] private GameObject[] yesNoButtons;

    private CheckControllers checkControllers;
	
    void Start () {
		popup.gameObject.SetActive(false);
        checkControllers = GetComponent<CheckControllers>();
        if(checkControllers.GetControllerOneState())
        {
            es.SetSelectedGameObject(menuButtons[0].gameObject);
        }
	}

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Tower1");
    }
    
    public void QuitGame()
    {
		popup.gameObject.SetActive(true);
        if(checkControllers.GetControllerOneState())
        {
            es.SetSelectedGameObject(yesNoButtons[0]);
            for(int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].SetActive(false);
            }
        }
    }
    
    public void YesButton()
    {
    	Debug.Log("Quit");
    	Application.Quit();
    }
    
    public void NoButton()
    {
    	popup.gameObject.SetActive(false);
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].SetActive(true);
        }
        es.SetSelectedGameObject(menuButtons[0]);
    }	
}
