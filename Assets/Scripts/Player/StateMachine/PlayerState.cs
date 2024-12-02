using UnityEngine;

namespace SpeedTaxi.Player
{
    public abstract class PlayerState
    {
        #region FIELDS
        private PlayerStateMachine _ctx;
        private PlayerStateFactory _factory;
        protected PlayerState _currentSubState;
        protected PlayerState _currentSuperState;

        protected bool _isRootState = false;
        #endregion

        #region PROPERTIES
        public bool IsRootState { get { return _isRootState; } }
        public PlayerState CurrentSubState { get { return _currentSubState; } }
        public PlayerState CurrentSuperState { get { return _currentSuperState; } }
        public PlayerStateMachine Ctx { get { return _ctx; } }
        public PlayerStateFactory Factory { get { return _factory; } }
        #endregion

        #region CONSTRUCTOR
        public PlayerState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory)
        {
            _ctx = currentContext;
            _factory = stateFactory;
        }
        #endregion

        #region ABSTRACT METHODS
        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();
        #endregion

        #region CONCRETE METHODS
        public void UpdateStates()
        {
            UpdateState();
            //Debug.Log(this.GetType().ToString()); // For debugging states
            if (_currentSubState != null)
            {
                _currentSubState.UpdateStates();
            }
        }

        public void FixedUpdateStates()
        {
            FixedUpdateState();
            if (_currentSubState != null)
            {
                _currentSubState.FixedUpdateStates();
            }
        }

        public void ExitStates()
        {
            ExitState();
            if (_currentSubState != null)
            {
                _currentSubState.ExitStates();
            }
        }

        protected void SwitchState(PlayerState newState)
        {
            ExitStates();
            newState.EnterState();

            if (_isRootState)
            {
                _ctx.CurrentState = newState;
            }
            else if (_currentSuperState != null)
            {
                _currentSuperState.SetSubState(newState);
            }
        }

        public void SetSuperState(PlayerState newSuperState)
        {
            _currentSuperState = newSuperState;
        }

        public void SetSubState(PlayerState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
        #endregion
    }
}
