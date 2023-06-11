using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseStateHandler : MonoBehaviour
{
    [SerializeField] private List<CoroutineState> states = new List<CoroutineState>();
    [SerializeField] private Enemy refEnemy;
    private CoroutineState activeState;

    public Enemy RefEnemy { get => refEnemy; }

    private void Start()
    {
        SortStates();
        SubscribeHandler();
        StartCoroutine(RunStateMachine());
    }


    private IEnumerator RunStateMachine()
    {
        yield return new WaitForSeconds(5f);
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
