using UnityEngine;

public class PlayerShadowState : PlayerShadowGroundedState
{
    public PlayerShadowState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        rb.linearVelocity = new Vector2(0, 0);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(xInput != 0)
        {
            stateMachine.ChangeState(player.shadowMove);
        }
    }
}
