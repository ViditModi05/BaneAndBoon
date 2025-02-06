using UnityEngine;

public class PlayerShadowWallJumpState : PlayerState
{
    public PlayerShadowWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        stateTimer = .4f;
        rb.linearVelocity = new Vector2(5 * -player.direction, player.jumpForce);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.shadowAir);
        }
        if (player.isGrounded())
        {
            stateMachine.ChangeState(player.shadowState);
        }
    }
}
