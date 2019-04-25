using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NarrowPOVSpell : MonoBehaviour {
    [SerializeField] private float spellTime;

    private Camera bottomCam;

    private PostProcessVolume postProcess;
    private Vignette vignette = null;

    private SpellBase sb;

	private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    private void Start()
    {
    	audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
        sb = GetComponent<SpellBase>();
    }
    private void Update()
    {
        if (sb.SpellCast)
        {
            bottomCam = GameObject.Find("Player 1 Camera").GetComponent<Camera>();
            postProcess = bottomCam.GetComponent<PostProcessVolume>();
            postProcess.profile.TryGetSettings(out vignette);

            vignette.active = true;

            StartCoroutine(StopBlur());
        }
    }

    IEnumerator StopBlur()
    {
        yield return new WaitForSeconds(spellTime);
        vignette.active = false;
        Destroy(this.gameObject);
    }
}
