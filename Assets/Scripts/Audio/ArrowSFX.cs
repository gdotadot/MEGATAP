using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSFX : MonoBehaviour {

    [SerializeField] private AudioClip stringDrawSFX;
    [SerializeField] private AudioClip stringReleaseSFX;

    private AudioSource audioSource;

	private void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	


    //Sound Effects

    private void StringDraw()
    {
        audioSource.PlayOneShot(stringDrawSFX);
    }

    private void StringRelease()
    {
        audioSource.PlayOneShot(stringReleaseSFX);
    }
}
