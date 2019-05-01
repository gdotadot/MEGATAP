using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRoller : MonoBehaviour
{
    [SerializeField] private float timeToShoot = 4;

    private TrapBase trapBase;
    private CameraTwoRotator cam;
    private GameObject logPrefab;
    private float timer = 0.0f;
    private GameObject logProjectile;
    private Rigidbody rb;

    [SerializeField] private float speed = 60;
    private Vector3 velocity;
    private Quaternion projectileRotation;
    // Use this for initialization
    void Start()
    {
        trapBase = GetComponent<TrapBase>();
        cam = GameObject.Find("Player 2 Camera").GetComponent<CameraTwoRotator>();
        logPrefab = Resources.Load("LogProjectile") as GameObject;

        switch (cam.GetState())
        {
            case 1:
                transform.eulerAngles = new Vector3(0, 0, 0);
                projectileRotation = Quaternion.identity;
                velocity = new Vector3(-speed, 0, 0);
                break;
            case 2:
                transform.eulerAngles = new Vector3(0, 270, 0);
                projectileRotation = Quaternion.Euler(0, -90, 0);
                velocity = new Vector3(0, 0, -speed);
                break;
            case 3:
                transform.eulerAngles = new Vector3(0, 180, 0);
                projectileRotation = Quaternion.Euler(0, -180, 0);
                velocity = new Vector3(speed, 0, 0);
                break;
            case 4:
                transform.eulerAngles = new Vector3(0, 90, 0);
                projectileRotation = Quaternion.Euler(0, -270, 0);
                velocity = new Vector3(0, 0, speed);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (trapBase.enabled == true)
        {
            if (timer > timeToShoot)
            {
                logProjectile = Instantiate(logPrefab);

                logProjectile.transform.position = transform.position + new Vector3(0, 0.5f, 0);
                logProjectile.transform.rotation = projectileRotation;

                rb = logProjectile.GetComponentInChildren<Rigidbody>();
                rb.velocity = velocity;
                timer = timer - timeToShoot;
            }
        }
    }
}
