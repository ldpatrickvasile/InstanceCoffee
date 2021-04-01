using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Patrick Vasile
 * AI System CHASE
 * (USED the FRAMEWORK (template) OF Annick Dumais FSM to mount my own State Machine)
 * 2021-03-13
 * */

public class FSM_Cube_Chase : FSM_Etat //FSM_ETAT referenced at start of each state
{


    public FSM_Cube_Chase(FSM_Master_Cube myMaster) : base(myMaster) { }
    public override void FakeUpdate() {//fake update that will be called in the FSM_Master


        //myMaster.CheckDistance();

        //will constantly chase the player when in CHASE state
        myMaster.Chase();

        #region State Change to Walk
        // if the target is out of the zone, then the state will change
        if (myMaster.outerZone.asTarget == null) {

                myMaster.FindClosestWaypoint(); //when leaves the chase state, it will find the closest waypoint
                ToWalk(); //state will change to ToWalk()
                          //Debug.Log("im stopped"); //DEBUG
            //Debug.Log("walking to closest waypoint now");
           
        }
        #endregion

        #region State Change to Attack
        /* Begin Add
         * //Add Patrick Vasile 03/15/2021*/

        // if the target is in the attack zone, then the state will change
        if (myMaster.innerZone.asTarget != null) {
            ToAttack();//state will change to ToAttack()
            //Debug.Log("im attacking");//DEBUG
        }
        /*End Add*/
        #endregion


    }

    /*
     * This section will change the state when one of these functions are called in the FakeUpdate()
     * 
     * */
    #region State Fonctions
    public override void ToWalk() {
        myMaster.ChangeState("WALK");
        //myMaster.FindClosestEnemy();

    }
    public override void ToIdle() {

    }
    public override void ToJump() {

    }
    public override void ToChase() {

    }

    public override void ToAttack() {
        myMaster.ChangeState("ATTACK"); //Add Patrick Vasile 03/15/2021
        myMaster.myAnimator.SetBool("isAttacking", true);//Add Patrick Vasile 03/15/2021
    }
    #endregion
}
