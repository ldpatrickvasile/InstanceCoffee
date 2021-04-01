using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM_Etat // this class will be referenced by all the states in the state machine
{
    protected FSM_Master_Cube myMaster;


    public FSM_Etat(FSM_Master_Cube myMaster)
    {
        this.myMaster = myMaster;
    }//my master will be called on when using the states
    #region Abstract Fonctions for FSM

    public abstract void FakeUpdate();
    public abstract void ToWalk();
    public abstract void ToIdle();
    public abstract void ToJump();


    //new
    public abstract void ToChase();

    public abstract void ToAttack();
}
#endregion

#region Fonctions that go in State Scripts
/*    
 *  public override void FakeUpdate()
    {

    }
    public override void ToWalk()
    {

    }
    public override void ToIdle()
    {

    }
    public override void ToJump()
    {

    }
    public override void ToChase(){

    }

    public override void ToAttack(){

    }

    myAnimator.SetBool("isWalking", true);

*/
#endregion