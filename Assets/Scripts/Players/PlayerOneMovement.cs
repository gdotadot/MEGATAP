using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneMovement : MonoBehaviour {
    //movement vars serialized for designers
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [Tooltip("How long the wall jump force lasts.")][SerializeField] private float wallJumpTime;
    [Tooltip("WallJumpForce = jumpHeight / this")][SerializeField] private int wallJumpDivider;
    [Tooltip("How far between -transform.fwd & transform.up the angle is.")][SerializeField] private int wallJumpDirectionDivider;

    //other movement vars
    private Vector3 movementVector;
    private Vector3 wallJumpVector;
    private bool crouching;
    private bool grounded;
    private bool jumping;
    private bool landing;
    private bool wallJumping;

    //Control if player can have input
    private bool move;

    private float speed; //Change this when crouching, etc.; set it back to moveSpeed when done
    private float jumpH; // change this when in sap etc.; set it back to jumpHeight when done

    //camera
    [SerializeField] private CameraOneRotator cam;
    [SerializeField] private float distanceFromGround = 2f;
    private int camOneState = 1;

    [SerializeField] private GameObject gameManager;

    private float inputAxis; //used to get input axis from controller/keyboard

	private Rigidbody rb;


    private CheckControllers checkControllers;
    private Animator animator;

    void Start() {
		rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        checkControllers = gameManager.GetComponent<CheckControllers>();

        speed = moveSpeed;
        jumpH = jumpHeight;

        move = true;
    }

    private void Update()
    {
        camOneState = cam.GetState();
        if (move == true)
        {
            inputAxis = checkControllers.GetInputAxis();

            //jumping
            if (Input.GetButtonDown("Jump_Joy_1") && grounded)
            {
                jumping = true;
            }

            //crouch
            if (Input.GetButton("Crouch_Joy_1") && grounded)
            {
                crouching = true;
            }
            if (Input.GetButtonUp("Crouch_Joy_1") && grounded)
            {
                crouching = false;
            }
            // Animation parameters update
            animator.SetBool("Jumping", jumping);
            if(jumping)
            {
                animator.SetBool("Running", false);
            }
            //animator.SetBool("Running", move);
        }

        switch (camOneState)
        {
            case 1:
                movementVector = new Vector3(inputAxis * speed, rb.velocity.y, 0);
                //Debug.Log(movementVector);
                if (inputAxis > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    animator.SetFloat("Velocity", speed);
                    if (grounded) animator.SetBool("Running", true);
                }
                else if (inputAxis < 0)
                {
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    animator.SetFloat("Velocity", -speed);

                    if (grounded) animator.SetBool("Running", true);
                }
                else
                {
                    animator.SetFloat("Velocity", 0);

                    if (grounded) animator.SetBool("Running", false);
                }
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                break;
            case 2:
                movementVector = new Vector3(0, rb.velocity.y, inputAxis * speed);
                if (inputAxis > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    animator.SetFloat("Velocity", speed);
                    if (grounded) animator.SetBool("Running", true);
                }
                else if (inputAxis < 0)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    animator.SetFloat("Velocity", -speed);
                    if (grounded) animator.SetBool("Running", true);
                }
                else
                {
                    animator.SetFloat("Velocity", 0);
                    if (grounded) animator.SetBool("Running", false);
                }
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                break;
            case 3:
                movementVector = new Vector3(-inputAxis * speed, rb.velocity.y, 0);
                if (inputAxis > 0)
                {
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    animator.SetFloat("Velocity", -speed);
                    if (grounded) animator.SetBool("Running", true);
                }
                else if (inputAxis < 0)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    animator.SetFloat("Velocity", speed);
                    if (grounded) animator.SetBool("Running", true);
                }
                else
                {
                    animator.SetFloat("Velocity", -rb.velocity.x);
                    if (grounded) animator.SetBool("Running", false);
                }
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                break;
            case 4:
                movementVector = new Vector3(0, rb.velocity.y, -inputAxis * speed);
                if (inputAxis > 0)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    animator.SetFloat("Velocity", -speed);
                    if (grounded) animator.SetBool("Running", true);
                }
                else if (inputAxis < 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    animator.SetFloat("Velocity", speed);
                    if (grounded) animator.SetBool("Running", true);
                }
                else
                {
                    animator.SetFloat("Velocity", 0);
                    if (grounded) animator.SetBool("Running", false);
                }
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                break;
        }
        
    }

    private void FixedUpdate()
    {
        if(jumping)
        {
            movementVector = new Vector3(movementVector.x, jumpH, movementVector.z);
            animator.Play("Armature|JumpStart", 0);
            jumping = false;
            landing = false;
        }
        else if(crouching)
        {
            //TODO
        }
        if (move == false)
        {
            movementVector = new Vector3(0, movementVector.y, 0);
        }

        if (!wallJumping) rb.velocity = movementVector;
        else rb.velocity = wallJumpVector;
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, distanceFromGround, LayerMask.GetMask("Platform")) && grounded == false)
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
            landing = true;
        }
        
        animator.SetBool("Landing", landing);
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("YVelocity", rb.velocity.y);
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            //CHECK WALL JUMP
            RaycastHit hit;
            RaycastHit downHit;
            bool raycastDown = Physics.Raycast(transform.position, -transform.up, out downHit, 1);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1) && !raycastDown)
            {
                if (hit.transform.tag == "Platform" && Input.GetButtonDown("Jump_Joy_1"))
                {
                    wallJumpVector = (-transform.forward + transform.up / wallJumpDirectionDivider).normalized * (jumpH / wallJumpDivider);
                    wallJumping = true;
                    jumping = false;
                    StartCoroutine(DisableWallJump());
                }
            }
            //NOT WALL JUMPING
            else
            {
                grounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            grounded = false;
        }
    }

    private IEnumerator DisableWallJump()
    {
        yield return new WaitForSeconds(wallJumpTime);
        wallJumping = false;
        wallJumpVector = Vector3.zero;
    }

    /////////////////////////////////////////////
    // GETTERS AND SETTERS                     //
    /////////////////////////////////////////////
    public int GetState()
    {
        return camOneState;
    }

    public float GetJumpHeight()
    {
        return jumpH;
    }

    public void SetJumpHeight(float j)
    {
        jumpH = j;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public float GetConstantSpeed()
    {
        return moveSpeed;
    }

    public float GetConstantJumpHeight()
    {
        return jumpHeight;
    }

    public void SetMove(bool m)
    {
        move = m;
    }
}
