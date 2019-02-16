using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour {

    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void JumpHyuh()
    {
        AudioClip clip = GetJumpingClip();
        audioSource.PlayOneShot(clip);
    }

    private void JumpLanding()
    {
        AudioClip clip = GetLandingClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetJumpingClip()
    {
        return clips[0];
    }

    private AudioClip GetLandingClip()
    {
        return clips[1];
    }
}
