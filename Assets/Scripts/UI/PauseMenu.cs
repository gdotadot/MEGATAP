using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	private bool GameIsPaused = false;
	
	[SerializeField] GameObject pauseMenuUI;


	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Debug.Log("Resume");
                Resume();
            }
            else
            {
                Debug.Log("Pause");
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
        Debug.Log("Pausing???");
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
	public void LoadMenu(){
		SceneManager.LoadScene("Menu");
		Time.timeScale = 1f;
		Debug.Log("Load Menu");
	}
    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
