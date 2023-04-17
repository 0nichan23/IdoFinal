using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseStateHandler : MonoBehaviour
{
    [SerializeField] private List<CoroutineState> states = new List<CoroutineState>();
    private CoroutineState activeState;

    private void Start()
    {
        SortStates();
        SubscribeHandler();
    }

    private IEnumerator RunStateMachine()
    {
        while (gameObject.activeInHierarchy)
        {
            if (!ReferenceEquals(activeState, null))
            {
                StopCoroutine(activeState.RunState());
                activeState.OnStateExit();
            }
            activeState = GetNextState();
            activeState.OnStateEnter();
            yield return StartCoroutine(activeState.RunState());

        }

        // run new state?
        //loop until object is turned off?
    }
    private void SortStates()
    {
        states.Sort((p1, p2) => p1.priority.CompareTo(p2.priority));
    }
    private void SubscribeHandler()
    {
        foreach (var item in states)
        {
            item.CacheHandler(this);
        }
    }

    private CoroutineState GetNextState()
    {
        foreach (var item in states)
        {
            if (item.IsLegal())
            {
                return item;
            }
        }
        return null;
    }





}
