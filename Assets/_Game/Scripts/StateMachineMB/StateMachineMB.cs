using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineMB : MonoBehaviour
{
    public State CurrentState { get; private set; }
    State _previousState;

    bool _isTransition = false;

    // ChangeState changes the current state to the new state
    public void ChangeState(State newState)
    {
        if (CurrentState == newState || _isTransition)
            return;
        
        ChangeStateSequence(newState);
    }

    // ChangeState changes the current state to the new state after a delay
    public void ChangeState(State newState, float delay)
    {
        if (CurrentState == newState || _isTransition)
            return;

        StartCoroutine(ChangeStateSequenceWithDelay(newState, delay));
    }

    // 
    IEnumerator ChangeStateSequenceWithDelay(State newState, float delay)
    {
        _isTransition = true;

        CurrentState?.Exit();
        StoreStateAsPrevious(newState);

        yield return new WaitForSeconds(delay);

        CurrentState = newState;

        CurrentState?.Enter();
        _isTransition = false;    
    }

    void ChangeStateSequence(State newState)
    {
        _isTransition = true;

        CurrentState?.Exit();
        StoreStateAsPrevious(newState);

        CurrentState = newState;

        CurrentState?.Enter();
        _isTransition = false;
    }

    // StoreStateAsPrevious stores the current state as the previous state
    void StoreStateAsPrevious(State newState)
    {
        if (_previousState == null && newState != null) _previousState = newState;
        else if (_previousState != null && CurrentState != null) _previousState = CurrentState;
    }

    // I don't see this being used but it's a good idea to have it
    // ChangeStateToPrevious changes the current state to the previous state
    public void ChangeStateToPrevious()
    {
        if (_previousState == null)
            return;

        ChangeState(_previousState);
    }

    void Update()
    {
        if (CurrentState != null && !_isTransition)
            CurrentState?.Tick();
    }

    void FixedUpdate()
    {
        if (CurrentState != null && !_isTransition)
            CurrentState?.FixedTick();
    }


}
