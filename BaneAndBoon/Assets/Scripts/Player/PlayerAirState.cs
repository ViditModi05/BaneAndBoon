using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if(player.isGrounded())
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !player.isGrounded() && !player.inShadowState)
        {
            player.inShadowState = true;
            stateMachine.ChangeState(player.shadowState);
            player.StartCoroutine("BusyFor", .1f);
        }

        if (player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }
        if(xInput != 0)
        {
            rb.linearVelocity = new Vector2(player.moveSpeed * .8f * xInput, rb.linearVelocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && player.CanJump())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);
            player.UseJump();
        }
    }
}
