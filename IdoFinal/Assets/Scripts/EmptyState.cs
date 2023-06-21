using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyState : CoroutineState
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
        Debug.Log(handler.RefEnemy + " is playing empty");
        yield return null;
    }
}
