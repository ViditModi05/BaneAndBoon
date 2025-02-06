using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        stateTimer = player.dashDuration;
    }

    public override void ExitState()
    {
        base.ExitState();
        rb.linearVelocity = new Vector2(0 , rb.linearVelocity.y);
        player.StartCoroutine("BusyFor", .15f);
        player.isDashing = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(!player.isGrounded() && player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }
        rb.linearVelocity = new Vector2(player.dashSpeed * player.dashDirection, 0);
        if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
