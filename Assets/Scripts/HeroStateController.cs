using System;
using UnityEngine;

public class HeroStateController : MonoBehaviour
{
    public event Action<StateChangedArgs> OnStateChanged;

    public struct StateChangedArgs
    {
        public State PreviousState;
        public State CurrentState;

        public StateChangedArgs(State previousState, State currentState)
        {
            CurrentState = currentState;
            PreviousState = previousState;
        }
    }

    [SerializeField] private State _currentState;
    public State CurrentState => _currentState;

    public void SetState(State state)
    {
        var previousState = _currentState;
        _currentState = state;

        OnStateChanged?.Invoke(new StateChangedArgs(previousState, _currentState));
    }

    [Flags]
    public enum State
    {
        None = 1 << 0,
        DontMove = 1 << 1,
        Stay = 1 << 2,
        Seek = 1 << 3,
        Move = 1 << 4,
        Attack = 1 << 5,
        Dead = 1 << 6,
    }
}