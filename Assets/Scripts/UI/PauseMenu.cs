using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	[HideInInspector] public bool GameIsPaused = false;
	
	[SerializeField] GameObject pauseMenuUI;


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
        }
	}
	
	public void Resume(){
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	
	public void Pause(){
		pauseMenuUI.SetActive(true);
        pauseMenuUI.transform.SetAsLastSibling();
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
	public void LoadMenu(){
		SceneManager.LoadScene("Menu");
		Time.timeScale = 1f;
		Debug.Log("Load Menu");
	}
	public void ControlScreen(){
		SceneManager.LoadScene("Control");
		Time.timeScale = 1f;
		Debug.Log("Control Scene");
	}
    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
