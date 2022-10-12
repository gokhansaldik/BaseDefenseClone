using System;
using System.Collections.Generic;
using Class;
using Interface;

namespace StateMachine
{
    public class StateMachinee
    {
        #region Self Variables

        #region Private Variables

        private readonly List<StateTransformer> _stateTransformers = new List<StateTransformer>();
        private readonly List<StateTransformer> _anyStateTransformer = new List<StateTransformer>();
        private IState _currentState;

        #endregion

        #endregion

        public void SetState(IState state)
        {
            if (_currentState == state) return;
            _currentState?.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }

        public void Tick()
        {
            var stateTransformer = CheckForTransformer();
            if (stateTransformer != null) SetState(stateTransformer.To);

            _currentState.Tick();
        }

        private StateTransformer CheckForTransformer()
        {
            foreach (var stateTransformer in _anyStateTransformer)
                if (stateTransformer.Condition.Invoke())
                    return stateTransformer;

            foreach (var stateTransformer in _stateTransformers)
                if (stateTransformer.Condition.Invoke() && _currentState == stateTransformer.From)
                    return stateTransformer;

            return null;
        }

        public void AddState(IState from, IState to, Func<bool> condition)
        {
            var stateTransformer = new StateTransformer(from, to, condition);
            _stateTransformers.Add(stateTransformer);
        }
    }
}