using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {
	[HideInInspector] public bool GameIsPaused = false;
	
	[SerializeField] GameObject pauseMenuUI;
    [SerializeField] Button[] pauseButtons;
    [SerializeField] GameObject controlsCanvas;
    [SerializeField] Button resumeButton;
    [SerializeField] GameObject playerTwo;
    [SerializeField] EventSystem es;

    private PlaceTrap pt;
    private CastSpell cs;
    private bool controlsUp;

    private void Start()
    {
        pt = playerTwo.GetComponent<PlaceTrap>();
        cs = playerTwo.GetComponent<CastSpell>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

            if(controlsUp)
            {
                controlsCanvas.SetActive(false);
                controlsUp = false;
                for (int i = 0; i < pauseButtons.Length; i++)
                {
                    pauseButtons[i].interactable = true;
                }
            }
        }
	}
	
	public void Resume(){
		pauseMenuUI.SetActive(false);
        bool buttonSet = false;
        for (int i = 0; i < cs.queue.Length; i++)
        {
            if (cs.queue[i] != null)
            {
                if (cs.active) cs.queue[i].GetComponent<Button>().interactable = true;
                if (cs.active && cs.queue[i].activeInHierarchy && !buttonSet)
                {
                    es.SetSelectedGameObject(cs.queue[i]);
                    buttonSet = true;
                }
            }
        }
        for (int i = 0; i < pt.queue.Count; i++)
        {
            if(pt.active) pt.queue[i].GetComponent<Button>().interactable = true;
            if (pt.active && pt.queue[i].activeInHierarchy && !buttonSet)
            {
                es.SetSelectedGameObject(pt.queue[i]);
                buttonSet = true;
            }
        }
        es.GetComponent<StandaloneInputModule>().submitButton = "Submit_Menu";
        Time.timeScale = 1f;
		GameIsPaused = false;
	}
	
	public void Pause(){
		pauseMenuUI.SetActive(true);
        pauseMenuUI.transform.SetAsLastSibling();

        for(int i = 0; i < cs.queue.Length; i++)
        {
            if (cs.queue[i] != null)
            {
                cs.queue[i].GetComponent<Button>().interactable = false;
            }
        }
        for (int i = 0; i < pt.queue.Count; i++)
        {
            pt.queue[i].GetComponent<Button>().interactable = false;
        }

        es.GetComponent<StandaloneInputModule>().submitButton = "Submit_Main_Menu";
        es.SetSelectedGameObject(resumeButton.gameObject);
        Time.timeScale = 0f;
		GameIsPaused = true;
	}
	public void LoadMenu(){
		SceneManager.LoadScene("Menu");
		Time.timeScale = 1f;
	}
	public void ControlScreen(){
        //SceneManager.LoadScene("Control");
        controlsUp = true;
        controlsCanvas.SetActive(true);

        for(int i = 0; i < pauseButtons.Length; i++)
        {
            pauseButtons[i].interactable = false;
        }
	}
    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
