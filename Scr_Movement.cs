using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Movement : MonoBehaviour
{
    private CharacterController cc;
    private Animator animator;
    private Transform tr;

    private Vector3 input = Vector3.zero;
    private Vector3 rootMotion;
    private Vector3 velocity;

    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float jumpDamp;
    public float pushPower;

    private int isWalkingHash;
    private int isRunningHash;
    private int isPushingHash;
    private int isJumpingHash;

    private bool isJumping;
    private bool isRunning = false;
    private bool isPushing = false;
    private bool isWalking = false;

    /*
     * Section Added By Patrick Vasile 
     * 03/19/21
     * */

    public Transform camTarget;
    public Transform swivel;
    public Transform leftTarget;
    public Transform rightTarget;

    public float deadzoneVal = 0;

    /*
     * Section END
     * 
     * */

    /*
     * Le stringToHash et l'ancien systeme de Input provient de ce video
     * https://www.youtube.com/watch?v=IurqiqduMVQ&t=489s&ab_channel=NickyB
     * 
     * */
    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isPushingHash = Animator.StringToHash("isPushing");
        isJumpingHash = Animator.StringToHash("isJumping");

        /*
     * Section Added By Patrick Vasile 
     * 03/22/21
     * */

        camTarget = GameObject.Find("Front").GetComponent<Transform>();
        leftTarget = GameObject.Find("Left").GetComponent<Transform>();
        rightTarget = GameObject.Find("Right").GetComponent<Transform>();
        swivel = GameObject.Find("Back").GetComponent<Transform>();
        /*
    * Section END
    * 
    * */
    }

    // Update is called once per frame
    void Update()
    {
        //camTarget.position = camTarget.position;
        HandleMovement();
        HandleRotation();
    }

    /* 
     * Le code en rapport au Character Controller a été pigé en partie de ce video :
    * https://www.youtube.com/watch?v=4y4QXEPnkgY&t=314s&ab_channel=TheKiwiCoder
    * Je n'ai pas trop commenté puisque je comprends bien le tout 
    * */
    private void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        if (isJumping)    //is inAir state
        {

            velocity.y -= gravity * Time.deltaTime;
            Vector3 displacement = velocity * Time.deltaTime;
            displacement += CalculateAirControl(); //Permet de ne pas tjrs augmenter la vitesse du air control
            cc.Move(displacement);
            isJumping = !cc.isGrounded;
            rootMotion = Vector3.zero;
            animator.SetBool(isJumpingHash, isJumping);
        }
        else        //isGrounded state
        {
            cc.Move(rootMotion + Vector3.down * stepDown); //Pour avancer et descendre 
            rootMotion = Vector3.zero;

            if (!cc.isGrounded)
            {
                isJumping = true;
                velocity = animator.velocity * jumpDamp;
                velocity.y = 0;
            }
        }
    }

    void HandleRotation()
    {

        /*
        * Section Edited By Patrick Vasile 
        * 03/22/21
        * */


        float hAxis = Input.GetAxis("Horizontal"); //ADD PV
        float vAxis = Input.GetAxis("Vertical"); //ADD PV

        var camera = Camera.main; //ADD PV

        var forward = camera.transform.forward; //ADD PV
        var right = camera.transform.right; //ADD PV

        forward.y = 0f; //ADD PV
        right.y = 0f;//ADD PV
        forward.Normalize();//ADD PV
        right.Normalize();//ADD PV

        var desiredMoveDirection = forward * vAxis + right * hAxis;//ADD PV

        Vector3 currentPosition = tr.position;

        Vector3 newPosition = new Vector3(input.x, 0, input.z);
        //Vector3 newDirection = new Vector3(0,0, camTarget.rotation.z);
        //Vector3 positionToLookAt = currentPosition + newPosition;
        Vector3 positionToLookAt = currentPosition + desiredMoveDirection;

        tr.LookAt(positionToLookAt);

        /*
        * Section Edited END
        * 
        * */
    }

    void HandleMovement()
    {

        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        isWalking = animator.GetBool(isWalkingHash);
        animator.SetBool(isWalkingHash, input.magnitude != 0);

        isPushing = Input.GetButton("Jump");
        animator.SetBool(isPushingHash, isPushing);

        isRunning = Input.GetButton("RightBumper"); //edited by Patrick to include Right Bumper
        animator.SetBool(isRunningHash, isRunning);

        if (Input.GetButton("Fire1"))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            velocity = animator.velocity * jumpDamp;
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            animator.SetBool(isJumpingHash, isJumping);
        }
    }

    Vector3 CalculateAirControl()
    {
        return ((tr.forward * input.y) + (tr.right * input.x)) * (airControl / 100);
    }

    /*
        * API Unity
        * 
        * */
    void OnControllerColliderHit(ControllerColliderHit hit) //Permet de pousser les objets en attachant un rigidBody
    {
        if (isPushing)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            if (body == null || body.isKinematic) //S'il n'y a pas de RigidBody
            {
                return;
            }

            if (hit.moveDirection.y < -0.3) //Pour ne pas pousser les objets vers le bas
            {
                return;
            }

            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z); //Calcule la direction du push

            body.velocity = pushDir * pushPower; //Force du push
        }
    }
}