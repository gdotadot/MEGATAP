using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    private GameObject projectile;
    private CapsuleCollider col;

    // Use this for initialization
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            projectile = other.gameObject;
            col = projectile.GetComponentInParent<CapsuleCollider>();
            col.enabled = true;
        }
    }
}
