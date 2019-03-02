using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeccySounds : MonoBehaviour {

    [SerializeField]
    private AudioClip[] BodySFX;
    [SerializeField]
    private AudioClip[] JumpVoiceSFX;
    [SerializeField]
    private AudioClip[] OofVoiceSFX;

    int jumps = 0;
    int oofs = 0;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        jumps = 0;
        oofs = 0;
    }

    // Body SFX
    // Footsteps
    private void  RightFootstep()
    {
        AudioClip clip = GetRightFootClip();
        audioSource.PlayOneShot(clip);
    }

    private void LeftFootstep()
    {
        AudioClip clip = GetLeftFootClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRightFootClip()
    {
        return BodySFX[0];
    }

    private AudioClip GetLeftFootClip()
    {
        return BodySFX[1];
    }

    // Landing
    private void JumpLanding()
    {
        AudioClip clip = GetLandingClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetLandingClip()
    {
        return BodySFX[2];
    }

    // Voice SFX
    // Jump

    private void JumpHyuh()
    {
        AudioClip clip = GetJumpingClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetJumpingClip()
    {
        int hyuh = jumps % 3;
        jumps++;
        return JumpVoiceSFX[hyuh];
    }

    // Oof-Slip

    private void OofSlip()
    {
        AudioClip clip = GetSlip();
        audioSource.PlayOneShot(clip);
    }

    private void GetUpSigh()
    {
        AudioClip clip = OofVoiceSFX[4];
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetSlip()
    {
        int oof = oofs % 3;
        oofs++;
        return OofVoiceSFX[oof];
    }

    // Oof-Knockback
    private void OofKB()
    {
        AudioClip clip = GetKB();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetKB()
    {
        return OofVoiceSFX[3];
    }

}
