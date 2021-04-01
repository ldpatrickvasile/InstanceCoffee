using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Patrick Vasile
 * AI System WALK
 * (USED the FRAMEWORK (template) OF Annick Dumais FSM to mount my own State Machine)
 * 2021-03-05
 * */

public class FSM_Cube_Walk : FSM_Etat //FSM_ETAT referenced at start of each state
{



    public FSM_Cube_Walk(FSM_Master_Cube myMaster) : base(myMaster) { }
    public override void FakeUpdate()
    {
        if (myMaster.avatarGO != null) {
            if (myMaster.rene.DebugBool) {
                Debug.DrawRay(myMaster.raycastPos.position, (myMaster.avatarGO.transform.position + Vector3.up - myMaster.raycastPos.position) * 10f, Color.red);
            }
        }
        //Debug.Log("Je suis en Walk");
        //Vector3 inputs = myMaster.CheckInputs();
        //myMaster.CheckWait();
        /*
                if (inputs.magnitude == 0)
                {
                    ToIdle();

                }
                else
                {
                    myMaster.Move(inputs);
                }
        */
        //myMaster.FindClosestEnemy();
        myMaster.CheckDistance(); ////check distance to point

        #region State Change to Chase
        if (myMaster.outerZone.asTarget != null) { //to enable the chase if the target is within the identified space
           // Debug.Log("avatar has entered");
           //Add Patrick Vasile 2021/03/23-24
            RaycastHit hit;
            if (Physics.Raycast(myMaster.raycastPos.position, myMaster.avatarGO.transform.position + Vector3.up - myMaster.raycastPos.position, out hit)) { //sends a raycast to check if the AI can actually see the player
                if (hit.transform.gameObject.tag == "Player") { // if the raycast hits the player..
                    //end add
                    ToChase();
                    //Debug.Log("im going to chase now");//DEBUG
                }
            }
        }
        
        #endregion

        /*myMaster.ChangePoint();
        myMaster.GoTo();
        */

        #region State Change to Wait
        if (myMaster.Wait == true) {
                ToIdle();
            }
        #endregion

        //myMaster.myAnimator.SetBool("isWalking", false);
        /* else {
             myMaster.Move();
         }
        */

        //IA:
        /* Deplace toi vers ton prochain WP
         * Si l'avatar est a une distance <10 -->To Chase
         */
    }


    /*
  * This section will change the state when one of these functions are called in the FakeUpdate()
  * 
  * */
    #region Fonctions for States
    public override void ToWalk()
    {

    }
    public override void ToIdle()
    {
        myMaster.ChangeState("IDLE");

        /*if (myMaster.Wait == true) {
            myMaster.myAnimator.SetBool("isWalking", false);
        }
        */
        myMaster.myAnimator.SetBool("isWalking", false);
    }
    public override void ToJump()
    {

    }

    public override void ToChase() {

        myMaster.ChangeState("CHASE");

    }

    public override void ToAttack() {

    }

    #endregion
}



