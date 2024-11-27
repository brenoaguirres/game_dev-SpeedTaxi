using SpeedTaxi.Player;
using UnityEngine;

public class PlayerStateBrake : PlayerState
{
    #region CONSTRUCTOR
    public PlayerStateBrake(PlayerStateMachine currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory) { }
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
        if (!Ctx.Inputs.Handbrake && (Ctx.Inputs.Accelerate <= 0.05f || Ctx.Inputs.Accelerate >= -0.05f))
            SwitchState(Factory.Idle());
        if (!Ctx.Inputs.Handbrake && (Ctx.Inputs.Accelerate > 0.05f || Ctx.Inputs.Accelerate < -0.05f))
            SwitchState(Factory.Accelerate());
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        Ctx.VehiclePhysics.EngineInput = Ctx.Inputs.Accelerate;
        Ctx.VehiclePhysics.WheelsInput = Ctx.Inputs.Steer;
        Ctx.VehiclePhysics.HandbrakeInput = Ctx.Inputs.Handbrake;
    }
    public override void FixedUpdateState()
    {
        
    }
    #endregion
}
