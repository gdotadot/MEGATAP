using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject avoidVinesText;

    //Game Over Status
    [SerializeField] private PlayerOneLose lost;
    private bool lose = false;

    private void Awake()
    {
        SceneManager.LoadScene("Tower1_Platforms", LoadSceneMode.Additive);
    }

    private void Update ()
    {
        //Game Over from timer
        lose = lost.GameOver();
        if(lose == true)
        {
            SceneManager.LoadScene("GameOver");
        }


        //Text to tell bottom player to avoid vines
        if(Time.time > 35f && Time.time <= 38f)
        {
            avoidVinesText.SetActive(true);
        }

	}
}
