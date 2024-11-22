namespace SpeedTaxi.Player
{
    public class PlayerStateGrounded : PlayerState
    {
        #region CONSTRUCTOR
        public PlayerStateGrounded(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory) 
        {
            _isRootState = true;
            InitializeSubState();
        }
        #endregion

        #region PLAYERSTATE METHODS
        public override void InitializeSubState()
        {
            if (Ctx.Inputs.Accelerate > 0.05f || Ctx.Inputs.Accelerate < -0.05f)
                SetSubState(Factory.Accelerate());
            else if (Ctx.Inputs.Accelerate <= 0.05f || Ctx.Inputs.Accelerate >= -0.05f)
                SetSubState(Factory.Idle());
        }
        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            
        }
        public override void CheckSwitchStates()
        {
            
        }

        public override void UpdateState()
        {
            
        }
        public override void FixedUpdateState()
        {
            
        }
        #endregion
    }
}
