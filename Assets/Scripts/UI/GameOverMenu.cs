using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour {
    [SerializeField] private EventSystem es;
    [SerializeField] private GameObject[] menuButtons;
    private CheckControllers cc;

    private void Start()
    {
        cc = GetComponent<CheckControllers>();
        if(cc.GetControllerOneState())
        {
            es.SetSelectedGameObject(menuButtons[0]);
        }
    }

    public void onClickRetry()
    {
        SceneManager.LoadScene("Tower1");
    }

    public void onClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
