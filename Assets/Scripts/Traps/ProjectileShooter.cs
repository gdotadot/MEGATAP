using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour {
    [SerializeField] private float timeToShoot;
    [SerializeField] private int FaceNumberIfPrePlaced;

    private TrapBase trapBase;
    private CameraTwoRotator cam;
    private Animator animator;
	GameObject prefab;
	private float timer = 0.0f;
  	GameObject projectile;
    private Rigidbody rb;

    private Vector3 velocity;
    private Quaternion projectileRotation;
	//private bool ghost = true;
	// Use this for initialization
	void Start () {
		prefab = Resources.Load("projectile") as GameObject;
		trapBase = GetComponent<TrapBase>();
        cam = GameObject.Find("Player 2 Camera").GetComponent<CameraTwoRotator>();
        animator = GetComponentInChildren<Animator>();

        if (!GetComponentInChildren<CheckMultipleBases>().Placed)
        {
            switch (cam.GetState())
            {
                case 1:
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    projectileRotation = Quaternion.identity;
                    velocity = new Vector3(-20, 0, 0);
                    break;
                case 2:
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    projectileRotation = Quaternion.Euler(0, -90, 0);
                    velocity = new Vector3(0, 0, -20);
                    break;
                case 3:
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    projectileRotation = Quaternion.Euler(0, -180, 0);
                    velocity = new Vector3(20, 0, 0);
                    break;
                case 4:
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    projectileRotation = Quaternion.Euler(0, -270, 0);
                    velocity = new Vector3(0, 0, 20);
                    break;
            }
        }
        else
        {
            switch (FaceNumberIfPrePlaced)
            {
                case 1:
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    projectileRotation = Quaternion.identity;
                    velocity = new Vector3(-20, 0, 0);
                    break;
                case 2:
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    projectileRotation = Quaternion.Euler(0, -90, 0);
                    velocity = new Vector3(0, 0, -20);
                    break;
                case 3:
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    projectileRotation = Quaternion.Euler(0, -180, 0);
                    velocity = new Vector3(20, 0, 0);
                    break;
                case 4:
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    projectileRotation = Quaternion.Euler(0, -270, 0);
                    velocity = new Vector3(0, 0, 20);
                    break;
            }
        }
    }

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(trapBase.enabled == true){
			if(timer > timeToShoot){
                animator.SetTrigger("Fire");
				projectile = Instantiate(prefab);
                
				projectile.transform.position = transform.position + new Vector3 (0, 0.5f, 0);
                projectile.transform.rotation = projectileRotation;

                rb = projectile.GetComponent<Rigidbody>();
                rb.velocity = velocity;
				timer = timer - timeToShoot;
			}
		}
	}
}
