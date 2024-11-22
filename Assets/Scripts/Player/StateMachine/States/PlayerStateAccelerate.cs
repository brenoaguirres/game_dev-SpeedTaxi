using SpeedTaxi.Player;
using UnityEngine;

public class PlayerStateAccelerate : PlayerState
{
    #region CONSTRUCTOR
    public PlayerStateAccelerate(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory) { }
    #endregion

    #region PLAYERSTATE METHODS

    public override void InitializeSubState()
    {
        
    }
    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.Inputs.Accelerate <= 0.05f || Ctx.Inputs.Accelerate >= -0.05f)
            SwitchState(Factory.Idle());
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        Ctx.VehiclePhysics.EngineInput = Ctx.Inputs.Accelerate;
        Ctx.VehiclePhysics.WheelsInput = Ctx.Inputs.Steer;
    }
    public override void FixedUpdateState()
    {
        
    }
    #endregion
}
