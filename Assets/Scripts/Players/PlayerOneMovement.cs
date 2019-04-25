using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneMovement : MonoBehaviour {
    //movement vars serialized for designers
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [Tooltip("How long the wall jump force lasts.")] [SerializeField] private float wallJumpTime;
    [Tooltip("WallJumpForce = jumpHeight / this")] [SerializeField] private float wallJumpDivider;
    [Tooltip("How far between -transform.fwd & transform.up the angle is.")] [SerializeField] private float wallJumpDirectionDivider;

    //other movement vars
    private Vector3 movementVector;
    private Vector3 wallJumpVector;
    private bool crouching;
    private bool grounded;
    private bool jumping;
    private bool landing;
    private bool wallJumping;
    private bool cantStandUp;
    private bool slowed = false;
    private bool spedUp = false;
    public bool InputEnabled = true;

    //Control if player can have input
    private bool move = true;

    private float speed; //Change this when crouching, etc.; set it back to moveSpeed when done
    private float jumpH; // change this when in sap etc.; set it back to jumpHeight when done

    //camera
    [SerializeField] private CameraOneRotator cam;
    [SerializeField] private float distanceFromGround = 2f;
    private int camOneState = 1;

    [SerializeField] private GameObject gameManager;

    private float inputAxis; //used to get input axis from controller/keyboard
    private InputManager inputManager;

    private Rigidbody rb;



    private PauseMenu pause;
    private Animator animator;
    private CapsuleCollider col;
    private ParticleSystemRenderer stun;
    private SphereCollider sphere;

    private void Awake()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //checkControllers = gameManager.GetComponent<CheckControllers>();
        col = GetComponent<CapsuleCollider>();
        stun = GetComponentInChildren<ParticleSystemRenderer>();
        pause = gameManager.GetComponent<PauseMenu>();
        sphere = GetComponent<SphereCollider>();

        speed = moveSpeed;
        jumpH = jumpHeight;

        move = true;
    }

    private void Update()
    {
        camOneState = cam.GetState();
        grounded = GetComponentInChildren<PlayerGrounded>().IsGrounded();
        if (move == true && !pause.GameIsPaused && InputEnabled)
        {
            if (Mathf.Abs(inputManager.GetAxis(InputCommand.BottomPlayerMoveStick)) > 0.4)
            {
                inputAxis = inputManager.GetAxis(InputCommand.BottomPlayerMoveStick);
            }
            else if (Mathf.Abs(inputManager.GetAxis(InputCommand.BottomPlayerMoveKeyboard)) > 0)
            {
                inputAxis = inputManager.GetAxis(InputCommand.BottomPlayerMoveKeyboard);
            }
            else
            {
                inputAxis = 0;
            }
            //jumping
            if (inputManager.GetButtonDown(InputCommand.BottomPlayerJump) && grounded && crouching == false)
            {
                jumping = true;
            }

            if(inputManager.GetButtonDown(InputCommand.BottomPlayerJump) && grounded && crouching == true && cantStandUp == false)
            {
                jumping = true;
            }

            //crouch
            if (inputManager.GetButtonDown(InputCommand.BottomPlayerCrouch) && grounded)
            {
                crouching = true;
            }
            if (inputManager.GetButtonUp(InputCommand.BottomPlayerCrouch) || (!inputManager.GetButton(InputCommand.BottomPlayerCrouch) && cantStandUp == false))
            {
                if (spedUp == false)
                {
                    if (cantStandUp == true)
                    {
                        crouching = true;
                        if (slowed == false)
                        {
                            speed = moveSpeed / 2;
                        }
                    }
                    if (cantStandUp == false)
                    {
                        crouching = false;
                        if (slowed == false)
                        {
                            speed = moveSpeed;
                        }
                    }
                }
            }
            // Animation parameters update
            animator.SetBool("Jumping", jumping);
            if (jumping)
            {
                animator.SetBool("Running", false);
            }
            //animator.SetBool("Running", move);
            animator.SetBool("Stunned", false);
            stun.enabled = false;
        }

        switch (camOneState)
        {
            case 1:
                movementVector = new Vector3(inputAxis * speed, rb.velocity.y, 0);
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

        if (crouching == true && spedUp == false)
        {
            if (slowed == false)
            {
                speed = moveSpeed / 2;
            }
            col.height = 2.25f;
            col.center = new Vector3(0, 1.1f, 0);
            sphere.center = new Vector3(0, 1f, 0); 
        }

        if(crouching == false || grounded == false) {
            col.height = 4.5f;
            col.center = new Vector3(0, 2.2f, 0);
            sphere.center = new Vector3(0, 3f, 0);
        }

        cantStandUp = gameObject.GetComponentInChildren<Colliding>().GetCollision();

        if(!pause.GameIsPaused) Move();
    }


    private void Move()
    {
        //Stuff that used to be in fixedupdate
        if (jumping)
        {
            crouching = false;
            movementVector = new Vector3(movementVector.x, jumpH, movementVector.z);
            if (move == true && wallJumping == false)
            {
                animator.Play("Armature|JumpStart", 0);
            }
            jumping = false;
            landing = false;
            if (slowed == false && spedUp == false)
            {
                speed = moveSpeed;
            }
        }
        else if (crouching)
        {
            //TODO
        }
        if (move == false)
        {
            movementVector = new Vector3(0, Physics.gravity.y * 0.255f, 0);
            stun.enabled = true;
        }

        if (!wallJumping) rb.velocity = movementVector;
        else rb.velocity = wallJumpVector;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, LayerMask.GetMask("Platform")) && grounded == false)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * distanceFromGround, Color.yellow);
            if (hit.distance <= distanceFromGround && jumping == false)
            {
                landing = true;
            }
            if (hit.distance > distanceFromGround)
            {
                landing = false;
            }
        }

        if(grounded == true)
        {
            landing = true;
        }

        animator.SetBool("Landing", landing);
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Crouched", crouching);
        animator.SetFloat("YVelocity", rb.velocity.y);
    }


    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Trap")
        {
            //CHECK WALL JUMP
            RaycastHit hit;
            RaycastHit downHit;
            bool raycastDown = Physics.Raycast(transform.position, -transform.up, out downHit, 1);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f) && !raycastDown)
            {
                if (hit.transform.tag == "Platform" && Input.GetButtonDown("Jump_Joy_1") && grounded == false && move == true)
                {
                    animator.Play("Wall Jump", 0);
                    wallJumpVector = (-transform.forward + transform.up / wallJumpDirectionDivider).normalized * (jumpH / wallJumpDivider);
                    wallJumping = true;
                    //jumping = false;
                    StartCoroutine(DisableWallJump());
                }
            }
            //NOT WALL JUMPING
        }
    }

    private IEnumerator DisableWallJump()
    {
        yield return new WaitForSeconds(wallJumpTime);
        wallJumping = false;
        wallJumpVector = Vector3.zero;
    }

    //Called from pause script to re-enable input after pressing "Resume"
    public IEnumerator ResumeInput()
    {
        yield return new WaitForSeconds(0.5f);
        InputEnabled = true;
    }

    public IEnumerator SpeedBoost(Collider other, float speedUpMultiplier, float speedUpDuration)
    {
        other.GetComponent<PlayerOneMovement>().SetSpedUp(true);
        other.GetComponent<PlayerOneMovement>().SetSpeed(other.GetComponent<PlayerOneMovement>().GetSpeed() * speedUpMultiplier);
        yield return new WaitForSeconds(speedUpDuration);
        other.GetComponent<PlayerOneMovement>().SetSpedUp(false);
        other.GetComponent<PlayerOneMovement>().SetSpeed(other.GetComponent<PlayerOneMovement>().GetConstantSpeed());

        spedUp = false;
        other.GetComponent<PlayerOneStats>().pickupCount = 0;
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

    public void SetSpedUp(bool s)
    {
        spedUp = s;
    }

    public bool GetSpedUp()
    {
        return spedUp;
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

    public Animator GetAnim()
    {
        return animator;
    }

    public float GetInputAxis()
    {
        return inputAxis;
    }

    public void IsSlowed(bool slow)
    {
        slowed = slow;
    }

    public bool IsCrouched()
    {
        return crouching;
    }

    public bool IsStunned()
    {
        return !move;
    }
}
