using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : CoroutineState
{
    public override bool IsLegal()
    {
        return true;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override IEnumerator RunState()
    {
        Debug.Log("chasing");
        yield return new WaitForEndOfFrame();
    }
}
