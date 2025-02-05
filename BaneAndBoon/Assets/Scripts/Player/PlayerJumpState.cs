using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.UseJump();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

    }
}
