using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CoroutineState
{
    public override bool IsLegal()
    {
        return true;
       //player must ne withing range && attack must not be in cool down
    }

    public override IEnumerator RunState()
    {
        //play attack animation and mark time stamp
        throw new System.NotImplementedException();
    }

    public override void OnStateEnter()
    {
        //nothing 
        throw new System.NotImplementedException();
    }

    public override void OnStateExit()
    {
        //nothing
        throw new System.NotImplementedException();
    }
}
