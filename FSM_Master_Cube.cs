using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Patrick Vasile
 * AI System (USED the FRAMEWORK (template) OF Annick Dumais FSM to mount my own State Machine)
 * 2021-03-05
 * */

public class FSM_Master_Cube : MonoBehaviour {
    #region Property
    //Property
    public List<HeadedTowards> points; //List of points that we use as our waypoints for the AI

    private float switchProb = 0.5f; //probability of change

    private float waitTimerMax = 2.0f; //max wait time
    private float waitTimer;

    [HideInInspector]
    public Man_GameManager rene; // rene the manager

    private NavMeshAgent aNavMesh; //navmesh system for the AI to stay within 

    private Transform avatarTarget; //whos the target
    public GameObject avatarGO;

    private Transform tr; //self transform
    private Renderer rd;
    private FSM_Etat currentState; 
    
    //[STATES]//
    private FSM_Cube_Idle idle;
    private FSM_Cube_Walk walk;
    private FSM_Cube_Jump jump;
    private FSM_Cube_Chase chase;
    private FSM_Cube_Attack attack;//Add Patrick Vasile 03/15/2021

    private Transform tMin;

    public Animator myAnimator;

    private int patrolIndex;

    private Transform innerLimits;//Add Patrick Vasile 03/15/2021
    private Transform outerLimits;

    public OuterDetectZone outerZone;
    public InnerDetectZone innerZone;//Add Patrick Vasile 03/15/2021

    [HideInInspector]
    public Collider asTarget; //TARGET hidden in inspector

    public Transform raycastPos;

    private bool wait = false;



    private bool moveForward;
    public bool travel; //bool to check if travelling or not

    //private bool waiting = false;

    //private bool stopMoving = false;

    #endregion

    // Start is called before the first frame update
    void Start() {
        aNavMesh = this.GetComponent<NavMeshAgent>(); //get the nav mesh component
        
        rene = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Man_GameManager>();

        //Ensure that we assign all the states in the state machine
        idle = new FSM_Cube_Idle(this);
        walk = new FSM_Cube_Walk(this);
        jump = new FSM_Cube_Jump(this);
        chase = new FSM_Cube_Chase(this);
        attack = new FSM_Cube_Attack(this);//Add Patrick Vasile 03/15/2021

        //Assign our detection zones//
        outerZone = GetComponentInChildren<OuterDetectZone>();
        innerZone = GetComponentInChildren<InnerDetectZone>();//Add Patrick Vasile 03/15/2021

        tr = this.transform;
        rd = this.GetComponent<Renderer>();

        //assign its primary state
        currentState = idle;


        outerLimits = tr.Find("OuterLimits");
        innerLimits = tr.Find("InnerLimits");

        avatarTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        
        
        //Debug.Log("My name is " + outerLimits.name);
        //Debug.Log("My name is " + innerLimits.name);

        //outerLimits = GetComponentInChildren<Collider>();

        //myAnimator = this.gameObject.GetComponentinParent(typeof(Animator))as Animator;

        // myAnimator = transform.parent.GetComponent<Animator>();
        myAnimator = this.GetComponent<Animator>();

        //To check if the nav Mesh is assigned. Acts as a debug system to ensure that the Navmesh has been applied
        if (aNavMesh == null) {

           // Debug.Log("Wheres the NAV");
        }
        else {

            if (points != null && points.Count >= 2) { // checks to ensue that there enough waypoints (atleast 2) in the list, or else it shall not run
                //ChangePoint();
                GoTo();
            }
            else {
               // Debug.Log("Not enough points available");
            }
        }

    }


    // Update is called once per frame
    void Update() {
        // calls on the fakeUpdate in the current state that the character will be in
        currentState.FakeUpdate();
        //Debug.Log(Wait);
        // Debug.Log(currentState);
        //FindClosestEnemy();
        /*
        if (!IsInvoking("CheckWait")) {
            Invoke("CheckWait", 3f);
        }
        */

        //aDistance = Vector3.Distance(tr.position, )
        if (avatarGO != null) {
            if (avatarTarget == null) {
                avatarTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }
        }
        if (avatarGO == null) {
            avatarGO = GameObject.Find("Ch03");
        }


    }

    #region Functions

    //public void Move(Vector3 deplacement) {
    /*
    public void Move() {
            //tr.Translate(deplacement * Time.deltaTime, Space.World);
            myAnimator.SetBool("isWalking", true);
    }
    */
    /*
        public void ChangeAnim() {



        }
    */


    /*------vvvvvvvvvvvvv--------
     * This fucntion helps find the closest waypoint near the enemy when he is done doing a chase of the player
     * -----vvvvvvvvvvvv---------
     * 
     * referenced from https://www.codegrepper.com/code-examples/csharp/unity+find+nearest+object for help with this problem
     * 
     * */


    public void FindClosestWaypoint() {

        float dCLosePoint = Mathf.Infinity; //create a float of a close number that starts at Max possible value
        HeadedTowards closest = null; //closest = null becaause there is no closest point
        HeadedTowards[] allPoints = GameObject.FindObjectsOfType<HeadedTowards>(); //get all the points with the script HeadedTowards and place them in an array

        //to find the nearest point
        foreach (HeadedTowards currentPoint in allPoints) { //foreach loop iterates through each item in the list
            float disToPoint = (currentPoint.transform.position - tr.position).sqrMagnitude;


            //when the disToPoint is smaller than the dCLosePoint value, the value of dClosePoint becomes the disToPoint
            if (disToPoint < dCLosePoint) {
                dCLosePoint = disToPoint;
                closest = currentPoint;
            }

            patrolIndex = points.IndexOf(closest); // finds the index of the closest obj and sets it to that so that the following points after that are in the correct order
            if (rene.DebugBool) {
                //Debug.Log(closest);
            }
        }
        
        //to set next position to the nearest target
        //Debug.Log(closest.transform);
        Vector3 target = closest.transform.position; //target then becomes the position of the closest point
        aNavMesh.SetDestination(target);
        //Debug.Log(target);
        travel = true; //sets travel to be true 
        
        //Debug.DrawLine(tr.position, closest.transform.position); //acts as a debug to draw a line to the nearest point
    }
   

    /*
     * This Switch allows for the states to change when the the proper String is called
     * 
     * */
    public void ChangeState(string etat) {
        switch (etat) {
            case "IDLE":
                rd.material.color = Color.white; //DEBUG
                currentState = idle;//changes the state to Idle
                //myAnimator.SetBool("isWalking", false);
               // Debug.Log("isWalking = false"); //DEBUG
                break;
            case "WALK":
                rd.material.color = Color.blue; //DEBUG
                currentState = walk;//changes the state to Walk
                //Debug.Log("isWalking = true"); //DEBUG
                // myAnimator.SetBool("isWalking", true);
                break;
            case "JUMP":
                rd.material.color = Color.red; //DEBUG
                currentState = jump;//changes the state to jump
                break;
            case "CHASE":
                rd.material.color = Color.green; //DEBUG
                currentState = chase;//changes the state to chase
                break;

            case "ATTACK":
                rd.material.color = Color.magenta; //DEBUG
                currentState = attack; //changes the state to attack
                break;
        }
    }
    /*
     public Vector3 CheckInputs() {
         Vector3 inputs = Vector3.zero;
         inputs.x = Input.GetAxisRaw("Horizontal");
         inputs.z = Input.GetAxisRaw("Vertical");
         return inputs;
     }
    */

    /*
     * Works like the chance system, but theres a random number that gets chosen, it is below or equal to the value than the AI will wait(go idle)
     * */
    public void CheckWait() {

        if (Random.Range(0, 100) <= 80) {
            Wait = !Wait;
        }
    }

    public void GetHit() {

        if (Vector3.Distance(tr.position, avatarTarget.transform.position) <= 1.0f) {
            avatarGO.GetComponent<Renderer>().material.color = Color.red;
            //Debug.Log("Hit");
            Destroy(rene.avatar);

        } else {
            avatarGO.GetComponent<Renderer>().material.color = Color.white;
        }
    }


    /*
     * GoTo() is used to assign a target for the AI
     * These Targets are predetermined points that are assigned through gameObjects that are in the inspector of the AI
     * These points that are stocked in the list are then chosen by the current PatrolIndex value
     * Referenced from https://www.youtube.com/watch?v=5q4JHuJAAcQ&ab_channel=TableFlipGames to explain how to use an AI with Navmesh and assign its targets
     * */
    public void GoTo() {

        //target = a position of one of the points stocked in the list. patrolindex is the position of the point in the list
        Vector3 target = points[patrolIndex].transform.position; 
        aNavMesh.SetDestination(target); //AI will then go to the target
        travel = true;

    }

    /*
   * ChangePoint is used to assist in changing the point of the current point in the patrol index
   * These points are positions of gameObjects that determine where the AI will walk
   * These points that are stocked in the list are then chosen by the current PatrolIndex value
   * Referenced from https://www.youtube.com/watch?v=5q4JHuJAAcQ&ab_channel=TableFlipGames to explain how to use an AI with Navmesh and assign its targets
   * */

    public void ChangePoint() {

        
        if (UnityEngine.Random.Range(0f, 1f) <= switchProb) { //Switch probability, between range, if less than or equal to value... 
            moveForward = !moveForward; //then no longer move forward down the list, forward becomes backward (and visa versa)
        }

        /* To ensure that the patrol index never exceeds its range of what it is in it.
         * We put % points.Count to ensure that if ever the points >= to the number of points in the list..
         * it will assign the next point in the patrol index as 0 (the start of the list)
         * */
            if (moveForward) {
            patrolIndex = (patrolIndex + 1) % points.Count;
            }
            else {
                if (patrolIndex > 0) {
                    patrolIndex = (patrolIndex - 1);
                }
                else {
                patrolIndex++;
                //FindClosestEnemy();
            }
            }
        
    }

    //When in chase the AI target will be the avatars position
    public void Chase() {
        if (rene.avatar != null) {
            Vector3 target = avatarTarget.transform.position;
            aNavMesh.SetDestination(target);
        }

    }

    public void CheckDistance() {

        if (travel && aNavMesh.remainingDistance < 0.1f) { //to ensure that the AI remaining distance to the target point is less than a value
            CheckWait(); 
            travel = false;
            /*
            if (stopMoving) {
                Wait = true;
                waitTimer = 0f;
            }
            */
            if (Wait) { //if the AI needs to wait..
                
                waitTimer = 0f; // a timer will be set to Zero
            }
            else { // if not then (refer to changePoint() and GoTo()
                ChangePoint(); //change point in the list, either the one before or one after it
                //FindClosestEnemy();
                GoTo();// and then go to that point
            }
        }

        //if the wait triggered
        if (Wait) {
            waitTimer += Time.deltaTime; //the waitTimer will increment on the Machines clock
            if (waitTimer >= waitTimerMax) { // if the time exceeds its limit

                Wait = false;
                travel = true;
                
                ChangePoint();//change point in the list, either the one before or one after it

                GoTo();// and then go to that point

            }
        

        }
    }

    #endregion

    //Accessors
    #region Accessors
    public bool Wait {
        get { return wait; }
        set { wait = value; }

    }


    public Transform OuterLimits {

        get { return outerLimits; }

    }

    public Transform InnerLimits {

        get { return innerLimits; }

    }


    public FSM_Etat CurrentState {
        get { return currentState; }
    }
    #endregion


}