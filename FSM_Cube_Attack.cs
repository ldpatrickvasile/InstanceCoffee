using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Patrick Vasile
 * AI System ATTACK
 * (USED the FRAMEWORK (template) OF Annick Dumais FSM to mount my own State Machine)
 * 2021-03-15
 * */

public class FSM_Cube_Attack : FSM_Etat //FSM_ETAT referenced at start of each state
{
    public FSM_Cube_Attack(FSM_Master_Cube myMaster) : base(myMaster) { }
    public override void FakeUpdate() {

        if (myMaster.innerZone.asTarget == null) {// if avatar no longer in the inner zone, then return to chase mode
            ToChase();
            //Debug.Log("im attacking"); //DEBUG
        }



    }


    /*
  * This section will change the state when one of these functions are called in the FakeUpdate()
  * 
  * */
    #region Fonctions for States
    public override void ToWalk() {

    }
    public override void ToIdle() {

    }
    public override void ToJump() {

    }
    public override void ToChase() {
        myMaster.ChangeState("CHASE");
        myMaster.myAnimator.SetBool("isAttacking", false); //revert animator back to false
    }

    public override void ToAttack() {

    }
    #endregion
}
