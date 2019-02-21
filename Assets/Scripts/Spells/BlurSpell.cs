using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurSpell : MonoBehaviour {
    [SerializeField] private float timeToBlur;

    private Camera bottomCam;

    private PostProcessVolume postProcess;
    private DepthOfField blur = null;

    private SpellBase sb;

    private void Start()
    {
        sb = GetComponent<SpellBase>();
    }
    private void Update()
    {
        if(sb.SpellCast)
        {
            bottomCam = GameObject.Find("Player 1 Camera").GetComponent<Camera>();
            postProcess = bottomCam.GetComponent<PostProcessVolume>();
            postProcess.profile.TryGetSettings(out blur);

            blur.active = true;

            StartCoroutine(StopBlur());
        }
    }
    
    IEnumerator StopBlur()
    {
        yield return new WaitForSeconds(timeToBlur);
        blur.active = false;
        Destroy(this.gameObject);
    }
}
