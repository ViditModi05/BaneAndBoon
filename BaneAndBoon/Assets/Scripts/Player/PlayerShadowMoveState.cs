using UnityEngine;

public class PlayerShadowMoveState : PlayerShadowGroundedState
{
    public PlayerShadowMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        rb.linearVelocity = new Vector2(xInput * player.moveSpeed, rb.linearVelocity.y);

        if (xInput == 0)
        {
            stateMachine.ChangeState(player.shadowState);
        }
    }
}
