using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Patrick Vasile
 * AI System IDLE
 * (USED the FRAMEWORK (template) OF Annick Dumais FSM to mount my own State Machine)
 * 2021-03-05
 * */
public class FSM_Cube_Idle : FSM_Etat //FSM_ETAT referenced at start of each state
{
    public FSM_Cube_Idle(FSM_Master_Cube myMaster) : base(myMaster) { }

    public override void FakeUpdate()
    {
        //Si input Gauche Droite --> ToWalk();
        //Si input Vertical --> ToJump();

        //Debug.Log("Je suis Idle"); //DEBUG
        //myMaster.CheckWait();
         /*if (Input.GetKeyDown(KeyCode.Return))
         {
             ToWalk();
         }
         */
        myMaster.CheckDistance();//check distance to point

        #region State Change to Walk

        if (myMaster.Wait == false) {//if no longer waiting
            ToWalk();//state change to walk
        }
        #endregion
        /*
                Vector3 inputs = myMaster.CheckInputs();
                if (Input.GetButtonDown("Jump"))
                {
                    ToJump();
                    return;
                }

                if (inputs.magnitude > 0) {
                    ToWalk();
                }
              */

    }

    /*
  * This section will change the state when one of these functions are called in the FakeUpdate()
  * vvvvvvvvvvv
  * */
    #region Fonctions for States
    public override void ToWalk()
    {
        //Transition vers l'etat de Marche
        myMaster.ChangeState("WALK");
        myMaster.myAnimator.SetBool("isWalking", true);
    }
    public override void ToIdle()
    {
        //Je suis deja en IDLE...
    }
    public override void ToJump()
    {
        //Jouer l'anim d'impulsion...

        //Transition vers l'etat de Jump
        myMaster.ChangeState("JUMP");
        
    }
    
    public override void ToChase() {

    }

    public override void ToAttack() {

    }
    #endregion
}
