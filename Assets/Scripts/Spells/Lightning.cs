using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{

    private SpellBase spellBase;

    [SerializeField] private float stunDuration;

    // Use this for initialization
    void Start()
    {
        spellBase = this.GetComponent<SpellBase>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("zzap");
            Debug.Log(other.GetComponent<GameObject>());
            spellBase.Stun(other.GetComponent<GameObject>(), stunDuration);
        }
    }
}
