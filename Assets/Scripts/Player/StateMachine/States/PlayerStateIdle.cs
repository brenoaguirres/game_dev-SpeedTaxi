namespace SpeedTaxi.Player
{
    public class PlayerStateIdle : PlayerState
    {
        #region CONSTRUCTOR
        public PlayerStateIdle(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory) { }
        #endregion

        #region PLAYERSTATE METHODS

        public override void InitializeSubState()
        {
            throw new System.NotImplementedException();
        }
        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }
        public override void CheckSwitchStates()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }
        public override void FixedUpdateState()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
