using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Patrick Vasile
 * AI System JUMP (NOT CURRENTLY BEING USED)
 * (USED the FRAMEWORK (template) OF Annick Dumais FSM to mount my own State Machine)
 * 2021-03-05
 * */

public class FSM_Cube_Jump : FSM_Etat //FSM_ETAT referenced at start of each state
{
    public FSM_Cube_Jump(FSM_Master_Cube myMaster) : base(myMaster) { }

    //private float airDampening = 0.25f;

    public override void FakeUpdate()
    {
        //Vector3 inputs = myMaster.CheckInputs();

        //myMaster.Move(inputs * airDampening);
    }

    /*
  * This section will change the state when one of these functions are called in the FakeUpdate()
  * 
  * */

    public override void ToWalk()
    {

    }
    public override void ToIdle()
    {

    }
    public override void ToJump()
    {

    }

    public override void ToChase() {

    }

    public override void ToAttack() {

    }
}
