using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    //Game Over Status
    [SerializeField] private PlayerOneLose lost;
    private bool lose = false;

    private void Awake()
    {
        Scene currScene = SceneManager.GetActiveScene();

        if (currScene.name.Equals("Tower1"))
        {
            SceneManager.LoadScene("Tower1_Platforms", LoadSceneMode.Additive);
        }
        
    }

    private void Update ()
    {
        //Game Over from timer
        lose = lost.GameOver();
        if(lose == true)
        {
            SceneManager.LoadScene("GameOver");
        }

	}
}
