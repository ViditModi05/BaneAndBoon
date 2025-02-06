using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        stateTimer = .4f;
        rb.linearVelocity = new Vector2(5 * -player.direction, player.jumpForce);
        if (player.onJump != null)
        {
            player.onJump();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Input.GetKeyDown(KeyCode.Space) && player.CanJump())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);
            player.UseJump();
        }

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
        if(player.isGrounded())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
