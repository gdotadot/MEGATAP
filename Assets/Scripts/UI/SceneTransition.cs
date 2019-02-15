using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public Animator transitionAnim;
	public string sceneName;
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump_Joy_1")||Input.GetButtonDown("Place_Joy_2")){
			StartCoroutine(LoadScene());
		}
	}
	
	IEnumerator LoadScene(){
		transitionAnim.SetTrigger("end");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(sceneName);
	}
}
