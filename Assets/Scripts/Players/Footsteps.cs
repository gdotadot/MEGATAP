using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void RightFootstep()
    {
        AudioClip clip = GetRightClip();
        audioSource.PlayOneShot(clip);
    }

    private void LeftFootstep()
    {
        AudioClip clip = GetLeftClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRightClip()
    {
        return clips[0];
    }

    private AudioClip GetLeftClip()
    {
        return clips[1];
    }
}
