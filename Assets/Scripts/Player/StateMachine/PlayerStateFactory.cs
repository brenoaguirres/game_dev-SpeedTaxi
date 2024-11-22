namespace SpeedTaxi.Player
{
    public class PlayerStateFactory
    {
        #region REFERENCES
        private PlayerStateMachine _ctx;
        #endregion

        #region CONSTRUCTOR
        public PlayerStateFactory(PlayerStateMachine ctx)
        {
            _ctx = ctx;
        }
        #endregion

        #region STATES
        public PlayerState Idle()
        {
            return new PlayerStateIdle(_ctx, this);
        }

        public PlayerState Air()
        {
            return new PlayerStateAir(_ctx, this);
        }

        public PlayerState Grounded()
        {
            return new PlayerStateGrounded(_ctx, this);
        }

        public PlayerState Accelerate()
        {
            return new PlayerStateAccelerate(_ctx, this);
        }

        public PlayerState Skill()
        {
            return new PlayerStateSkill(_ctx, this);
        }
        #endregion
    }
}